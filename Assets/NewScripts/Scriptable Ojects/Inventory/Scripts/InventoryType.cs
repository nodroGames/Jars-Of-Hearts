using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    Cookie,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Inventory System/Items")]
public class InventoryType : ScriptableObject
{
    [Header("= Visuals =")]
    public Sprite uiDisplay;

    [Header("= Data =")]
    public bool stackable;
    public Item data = new Item();
    public ItemType type;
    public LocationTypes location;
    [TextArea(15, 20)]
    public string description;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}


[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;

    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(InventoryType item)
    {
        Name = item.name;
        Id = item.data.Id;
    }
}
