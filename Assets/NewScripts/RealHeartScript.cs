using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class RealHeartScript : MonoBehaviour
    {
        [SerializeField]
        private float rotBaseTime = 60.0f;
        [SerializeField]
        private float currentRotTime;
        private float half = 0.50f;
        private float quarter = 0.25f;

        public Sprite HalfHeart;
        public Sprite QuarterHeart;

        [SerializeField]
        private UIHeartControl uiHeartControlScript = null;

        [SerializeField]
        private GameObject heartItemUI;
        //[SerializeField]
        //private GameObject instructionalF;
        //[SerializeField]
        //private Transform instructionLocation;

        public GameObject healthyHeartIconUI;
        public GameObject halfRotHeartIconUI;
        public GameObject quarterRotHeartIconUI;

        private Transform heartPool;

        public enum LocationType
        {
            Floor,
            Inventory,
            Fridge
        }

        public LocationType locationType;

        public enum HeartStateType
        {
            Healthy,
            HalfRot,
            QuarterRot,
            Mush
        }
        
        public HeartStateType heartStateType;
               
        private void Start()
        {
            heartPool = GameObject.FindGameObjectWithTag("HeartPool").GetComponent<Transform>();
            //GameObject iLGO = Instantiate(instructionalF, transform.position, instructionalF.transform.rotation);
            //iLGO.transform.SetParent(instructionLocation);
            currentRotTime = rotBaseTime;
            locationType = LocationType.Floor;
            heartStateType = HeartStateType.Healthy;
        }

        // Update is called once per frame
        private void Update()
        {
            rotOverTime();
            changeHeartState();
        }

        private void rotOverTime()
        {
            if (currentRotTime >= 0.00f)
            {
                switch (locationType)
                {
                    case LocationType.Floor:
                        currentRotTime -= Time.deltaTime * 1.5f;
                        break;
                    case LocationType.Inventory:
                        currentRotTime -= Time.deltaTime * 0.5f;
                        break;
                    case LocationType.Fridge:
                        print("Fridge");
                        currentRotTime -= Time.deltaTime;
                        break;
                }
            }                
        }

        private void changeHeartState()
        {
            if (currentRotTime <= rotBaseTime * 0)
            {
                heartStateType = HeartStateType.Mush;
            }
            else if (currentRotTime <= rotBaseTime * quarter)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = QuarterHeart;
                heartStateType = HeartStateType.QuarterRot;
                heartItemUI = quarterRotHeartIconUI;
            }
            else if (currentRotTime <= rotBaseTime * half)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HalfHeart;
                heartStateType = HeartStateType.HalfRot;
                heartItemUI = halfRotHeartIconUI;
            }
            else
            {
                heartItemUI = healthyHeartIconUI;
                //Debug.Log("We Have A Healthy Heart");
            }
        }

        public void HeartInInventory(Inventory inventory)
        {
            for (int i = 0; i < inventory.heartSlots.Length; i++)
            {
                if (inventory.heartIsFull[i] == false)
                {                    
                    Instantiate(heartItemUI, inventory.heartSlots[i].transform, false);
                    inventory.heartSlots[i].GetComponent<SpriteRenderer>().sprite = null;
                    uiHeartControlScript = inventory.heartSlots[i].GetComponent<UIHeartControl>();
                    locationType = LocationType.Inventory;
                    uiHeartControlScript.SetHeartUI(locationType, heartStateType, currentRotTime); 
                    inventory.heartIsFull[i] = true;                    
                    this.gameObject.transform.position = new Vector2(heartPool.position.x, heartPool.position.y);
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
