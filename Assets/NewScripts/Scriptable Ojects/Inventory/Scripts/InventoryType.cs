using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    Cookie,
    Default
}

//public enum Attributes
//{
//    Stackable,
//    NotStackable
//}

public abstract class InventoryType : ScriptableObject
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
    //public ItemBuff[] buffs;

    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(InventoryType item)
    {
        Name = item.name;
        Id = item.data.Id;
        //buffs = new ItemBuff[item.data.buffs.Length];
        //for (int i = 0; i < buffs.Length; i++)
        //{            
        //    buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
        //    buffs[i].attribute = item.data.buffs[i].attribute;
        //}
    }
}
[System.Serializable]
public class ItemBuff
{
    //public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}
