using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    Cookie,
    Default
}


public abstract class InventoryType : ScriptableObject
{
    [Header("= Visuals =")]
    public Sprite uiDisplay;

    [Header("= Data =")]
    public int Id;
    public ItemType type;
    public LocationTypes location;
    [TextArea(15, 20)]
    public string description;
    public int value;

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
    public int Id;

    public Item(InventoryType item)
    {
        Name = item.name;
        Id = item.Id;
    }
}
