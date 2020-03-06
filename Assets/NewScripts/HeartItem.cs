using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeartItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public InventoryType item;
    public InventoryType[] statesOfProduct;
    public InterfaceLocations locationInterface;
    private LocationTypes location;
    private SpriteRenderer currentHeartImage;

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

#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }

    [SerializeField]
    private float rotBaseTime = 60.0f;

    public float currentRotTime;
    private float half = 0.50f;
    private float quarter = 0.75f;
    private object heartStateType;

    private void Awake()
    {
        currentRotTime = rotBaseTime;
        //currentHeartImage = this.sprit
        //item.uiDisplay = item.healthyHeartSprite;

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
            ReadData();


        }
        else if (currentRotTime <= rotBaseTime * half)
        {
            heartStateType = statesOfProduct[2];
            item = (InventoryType)heartStateType;
            ReadData();

        }
        else if (currentRotTime <= rotBaseTime * quarter)
        {
            heartStateType = statesOfProduct[1];
            item = (InventoryType)heartStateType;
            ReadData();

        }
        else
        {
            heartStateType = statesOfProduct[0];
            item = (InventoryType)heartStateType;
            ReadData();

            //Debug.Log("We Have A Healthy Heart");
        }
    }

    public void ReadData()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    }


}
