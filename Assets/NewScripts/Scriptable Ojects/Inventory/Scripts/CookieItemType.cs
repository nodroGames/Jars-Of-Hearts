using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCookieItem", menuName = "Scriptable Objects/Inventory Item/Cookie Type")]
public class CookieItemType : InventoryType
{
    public void Awake()
    {
        type = ItemType.Cookie;
    }
}
