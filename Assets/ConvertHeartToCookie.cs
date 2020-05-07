using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConvertHeartToCookie : MonoBehaviour
{
    private Item item;
    private OvenRack ovenRack;
    private ItemDatabaseObject database;

    private void Awake()
    {
        ovenRack = GetComponent<OvenRack>();
        if (ovenRack)
        {
            database = ovenRack.database;
        }
    }

    public void ConvertToCorrectCookie(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {        
        item = _item;

        if (item.Id == 0)
        {
            item.Id = 4;
            item = database.ItemObjects[item.Id].data;            
        }
        ovenRack.SetCookiesOnRack(item, _amount, _location, _currentRotTime, _currentRotRate);
    }

}
