using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.NewScripts
{
    public class Inventory : MonoBehaviour
    {
        public GameObject[] cookieInventory = new GameObject[13];
        public GameObject[] heartInventory = new GameObject[4];
        public GameObject[] heartSlots;
        public bool[] heartIsFull;
        public bool cookieIsEmpty = true;

        public void AddItemHeart(GameObject itemHeart)
        {
            //Debug.Log("Got To Adding The Heart");
            bool itemAdded = false;
            //Find the first open slot in heart inventory
            for (int i = 0; i < heartInventory.Length && i < heartSlots.Length ; i++)
            {
                if (heartInventory[i] == null)
                {
                    heartInventory[i] = itemHeart;
                    itemAdded = true;
                    //itemHeart.SendMessage("DoInteraction");
                    break;
                }
            }
            if (!itemAdded)
            {
                Debug.Log("Inventory Full - Item Not Added");
            }
        }

        public void AddItemCookie(GameObject itemCookie)
        {
             bool itemAdded = false;
            //Find the first open slot in cookie inventory
            for (int i = 0; i < cookieInventory.Length; i++)
            {
                if (cookieInventory[i] == null)
                {
                    cookieInventory[i] = itemCookie;
                    Debug.Log(itemCookie.name + " was added to cookie inventory");
                    itemAdded = true;
                    //Do something with the object
                    itemCookie.SendMessage("DoInteraction");
                    break;
                    ;
                }
            }
            //Inventory full
            if (!itemAdded)
            {
                Debug.Log("Inventory Full - Item Not Added");
            }
        }
    }
}
