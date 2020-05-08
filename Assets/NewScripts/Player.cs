using UnityEngine;
//using static Utilities;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fridge;
    public InventoryObject trash;
    public InventoryObject topOvenRack;
    public InventoryObject bottomOvenRack;
    public InventoryObject topCookieRack;
    public InventoryObject bottomCookieRack;

    //[Line]
    public UserInterface _inventorySlot;

    public delegate void AddToInventoryHandler(Item item, float currentRotTime, float currentRotRate);
    public event AddToInventoryHandler OnAdded;

    public void OnTriggerEnter2D(Collider2D other)
    {       
        HeartItem thisItem = other.GetComponent<HeartItem>();

        if (thisItem)
        {
            LocationTypes location = thisItem.Location;
            float currentRotTime = thisItem.currentRotTime;
            float currentRotRate = thisItem.currentRotRate;
            
            Item _item = new Item(thisItem.item);

            if (inventory.AddItem(_item, 1, location, currentRotTime, currentRotRate))
            {
                OnAdded?.Invoke(_item, currentRotTime, currentRotRate);
                //RotTimer rotTime = GetComponentInChildren<RotTimer>();
                //rotTime.StartRot(_item, currentRotTime, currentRotRate);
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
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        fridge.Clear();
        trash.Clear();
        topOvenRack.Clear();
        bottomOvenRack.Clear();
        topCookieRack.Clear();
        bottomCookieRack.Clear();
    }
}
