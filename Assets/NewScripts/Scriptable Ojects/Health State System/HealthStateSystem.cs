using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStateSystem", menuName = "Scriptable Objects/Systems/Health State System")]
public class HealthStateSystem : ScriptableObject
{
    public List<HealthStateDetails> details;

    public EnumValue defaultHealthStateType;

    public void SetHeartState(GameObject obj)
    {
        HeartItem product = obj.GetComponent<HeartItem>();
        if (null == product) return;

        var newHeartState = details.Find(l => l.type == product.HealthState);
        if (null == newHeartState) return;


        product.heartStateType = newHeartState.heartProductSO;
    }

    //public void SetValue(GameObject obj)
    //{
    //    HeartItem product = obj.GetComponent<HeartItem>();
    //    if (null == product) return;

    //    var oldProductValue = product.currentProductValue;
    //    //if (null == oldProductValue) return;

    //    var newLocation = details.Find(l => l.type == product.Location);
    //    if (null == newLocation) return;


    //    //oldProductValue = newLocation.value;
    //    product.currentProductValue = newLocation.value;
    //}
}

[Serializable]
public class HealthStateDetails
{
    public EnumValue type;
    public InventoryType heartProductSO;
    public InventoryType cookieProductSO;
    //public int value;
}
