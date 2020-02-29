using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeartItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public InventoryType item;
    //public LocationTypes location;

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
    [SerializeField]
    private float currentRotTime;
    private float half = 0.50f;
    private float quarter = 0.25f;

    [SerializeField]
    private float floorTime = 1.5f;
    [SerializeField]
    private float inventoryTime = 0.5f;
    [SerializeField]
    private float fridgeTime = 0.25f;

    private Transform heartPool;

    private void Awake()
    {
        heartPool = GameObject.FindGameObjectWithTag("HeartPool").GetComponent<Transform>();
        currentRotTime = rotBaseTime;
        //location = item.location;
    }

    private void Update()
    {
        rotOverTime();
        //changeHeartState();
    }

    private void rotOverTime()
    {
        if (currentRotTime >= 0.00f)
        {
            switch (item.location)
            {
                case InterfaceLocation.Floor:
                    currentRotTime -= Time.deltaTime * floorTime;
                    break;
                case InterfaceLocation.Inventory:
                    currentRotTime -= Time.deltaTime * inventoryTime;
                    break;
                case InterfaceLocation.Fridge:
                    currentRotTime -= Time.deltaTime * fridgeTime;
                    break;
                case InterfaceLocation.Oven:
                    print("Oven");
                    break;
                case InterfaceLocation.Jar:
                    print("Jar");
                    break;
            }
        }
    }


    /*

    

    public Sprite HalfHeart;
    public Sprite QuarterHeart;

    //[SerializeField]
    //private UIHeartControl uiHeartControlScript = null;

    [SerializeField]
    private GameObject heartItemUI;

    public GameObject healthyHeartIconUI;
    public GameObject halfRotHeartIconUI;
    public GameObject quarterRotHeartIconUI;

    */


}
