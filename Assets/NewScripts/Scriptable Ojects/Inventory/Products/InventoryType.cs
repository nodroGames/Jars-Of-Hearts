using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Utilities;


public enum ProductType
{
    Heart,
    Cookie,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Inventory System/Items")]
public class InventoryType : ScriptableObject
{
    [Header("= Visuals =")]
    //[Line]
    public Sprite uiDisplay;

    [Header("= Data =")]
    //[Line]
    public bool stackable;
    public Item data = new Item();
    public ProductType type;
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
    //public LocationTypes location;
    //public float currentRotTime;
    //public float currentRotRate;

    public Item()
    {
        Name = "";
        Id = -1;
        //location = null;
        //currentRotTime = 0;
        //currentRotRate = 0;

    }
    public Item(InventoryType item)
    {
        Name = item.name;
        Id = item.data.Id;
        //location = item.data.location;
        //currentRotTime = item.data.currentRotTime;
        //currentRotRate = item.data.currentRotRate;
    }
}
