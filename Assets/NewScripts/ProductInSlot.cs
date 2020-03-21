using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInSlot : MonoBehaviour
{
    [SerializeField]
    private InventoryType item;
    
    public HealthStateSystem healthSystem;

    public EnumValue healthy;
    public EnumValue quarterRot;
    public EnumValue halfRot;
    public EnumValue mush;

    
    public LocationTypes floor;
    public LocationTypes inventory;
    public LocationTypes fridge;
    public LocationTypes oven;
    public LocationTypes jar;
       
    public float currentRotRate;
    //public float currentProductValue;

    private EnumValue healthState;

    public EnumValue HealthState
    {
        get { return healthState; }
        set
        {
            healthState = value;
            changeHealthState();
        }
    }

    private void changeHealthState()
    {
        healthSystem.SetHeartState(gameObject);
    }

    [SerializeField]
    private float rotBaseTime = 60.0f;

    [SerializeField]
    private LocationTypes location;

    public float currentRotTime;
    private float half = 0.50f;
    private float quarter = 0.75f;
    public object heartStateType;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rotOverTime();
    }

    public void SetHeartUI(float _currentSOProductRate, LocationTypes _location)
    {
        currentRotTime = _currentSOProductRate;
        location = _location;

        if (_location != null )
        {
            if (_location == floor)
            {
                currentRotRate = 1.5f;
            }
            if (_location == inventory)
            {
                currentRotRate = .5f;
            }
            if (_location == fridge)
            {
                currentRotRate = .25f;
            }
        }

    }

    private void rotOverTime()
    {
        if (currentRotTime >= 0.00f)
        {
            currentRotTime -= Time.deltaTime * currentRotRate;
        }

        if (currentRotTime <= rotBaseTime * 0)
        {
            HealthState = mush;
            item = (InventoryType)heartStateType;
            //ReadData();


        }
        else if (currentRotTime <= rotBaseTime * half)
        {
            HealthState = halfRot;
            item = (InventoryType)heartStateType;
            //ReadData();

        }
        else if (currentRotTime <= rotBaseTime * quarter)
        {
            HealthState = quarterRot;
            item = (InventoryType)heartStateType;
            //ReadData();

        }
        else
        {
            HealthState = healthy;
            item = (InventoryType)heartStateType;
            //ReadData();
        }
    }


    public void ReadData()
    {
       // GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    }


}
