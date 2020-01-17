using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class UIHeartControl : MonoBehaviour
    {
        [SerializeField]
        private float rotBaseTime = 60.0f;
        private float uiHalf = 0.50f;
        private float uiQuarter = 0.25f;

        public Sprite UIHalfHeart;
        public Sprite UIQuarterHeart;
        public Sprite UIHealthyHeart;

        public float uiCurrentRotTime;

        private void Update()
        {
            rotOverTime();
            changeHeartState();
        }

        public enum UILocationType
        {
            Floor,
            Inventory,
            Fridge
        }

        public UILocationType uiLocationType;

        public enum UIHeartStateType
        {
           Healthy,
           HalfRot,
           QuarterRot,
           Mush
        }

        public UIHeartStateType uIHeartStateType;

        public void SetHeartUI(RealHeartScript.LocationType locationType, RealHeartScript.HeartStateType heartStateType, float currentRotTime)
        {
            //Debug.Log(this.name + this.transform.GetChild(0).gameObject.name);
            uIHeartStateType = (UIHeartStateType)heartStateType;
            uiLocationType = (UILocationType)locationType;
            uiCurrentRotTime = currentRotTime;
            //Debug.LogError(uiCurrentRotTime);
            //uIHeartControlLocationType = locationType;
        }

        private void rotOverTime()
        {
            if (uiCurrentRotTime >= 0.00f)
            {
                switch (uiLocationType)
                {
                    case UILocationType.Floor:
                        uiCurrentRotTime -= Time.deltaTime * 1.5f;
                        break;
                    case UILocationType.Inventory:
                        uiCurrentRotTime -= Time.deltaTime * 0.5f;
                        break;
                    case UILocationType.Fridge:
                        print("Fridge");
                        uiCurrentRotTime -= Time.deltaTime;
                        break;
                }
            }
        }

        private void changeHeartState()
        {
            if (uiCurrentRotTime <= rotBaseTime * 0)
            {
                uIHeartStateType = UIHeartStateType.Mush;
            }
            else if (uiCurrentRotTime <= rotBaseTime * uiQuarter)
            {
                this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = UIQuarterHeart;
                uIHeartStateType = UIHeartStateType.QuarterRot;
                //heartItemUI = quarterRotHeartIconUI;
            }
            else if (uiCurrentRotTime <= rotBaseTime * uiHalf)
            {
                this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = UIHalfHeart;
                uIHeartStateType = UIHeartStateType.HalfRot;
                //heartItemUI = halfRotHeartIconUI;
            }
            else
            {
                this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = UIHealthyHeart;
                uIHeartStateType = UIHeartStateType.Healthy;
                //heartItemUI = healthyHeartIconUI;
                Debug.Log("We Have A Healthy Heart");
            }
        }
    }
}
