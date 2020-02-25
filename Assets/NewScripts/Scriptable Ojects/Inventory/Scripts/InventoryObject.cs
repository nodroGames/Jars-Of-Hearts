﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Scriptable Objects/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public Inventory Container;
    public ItemDatabaseObject database;

    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0)
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if(!database.GetItem[_item.Id].stackable || slot == null)
        {
            SetFirstEmptySlot(_item, _amount);
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
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
                return Container.Items[i];
        }
        return null;
    }
    public InventorySlot SetFirstEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id <= -1)
            {
                Container.Items[i].UpdateSlot(_item, _amount);
                return Container.Items[i];
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
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(tempSlot.item, tempSlot.amount);
        }
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(null, 0);
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
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
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
    public InventorySlot[] Items = new InventorySlot[4]; //there are only 4 slots available by default.
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(new Item(), 0);
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public UserInterface parent;
    public Item item;
    public int amount;

    public InventoryType ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.GetItem[item.Id];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        item = null;
        amount = 0;
    }
    public InventorySlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void RemoveItem()
    {
        item = new Item();
        amount = 0;
    }
    public void AddAmount(int value)
    {
        amount += value;
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