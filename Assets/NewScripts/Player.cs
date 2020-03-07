﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fridge;

    //[SerializeField]

    public UserInterface _inventorySlot;

    private Transform heartPool;

    private void Awake()
    {
        heartPool = GameObject.FindGameObjectWithTag("HeartPool").GetComponent<Transform>();

        
    }    

    public void OnTriggerEnter2D(Collider2D other)
    {       
        HeartItem thisItem = other.GetComponent<HeartItem>();

        if (thisItem)
        {
            InventoryType productSO = thisItem.item;
            LocationTypes location = thisItem.Location;
            float currentRotTime = thisItem.currentRotTime;
            
            Item _item = new Item(thisItem.item);

            if (inventory.AddItem(_item, 1, location, productSO, currentRotTime))
            {
                //other.gameObject.transform.position = new Vector2(heartPool.position.x, heartPool.position.y);                
                

                //_heartInSlotScript.SetHeartUI(thisItem.Location, thisItem.item, thisItem.currentRotTime);

                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            fridge.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            fridge.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        fridge.Clear();
    }

}
