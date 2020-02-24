using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefaultItem", menuName = "Scriptable Objects/Inventory System/Inventory Item/Default Type")]
public class DefaultItemType : InventoryType
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}
