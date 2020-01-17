using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class CounterQueue : MonoBehaviour
    {
        public int IntervalBetweenCustomerMovingToQueue;

        // This was to be fast, not the best way to do it
        // Talk to Hugo about doing this better

        public GameObject firstPositionQueue;
        public GameObject queue02;
        public GameObject queue03;
        public GameObject queue04;
        public GameObject entranceQueue;

        public List<NewPoint> ListOfPoints { get; set; }
        public List<NewCustomer> ListOfCustomersInQ { get; set; }
        
        private PauseDialogue pause;

        public GameObject storeGameObject;

        private StoreFront store;

        public GameObject dialog;

        // 3. Each (as defind in GameStarter script) seconds 1 customer idle at a random point moves to the counter queue if there is a free space.
        // Start is called every time the script is enabled and before the first frame update
        // Construct the list of Queue points
        void Start()
        {
            ListOfPoints = new List<NewPoint>();
            ListOfCustomersInQ = new List<NewCustomer>();

            // because the point was not made the best way we need to initialize the points this way
            ListOfPoints.Add(new NewPoint(firstPositionQueue));
            ListOfPoints.Add(new NewPoint(queue02));
            ListOfPoints.Add(new NewPoint(queue03));
            ListOfPoints.Add(new NewPoint(queue04));
            ListOfPoints.Add(new NewPoint(entranceQueue));

            pause = dialog.GetComponentInChildren<PauseDialogue>();
            store = storeGameObject.GetComponentInChildren<StoreFront>();
        }


        void Update()
        {
            CustomerMovement();
        }

        public void AddCustomer(NewCustomer customer)
        {
            ListOfCustomersInQ.Add(customer);
            customer.MoveTo(ListOfPoints[ListOfCustomersInQ.IndexOf(customer)]);
        }
                
        //If Mr. Todd invites the customer to tour the kitchen this will trigger
        public void SendFirstInQueueToKitchenDoor()
        {
            if (ListOfCustomersInQ.Count > 0)
            {
                if (!Kitchen.customerIsOnTheTour)
                {
                    Kitchen.customerIsOnTheTour = true;
                    NewCustomer customer = ListOfCustomersInQ[0];
                    ListOfCustomersInQ.Remove(customer);
                    RelocateAllCustomers();
                    store.AddToKitchenDoor(customer);
                    pause.Nevermind();
                }
                else
                {
                    Debug.Log("There is already someone touring the kitchen");
                }
            }
            else
            {
                Debug.Log("There isn't any customers in the queue");
            }
        }

        public void CustomerMovement()
        {
            foreach (NewCustomer c in ListOfCustomersInQ)
            {
                c.Move();
            }
        }

        private void RelocateAllCustomers()
        {
            for(int i = 0; i < ListOfCustomersInQ.Count; i++)
            {
                ListOfCustomersInQ[i].MoveTo(ListOfPoints[i]);
            }
        }
    }
}