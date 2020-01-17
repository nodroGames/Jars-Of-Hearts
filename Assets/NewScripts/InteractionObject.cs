using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{

    public class InteractionObject : MonoBehaviour
    {
        public bool cookieInventory;      //If true this object can be stored in the Cookie inventory
        public bool heartInventory;      //If true this object can be stored in the Heart inventory
        public bool talks;                 //If true this object can talk to player        
        public bool isVisible;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {            
            isVisible = true;
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void DoInteraction()
        {
            //Picked up and put in inventory
            spriteRenderer.enabled = false;
            isVisible = false;
        }

    }
}
