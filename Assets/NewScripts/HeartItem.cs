using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeartItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public InventoryType item;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }

    /*public LocationTypes location;

    [SerializeField]
    private float rotBaseTime = 60.0f;
    [SerializeField]
    private float currentRotTime;
    private float half = 0.50f;
    private float quarter = 0.25f;

    public Sprite HalfHeart;
    public Sprite QuarterHeart;

    //[SerializeField]
    //private UIHeartControl uiHeartControlScript = null;

    [SerializeField]
    private GameObject heartItemUI;

    public GameObject healthyHeartIconUI;
    public GameObject halfRotHeartIconUI;
    public GameObject quarterRotHeartIconUI;

    private Transform heartPool;

    private void Awake()
    {
        heartPool = GameObject.FindGameObjectWithTag("HeartPool").GetComponent<Transform>();
        currentRotTime = rotBaseTime;
    }*/


}
