using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    Cookie,
    Default
}

public enum Attributes
{
    Stackable,
    NotStackable
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
    //public int value;
    public ItemBuff[] buffs;

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
    public ItemBuff[] buffs;

    public Item(InventoryType item)
    {
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {            
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}
[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
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
