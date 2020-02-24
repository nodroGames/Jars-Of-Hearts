using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHeartItem", menuName = "Scriptable Objects/Inventory System/Inventory Item/Heart Type")]
public class HeartItemType : InventoryType
{
    public void Awake()
    {
        type = ItemType.Heart;
    }
}
