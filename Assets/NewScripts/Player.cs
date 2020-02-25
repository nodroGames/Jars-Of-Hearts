using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fridge;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<HeartItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
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
        inventory.Container.Clear();
        fridge.Container.Clear();
    }

}
