using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fridge;

    //public LocationTypes inventoryLocation;

    private Transform heartPool;

    private void Awake()
    {
        heartPool = GameObject.FindGameObjectWithTag("HeartPool").GetComponent<Transform>();
    }    

    public void OnTriggerEnter2D(Collider2D other)
    {
        var thisItem = other.GetComponent<HeartItem>();
        if (thisItem)
        {
            Item _item = new Item(thisItem.item);
            if (inventory.AddItem(thisItem, _item, 1))
            {
                other.gameObject.transform.position = new Vector2(heartPool.position.x, heartPool.position.y);
                //thisItem.Location = inventoryLocation;
                //Destroy(other.gameObject);
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
