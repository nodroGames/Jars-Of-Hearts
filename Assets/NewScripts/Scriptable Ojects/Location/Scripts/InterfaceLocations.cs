using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationTypes", menuName = "Scriptable Objects/Location System/Location Interface")]
public class InterfaceLocations : ScriptableObject
{
    public List<LocationDetails> details;

    public LocationTypes defaultLocationType;

    public void SetRotRate(GameObject obj)
    {
        HeartItem product = obj.GetComponent<HeartItem>();
        if (null == product) return;

        //var oldRotRate = product.currentRotRate;
        //if (null == oldRotRate) return;

        var newLocation = details.Find(l => l.type == product.Location);
        if (null == newLocation) return;

        //oldRotRate = newLocation.rotRate;
        product.currentRotRate = newLocation.rotRate;
    }

    public void SetValue(GameObject obj)
    {
        HeartItem product = obj.GetComponent<HeartItem>();
        if (null == product) return;

        var oldProductValue = product.currentProductValue;
        //if (null == oldProductValue) return;

        var newLocation = details.Find(l => l.type == product.Location);
        if (null == newLocation) return;


        //oldProductValue = newLocation.value;
        product.currentProductValue = newLocation.value;
    }
}

[System.Serializable]
public class LocationDetails
{
    public LocationTypes type;
    public float rotRate;
    public int value;
}
