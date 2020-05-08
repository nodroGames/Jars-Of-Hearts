using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenRack : MonoBehaviour
{
    public  InventoryObject cookieInventory = default;

    public ItemDatabaseObject database;
    
    public UserInterface inventory = default;

    public void SetCookiesOnRack(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {
        CookieItem cookieItem = GetComponentInChildren<CookieItem>();
        cookieItem.CovertToCookieType(_item);

        if (cookieItem)
        {
            LocationTypes location = _location;
            float currentRotTime = _currentRotTime;
            float currentRotRate = _currentRotRate;

            Item item = new Item(cookieItem.product);

            for (int i = 0; i < cookieInventory.GetSlots.Length; i++)
            {
                if (cookieInventory.AddItem(item, 1, location, currentRotTime, currentRotRate))
                {
                    CookProduct cook = GetComponentInChildren<CookProduct>();
                    cook.StartTimer(item);
                }
            }
        }
    }
}
