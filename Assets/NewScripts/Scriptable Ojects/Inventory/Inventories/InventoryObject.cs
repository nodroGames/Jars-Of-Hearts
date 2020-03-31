using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using System;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Scriptable Objects/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    //public InterfaceType type;
    public LocationTypes Location;
    public Inventory Container;
    public InventorySlot[] GetSlots { get { return Container.Slots; } }

    public bool AddItem(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {
        if (EmptySlotCount <= 0 && !database.ItemObjects[_item.Id].stackable)
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if (EmptySlotCount <= 0 && slot == null && database.ItemObjects[_item.Id].stackable)
            return false;
        if (!database.ItemObjects[_item.Id].stackable || slot == null)
        {
            SetFirstEmptySlot(_item, _amount, _location, _currentRotTime, _currentRotRate);
            return true;
        }
        slot.AddAmount(_amount);
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
    public InventorySlot SetFirstEmptySlot(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].UpdateSlot(_item, _amount, _location, _currentRotTime, _currentRotRate);
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
            InventorySlot tempSlot = new InventorySlot(item2.item, item2.amount, item2.location,item2.currentRotTime, item2.currentRotRate);
            item2.UpdateSlot(item1.item, item1.amount, item1.location, item1.currentRotTime, item1.currentRotRate);
            item1.UpdateSlot(tempSlot.item, tempSlot.amount, tempSlot.location, tempSlot.currentRotTime, tempSlot.currentRotRate);
        }
    }

    public void RemoveItem(Item _item, LocationTypes _location, float _currentRotTime)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item == _item)
            {
                GetSlots[i].UpdateSlot(null, 0, null, 0, 0);
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
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount, newContainer.Slots[i].location, newContainer.Slots[i].currentRotTime, newContainer.Slots[i].currentRotRate);
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

public delegate void SlotUpdated(InventorySlot _slot); //reference to Inventory Slot

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
    [System.NonSerialized]
    public LocationTypes location;
    [System.NonSerialized]
    public float currentRotTime;
    [System.NonSerialized]
    public float currentRotRate;

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
        //might need to delete
        set
        {

        }
    }

    public InventorySlot()
    {
        UpdateSlot(new Item(), 0, location, currentRotTime, currentRotRate);
    }
    public InventorySlot(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {
        UpdateSlot(_item, _amount, _location, _currentRotTime, _currentRotRate);
    }
    public void UpdateSlot(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        location = _location;
        currentRotTime = _currentRotTime;
        currentRotRate = _currentRotRate;

        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0, null, 0, 0);
    }
    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value, location, currentRotTime, currentRotRate);
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