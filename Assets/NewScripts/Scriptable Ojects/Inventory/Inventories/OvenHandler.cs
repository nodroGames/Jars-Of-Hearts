using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenHandler : MonoBehaviour
{
    [SerializeField]
    private UserInterface InventoryInterface = default;
    [SerializeField]
    private UserInterface FridgeInterface = default;

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

    private void ReceiveData(Item _item, int _amount, LocationTypes _location, float _currentRotTime, float _currentRotRate)
    {
        item = _item;
        amount = _amount;
        location = _location;
        currentRotTime = _currentRotTime;
        currentRotRate = _currentRotTime;

        Debug.Log(item.Id);

    }

    public void OnDestroy()
    {
        InventoryInterface.OnRemoved -= ReceiveData;
        FridgeInterface.OnRemoved -= ReceiveData;
    }
}
