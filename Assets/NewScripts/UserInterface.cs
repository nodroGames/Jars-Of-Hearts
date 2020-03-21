using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public abstract class UserInterface : MonoBehaviour
{

    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public List<LocationTypes> listOfLocations;

    //public List<Sprite> listOfProductImages;

    public List<InventoryType> listOfProducts;

    //[SerializeField]
    //private ProductInSlot slotScript;

    private InventorySlot tempSlot;

    private LocationTypes tempLocation;

    private int rotBaseTime;
    private float tempRotTime;
    private float tempRotRate;
    private float half;
    private float quarter;
    private float tempRotStartTime;
    

    private void Awake()
    {
        rotBaseTime = 60;
        half = 0.50f;
        quarter = 0.75f;
        tempRotStartTime = 0.01f;
    }

    void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;

        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    //Update is called once per frame
    void Update()
    {
        findRotRate();
        rotOverTime();
    }

    private void findRotRate()
    {
        if (tempLocation != null)
        {
            if (tempLocation == listOfLocations[0])
            {
                tempRotRate = 1.5f;
            }
            if (tempLocation == listOfLocations[1])
            {
                tempRotRate = 0.5f;
            }
            if (tempLocation == listOfLocations[2])
            {
                tempRotRate = .25f;
            }
            if (tempLocation == listOfLocations[3])
            {
                tempRotRate = 0f;
            }
            if (tempLocation == listOfLocations[4])
            {
                tempRotRate = 0f;
            }
        }
    }

    private void rotOverTime()
    {
        //.item.Id >= 0 /

        if (tempSlot != null && tempRotTime > tempRotStartTime)
        {
            foreach (KeyValuePair<GameObject, InventorySlot> obj in slotsOnInterface)
            {
                if (obj.Value.currentRotTime > 0.00f)
                {    
                    obj.Value.currentRotTime -= Time.deltaTime * tempRotRate;
                }

                if (obj.Value.currentRotTime <= rotBaseTime * 0)
                {
                    obj.Value.ItemObject = listOfProducts[3];
                    //tempSlot.productSO = listOfProducts[3];
                    //HealthState = mush;
                    //item = (InventoryType)heartStateType;
                    //ReadData();
                }
                else if (obj.Value.currentRotTime <= rotBaseTime * half)
                {
                    obj.Value.ItemObject = listOfProducts[2];
                    //slotsOnInterface[obj].ItemObject.uiDisplay
                    //tempSlot.productSO = listOfProducts[2];
                    //HealthState = halfRot;
                    //item = (InventoryType)heartStateType;
                    //ReadData();


                }
                else if (obj.Value.currentRotTime <= rotBaseTime * quarter)
                {
                    obj.Value.ItemObject = listOfProducts[1];
                    //obj.Value.productSO = listOfProducts[1];
                    obj.Value.item = listOfProducts[1].data;
                    //Item _item = new Item(obj.Value.ItemObject);
                    

                    obj.Value.UpdateSlot(obj.Value.item, obj.Value.amount, obj.Value.location, obj.Value.currentRotTime, obj.Value.currentRotRate);
                    //tempSlot.productSO = listOfProducts[1];

                   
                    Debug.Log("should be quarter brown " + obj.Value.ItemObject);
                    //OnSlotUpdate(tempSlot);

                    //Item _item = new Item(tempSlot.productSO);

                    //tempSlot.slotDisplay.GetComponentInChildren<Image>().sprite = listOfProductImages[1];
                    //HealthState = quarterRot;
                    //item = (InventoryType)heartStateType;
                    //ReadData();
                }
                else
                {
                    return;
                }
            }            
        }
    }

    private void OnSlotUpdate(InventorySlot _slot)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> obj in slotsOnInterface)
        {
            tempSlot = _slot;
            tempRotTime = _slot.currentRotTime;
            tempRotRate = _slot.currentRotRate;
            //Debug.Log(_slot.ItemObject);
        }

        if (_slot.item.Id >= 0)
        {
            //_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.productSO.uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            //slotScript = _slot.slotDisplay.GetComponent<ProductInSlot>();
            _slot.location = inventory.Location;
            tempLocation = _slot.location;
            //slotScript.SetHeartUI(_slot.currentRotTime, _slot.location);
            //_slot.RotOverTime();
            //_slot.currentRotTime = ProductInSlot.curr
            //Debug.Log("This is the current SO " + _slot.productSO);
            //Debug.Log("This is the current location " + _slot.location);
            //Debug.Log("This is the current rot time " + _slot.currentRotTime);
            //rotOverTime(_slot);
            //while (_slot.currentRotTime > 0.00f)
            //{

            //_slot.currentRotTime -= controlledTime * .25f;
            //Debug.Log("This Is Also Working");
            //}
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

    }

    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(120, 195);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged); //Destroys the temporarily created item.
        if (MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.slotHoveredOver)
        {

            //if (inventory.type == InterfaceType.Trash)
            //{

            //}
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
    }
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

}

public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
    //public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    //{
    //    foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
    //    {
    //        if (_slot.Value.item.Id >= 0)
    //        {
    //            _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
    //            _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
    //            _slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    //            _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
    //        }
    //        else
    //        {
    //            _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
    //            _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
    //            _slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    //            _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
    //        }
    //    }
    //}
}