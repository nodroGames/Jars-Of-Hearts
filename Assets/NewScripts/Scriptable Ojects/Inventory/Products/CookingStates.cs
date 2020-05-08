using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStates : MonoBehaviour
{
    [SerializeField]
    private InventoryType[] healthyCookieStates;
    [SerializeField]
    private InventoryType[] quarterCookieStates;
    [SerializeField]
    private InventoryType[] halfCookieStates;
    [SerializeField]
    private CookProduct timer;

    private OvenRack ovenRack;
    private ItemDatabaseObject database;
    private UserInterface inventory;
    private InventoryObject slots;

    private Item item;
    private InventoryType[] currentStateArray;
    private InventoryType currentState;
    private LocationTypes location = null;
    private int currentRotRate = 0;
    private int currentRotTime = 0;

    private void Awake()
    {
        ovenRack = GetComponent<OvenRack>();
        if (ovenRack)
        {
            database = ovenRack.database;
            inventory = ovenRack.inventory;
            slots = ovenRack.cookieInventory;
        }
        item = null;
        currentStateArray = null;
        currentState = null;
    }

    public bool SetupState(Item _item)
    {
        if (_item.Id == 16)
        {
            return false;
        }
        else
        {
            item = _item;

            if (item.Id == 4)
            {
                currentStateArray = healthyCookieStates;
            }
            else if (item.Id == 8)
            {
                currentStateArray = quarterCookieStates;
            }
            else if (item.Id == 12)
            {
                currentStateArray = halfCookieStates;
            }
            Bake();
            return true;
        }
    }

    public void Bake()
    {
        for (int j = 0; j < currentStateArray.Length - 1; j++)
        {
            if (item.Name == currentStateArray[j].name)
            {
                currentState = currentStateArray[j + 1];
            }
        }
        item = currentState.data;

        for (int i = 0; i < slots.GetSlots.Length; i++)
        {
            slots.GetSlots[i].UpdateSlot(item, 1, location, currentRotTime, currentRotRate);
        } 
       timer.StartTimer(item);
       return;
    }
}
