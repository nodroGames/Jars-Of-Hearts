using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCanImageHandler : MonoBehaviour
{
    [SerializeField]
    private UserInterface InventoryInterface = default;
    [SerializeField]
    private UserInterface FridgeInterface = default;
    [SerializeField]
    private UserInterface OvenInterface = default;


    // Start is called before the first frame update
    void Start()
    {
        InventoryInterface.OnRemoved += ChangeImageColor;
        FridgeInterface.OnRemoved += ChangeImageColor;
        OvenInterface.OnRemoved += ChangeImageColor;
    }

    private void ChangeImageColor()
    {
       GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void OnDestroy()
    {
        InventoryInterface.OnRemoved -= ChangeImageColor;
        FridgeInterface.OnRemoved -= ChangeImageColor;
        OvenInterface.OnRemoved -= ChangeImageColor;
    }
}
