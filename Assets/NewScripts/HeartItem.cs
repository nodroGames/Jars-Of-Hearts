﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeartItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public InventoryType item;
    public InventoryType[] statesOfProduct;
    public InterfaceLocations locationInterface;
    private LocationTypes location;

    public float currentRotRate;
    public float currentProductValue;

    public LocationTypes Location
    {
        get { return location; }
        set
        {
            location = value;
            ChangeLocationType();
        }
    }

    private void ChangeLocationType()
    {
        locationInterface.SetRotRate(gameObject);
        locationInterface.SetValue(gameObject);
    }

    public ItemDatabaseObject database;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
#if UNITY_EDITOR

        //EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }

    [SerializeField]
    private float rotBaseTime = 60.0f;
    [SerializeField]
    private float currentRotTime;
    private float half = 0.50f;
    private float quarter = 0.75f;
    private object heartStateType;

    private void Awake()
    {
        currentRotTime = rotBaseTime;
        item.uiDisplay = item.healthyHeartSprite;

        //location = item.location;
    }

    private void Start()
    {
        if (locationInterface != null)
            Location = locationInterface.defaultLocationType;
    }

    private void Update()
    {
        rotOverTime();
        changeHeartState();
    }

    private void rotOverTime()
    {
        if (currentRotTime >= 0.00f)
        {
            currentRotTime -= Time.deltaTime * currentRotRate;
        }
    }

    private void changeHeartState()
    {
        if (currentRotTime <= rotBaseTime * 0)
        {
            heartStateType = statesOfProduct[3];
            item = (InventoryType)heartStateType;
        }
        else if (currentRotTime <= rotBaseTime * half)
        {
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = QuarterHeart;
            heartStateType = statesOfProduct[2];
            item = (InventoryType)heartStateType;
            //heartItemUI = quarterRotHeartIconUI;
        }
        else if (currentRotTime <= rotBaseTime * quarter)
        {
            heartStateType = statesOfProduct[1];
            item = (InventoryType)heartStateType;
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = HalfHeart;
            // heartItemUI = halfRotHeartIconUI;
        }
        else
        {
            heartStateType = statesOfProduct[0];
            item = (InventoryType)heartStateType;
            //heartItemUI = healthyHeartIconUI;
            //Debug.Log("We Have A Healthy Heart");
        }
    }


}
