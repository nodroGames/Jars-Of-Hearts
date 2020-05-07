using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeartItem : MonoBehaviour/*, ISerializationCallbackReceiver*/
{
    public InventoryType item;

    public InterfaceLocations locationInterface;
    public HealthStateSystem healthSystem;

    public EnumValue healthy;
    public EnumValue quarterRot;
    public EnumValue halfRot;
    public EnumValue mush;
    
    public float currentRotRate;
    public float currentProductValue;

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

    private LocationTypes location;

    public LocationTypes Location
    {
        get { return location; }
        set
        {
            location = value;
            changeLocationType();
        }
    }

    private void changeLocationType()
    {
        locationInterface.SetRotRate(gameObject);
        locationInterface.SetValue(gameObject);
    }

    public ItemDatabaseObject database;

    //public void OnAfterDeserialize()
    //{

    //}

    //public void OnBeforeSerialize()
    //{
    //    GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    //}

    [SerializeField]
    private float rotBaseTime = 60.0f;

    public float currentRotTime;
    private float half = 0.50f;
    private float quarter = 0.75f;
    public object heartStateType;

    public Animator anim;

    private void Awake()
    {
        currentRotTime = rotBaseTime;
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (locationInterface != null)
            Location = locationInterface.defaultLocationType;

        if (healthSystem != null)
            HealthState = healthSystem.defaultHealthStateType;
    }

    private void Update()
    {
        rotOverTime();
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
            anim.enabled = false;
            ReadData();


        }
        else if (currentRotTime <= rotBaseTime * half)
        {
            HealthState = halfRot;
            item = (InventoryType)heartStateType;
            ReadData();

        }
        else if (currentRotTime <= rotBaseTime * quarter)
        {
            HealthState = quarterRot;
            item = (InventoryType)heartStateType;
            ReadData();

        }
        else
        {
            HealthState = healthy;
            item = (InventoryType)heartStateType;
            ReadData();
        }
    }
    

    public void ReadData()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    }
}
