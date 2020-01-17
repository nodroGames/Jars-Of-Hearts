using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class Portal : MonoBehaviour
    {
        private Transform destination;

        public Transform newDestination;

        public bool isStore;
        
        private void Start()
        {
            if (isStore == false)
            {
                destination = GameObject.FindGameObjectWithTag("StorePortal").GetComponent<Transform>();
            }
            else
            {
                destination = GameObject.FindGameObjectWithTag("KitchenPortal").GetComponent<Transform>();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Customer") )
            {
                other.transform.position = new Vector2(newDestination.position.x, newDestination.position.y);
            }
        }
    }
}
