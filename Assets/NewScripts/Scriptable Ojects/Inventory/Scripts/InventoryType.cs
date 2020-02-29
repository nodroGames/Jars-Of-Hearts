using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    Heart,
    Cookie,
    Default
}

public enum ProductState
{
    Healthy,
    QuarterRot,
    HalfRot,
    Mush
}

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Inventory System/Items")]
public class InventoryType : ScriptableObject
{
    [Header("= Visuals =")]
    public Sprite uiDisplay;

    [Header("= Data =")]
    public ProductState state;
    public bool stackable;
    public Item data = new Item();
    public ProductType type;
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
