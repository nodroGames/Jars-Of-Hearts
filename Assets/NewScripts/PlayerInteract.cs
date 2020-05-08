using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class PlayerInteract : MonoBehaviour
    {

        public GameObject currentInterObj = null;
        //public GameObject heart;
        [SerializeField]
        private InteractionObject currentInterObjScript = null;
        [SerializeField]
        private Inventory inventory;
        public PauseDialogue pauseDialogue;

       // public FTut fTutScript;

        private void Awake()
        {
            inventory = GetComponent<Inventory>();
            //if (inventory == null)
            //{
            //    Debug.LogError("Can't find inventory");
            //}
            //if (inventory != null)
            //{
            //    Debug.LogError("The Inventory is in game");
            //}
        }

        void Update()
        {
            Interaction();
        }

        private void Interaction()
        { 
            if (Input.GetButtonDown("Interact") && currentInterObj != null)
            {
                //Check to see if this object is to be stored in inventory/which inventory
                if (currentInterObjScript.heartInventory)
                {

                   // currentInterObj.GetComponent<RealHeartScript>().HeartInInventory(inventory);
                    //fTutScript.DestroyFIcon();
                    inventory.AddItemHeart(currentInterObj);
                }
                else if (currentInterObjScript.cookieInventory)
                {
                    inventory.AddItemCookie(currentInterObj);
                }
                //Check to see if this object talks and has a message
                else if (currentInterObjScript.talks)
                {
                    pauseDialogue.Pause();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("InterObject") || other.gameObject.CompareTag("WelcomeMat"))
            {
                currentInterObj = other.gameObject;
                currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();

            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("InterObject") || other.gameObject.CompareTag("WelcomeMat"))
            {
                if (other.gameObject == currentInterObj)
                {
                    currentInterObj = null;
                    currentInterObjScript = null;
                }
            }
        }
        
    }
}
