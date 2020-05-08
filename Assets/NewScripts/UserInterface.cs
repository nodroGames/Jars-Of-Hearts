using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{

    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    
    //public List<InventoryType> listOfProducts;

    //private int rotBaseTime;

    //private float half;
    //private float quarter;

    public delegate void RemoveFromInventoryHandler(InventorySlot slotsOnInterface);
    public event RemoveFromInventoryHandler OnRemoved;

    //private void Awake()
    //{
    //    rotBaseTime = 60;
    //    half = 0.50f;
    //    quarter = 0.75f;
    //}

    void Start()
    {
        CallUpdate();        
    }

    //Update is called once per frame
    void Update()
    {
        //swapProductState();
    }

    public void CallUpdate()
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

    //private void swapProductState()
    //{
    //        foreach (KeyValuePair<GameObject, InventorySlot> obj in slotsOnInterface)
    //        {
    //        if (obj.Value.item.Id >= 0)
    //        {
    //            // Changes to Mush State
    //            if (obj.Value.currentRotTime <= rotBaseTime * 0)
    //            {
    //                obj.Value.ItemObject = listOfProducts[3];
    //                obj.Value.item = listOfProducts[3].data;

    //                obj.Value.UpdateSlot(obj.Value.item, obj.Value.amount, obj.Value.location, obj.Value.currentRotTime, obj.Value.currentRotRate);

    //                obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = false;
    //            }

    //            // Changes to half rot state
    //            else if (obj.Value.currentRotTime <= rotBaseTime * half)
    //            {
    //                if (obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled == false)
    //                {
    //                    obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = true;
    //                }
    //                if (obj.Value.currentRotTime > 0.00f)
    //                {
    //                    obj.Value.currentRotTime -= Time.deltaTime * obj.Value.currentRotRate;
    //                }
    //                else { return; }

    //                obj.Value.ItemObject = listOfProducts[2];
    //                obj.Value.item = listOfProducts[2].data;

    //                obj.Value.UpdateSlot(obj.Value.item, obj.Value.amount, obj.Value.location, obj.Value.currentRotTime, obj.Value.currentRotRate);
    //            }

    //            // Changes to quarter rot state
    //            else if (obj.Value.currentRotTime <= rotBaseTime * quarter)
    //            {
    //                if (obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled == false)
    //                {
    //                    obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = true;
    //                }
    //                if (obj.Value.currentRotTime > 0.00f)
    //                {
    //                    obj.Value.currentRotTime -= Time.deltaTime * obj.Value.currentRotRate;
    //                }
    //                else { return; }

    //                obj.Value.ItemObject = listOfProducts[1];
    //                obj.Value.item = listOfProducts[1].data;

    //                obj.Value.UpdateSlot(obj.Value.item, obj.Value.amount, obj.Value.location, obj.Value.currentRotTime, obj.Value.currentRotRate);
    //            }

    //            // Starts at healthy heart state
    //            else
    //            {
    //                if (obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled == false)
    //                {
    //                    obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = true;
    //                }
    //                if (obj.Value.currentRotTime > 0.00f)
    //                {
    //                    obj.Value.currentRotTime -= Time.deltaTime * obj.Value.currentRotRate;
    //                }
    //                else { return; }
    //            }
    //        }
    //    }
    //}

    private void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.item.Id >= 0)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            _slot.location = inventory.Location;
            _slot.currentRotRate = inventory.Location.rotRate;

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
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            // If the mouse hovers over the trash can prefab it will distroy to item out of the inventory
            if (slotsOnInterface[obj].item.Id >= 0 && MouseData.interfaceMouseIsOver.name == "Trash Can Panel")
            {
                slotsOnInterface[obj].RemoveItem();
            }
            else if (slotsOnInterface[obj].item.Id >= 0 && MouseData.interfaceMouseIsOver.name == "Top Rack Panel" || MouseData.interfaceMouseIsOver.name == "Bottom Rack Panel")
            {            

                OnRemoved?.Invoke(slotsOnInterface[obj]);                

                slotsOnInterface[obj].RemoveItem();
            }
            else
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
                // only will start to swap if there is an item in the inventory
                if (slotsOnInterface[obj].item.Id >= 0)
                {
                    inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
                }
            }        
            return;
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

//public static class ExtensionMethods
//{

//}