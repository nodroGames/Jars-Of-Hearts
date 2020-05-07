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
            var setTopRack = ovenRackTopCookiePanel.GetComponent<ConvertHeartToCookie>();
            setTopRack.ConvertToCorrectCookie(item, amount, location, currentRotTime, currentRotRate);
        }
        if (MouseData.interfaceMouseIsOver.name == "Bottom Rack Panel")
        {
            ovenRackBottomHeartPanel.SetActive(false);
            var setBottomRack = ovenRackBottomCookiePanel.GetComponent<ConvertHeartToCookie>();
            setBottomRack.ConvertToCorrectCookie(item, amount, location, currentRotTime, currentRotRate);
        }
    }

    public void OnDestroy()
    {
        InventoryInterface.OnRemoved -= ReceiveData;
        FridgeInterface.OnRemoved -= ReceiveData;
    }
}
