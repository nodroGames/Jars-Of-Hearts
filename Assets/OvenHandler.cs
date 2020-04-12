using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenHandler : MonoBehaviour
{
    [SerializeField]
    private UserInterface InventoryInterface = default;
    [SerializeField]
    private UserInterface FridgeInterface = default;
    [SerializeField]
    private GameObject ovenRackTopHeartPanel = default;
    [SerializeField]
    private GameObject ovenRackBottomHeartPanel = default;
    [SerializeField]
    private GameObject ovenRackTopCookiePanel = default;
    [SerializeField]
    private GameObject ovenRackBottomCookiePanel = default;
    [SerializeField]
    private StaticInterface ovenRackTopScript = default;
    [SerializeField]
    private InventoryType healthCookie;
    [SerializeField]
    private InventoryType quarterCookie;
    [SerializeField]
    private InventoryType halfCookie;

    private Item item;
    private int amount;
    private LocationTypes location;
    private float currentRotTime;
    private float currentRotRate;

    

    // Start is called before the first frame update
    void Start()
    {
        InventoryInterface.OnRemoved += ReceiveData;
        FridgeInterface.OnRemoved += ReceiveData;
    }

    private void ReceiveData(InventorySlot _slotsOnInterface)
    {
        item = _slotsOnInterface.item;
        amount = _slotsOnInterface.amount;
        location = _slotsOnInterface.location;

        if (MouseData.interfaceMouseIsOver.name == "Top Rack Panel")
        {
            ovenRackTopHeartPanel.SetActive(false);
            ovenRackTopCookiePanel.SetActive(true);
            foreach (GameObject cookieSlots in ovenRackTopScript.slots)
            {
                if (item.Id == 0)
                {
                    Instantiate(healthCookie, Vector3.zero, Quaternion.identity, cookieSlots.transform);
                }
            }
        }
        if (MouseData.interfaceMouseIsOver.name == "Bottom Rack Panel")
        {
            ovenRackBottomHeartPanel.SetActive(false);
            //ovenRackBottomCookiePanel.SetActive(true);
        }
    }

    public void OnDestroy()
    {
        InventoryInterface.OnRemoved -= ReceiveData;
        FridgeInterface.OnRemoved -= ReceiveData;
    }
}
