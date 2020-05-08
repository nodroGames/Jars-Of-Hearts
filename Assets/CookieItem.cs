using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieItem : MonoBehaviour
{
    public InventoryType product;

    [SerializeField]
    private ItemDatabaseObject database;
    [SerializeField]
    private InventoryType rawHealthyCookie;
    [SerializeField]
    private InventoryType rawQuarterCookie;
    [SerializeField]
    private InventoryType rawHalfCookie;

    public void CovertToCookieType(Item _item)
    {
        if (_item.Id == 0)
        {
            product = rawHealthyCookie;
        }

        if (_item.Id == 1)
        {
            product = rawQuarterCookie;
        }

        if (_item.Id == 2)
        {
            product = rawHalfCookie;
        }
    }

}

