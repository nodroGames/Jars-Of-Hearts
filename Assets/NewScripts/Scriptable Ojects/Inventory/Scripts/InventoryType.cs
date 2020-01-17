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
    public GameObject prefab;

    [Header("= Data =")]
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public int value;
}
