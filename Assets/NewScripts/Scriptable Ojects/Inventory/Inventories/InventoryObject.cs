using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using System;

public enum InterfaceType
{
    Inventory,
    Fridge,
    Oven,
    Trash
}

[CreateAssetMenu(fileName = "NewInventory", menuName = "Scriptable Objects/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public InterfaceType type;
    public LocationTypes inventoryLocation;
    public Inventory Container;
    public InventorySlot[] GetSlots { get { return Container.Slots; } }

    //public bool AddItem(HeartItem changeableItem, Item _item, int _amount)
    //{
    //    if (EmptySlotCount <= 0 && !database.ItemObjects[_item.Id].stackable)
    //        return false;
    //    InventorySlot slot = FindItemOnInventory(_item);
    //    if (!database.ItemObjects[_item.Id].stackable || slot == null)
    //    {
    //        SetFirstEmptySlot(_item, _amount);
    //        changeableItem.Location = inventoryLocation;
    //        Debug.Log(changeableItem.currentRotRate);
    //        //database.ItemObjects[_item.Id].location = InterfaceLocation.Inventory;
    //        //Debug.Log("Item Got Added and loction changed" + database.ItemObjects[_item.Id].location);
    //        return true;
    //    }
    //    slot.AddAmount(_amount);
    //    changeableItem.Location = inventoryLocation;
    //    return true;
    //}

    public bool AddItem(HeartItem changeableItem, Item _item, int _amount)
    {
        if (EmptySlotCount <= 0 && !database.ItemObjects[_item.Id].stackable)
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if (EmptySlotCount <= 0 && slot == null && database.ItemObjects[_item.Id].stackable)
            return false;
        if (!database.ItemObjects[_item.Id].stackable || slot == null)
        {
            SetFirstEmptySlot(/*changeableItem, */_item, _amount);
            changeableItem.Location = inventoryLocation;
            //changeableItem.ReadData();
            Debug.Log(changeableItem.currentRotRate);
            return true;
        }
        slot.AddAmount(_amount);
        changeableItem.Location = inventoryLocation;
        //changeableItem.ReadData();
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id)
                return GetSlots[i];
        }
        return null;
    }
    public InventorySlot SetFirstEmptySlot(/*HeartItem changeableItem, */Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].UpdateSlot(/*changeableItem, */_item, _amount);
                return GetSlots[i];
            }
        }
        // Setup function for full inventory
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if(item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot tempSlot = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(/*item1.changeableItem, */item1.item, item1.amount);
            item1.UpdateSlot(/*tempSlot.changeableItem, */tempSlot.item, tempSlot.amount);
        }
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item == _item)
            {
                GetSlots[i].UpdateSlot(/*null, */null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(/*newContainer.Slots[i].changeableItem, */newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[4]; //there are only 4 slots available by default.
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}

public delegate void SlotUpdated(/*HeartItem _changeableItem,*/ InventorySlot _slot); //reference to Inventory Slot

[System.Serializable]
public class InventorySlot
{
    public ProductType[] AllowedItems = new ProductType[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay; // Link to the display of the slot
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public Item item = new Item();
    public int amount;
    //public HeartItem changeableItem;

    public InventoryType ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.Id];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        UpdateSlot(/*new HeartItem(), */new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(/*changeableItem, */_item, _amount);
    }
    public void UpdateSlot(/*HeartItem _changeableItem, */Item _item, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        //changeableItem = _changeableItem;
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void RemoveItem()
    {
        UpdateSlot(/*new HeartItem(), */new Item(), 0);
    }
    public void AddAmount(int value)
    {
        UpdateSlot(/*changeableItem, */item, amount += value);
    }
    public bool CanPlaceInSlot(InventoryType _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
        {
            return true;
        }
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
            {
                return true;
            }
        }
        return false;
    }
}