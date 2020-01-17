using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class StoreFront : MonoBehaviour
    {
        // This was to be fast, not the best way to do it
        // Talk to Hugo about doing this better
        public GameObject doorSpawnPoint;
        public GameObject movePoint01;
        public GameObject movePoint02;
        public GameObject movePoint03;
        public GameObject movePoint04;
        public GameObject movePoint05;
        public GameObject movePoint06;
        public GameObject movePoint07;
        public GameObject movePoint08;
        public GameObject movePoint09;
        public GameObject movePoint10;

        public GameObject kitchenDoorMoveTo;

        public GameObject kitchenDoorPoint;

        public GameObject kitchenGameObject;

        public GameObject counterGameObject;
        private CounterQueue counterQueue;

        public List<NewPoint> ListOfMovePoints { get; set; }
        public List<NewCustomer> ListOfCustomersWandering { get; set; }

        public List<NewPoint> SingleKitchenDoorPoint { get; set; }
        public List<NewCustomer> CustomerEnteringKitchen { get; set; }

        private NewPoint startingPoint;

        private NewPoint kitchenInsideDoorPoint;

        private Kitchen kitchen;

        //private KitchenEnteranceCheck kitchenCheck;

        //private float waitTime { get; set; }
        //public float startWaitTime;

        // 2. Each (as defind in GameStarter script) seconds 1 customer enters the store, moves to a pre defined free random point and stops
        // need to control points
        // Start is called every time the script is enabled and before the first frame update
        // Construct the list of move points
        void Start()
        {
            // waitTime = startWaitTime;

            ListOfMovePoints = new List<NewPoint>();
            ListOfCustomersWandering = new List<NewCustomer>();

            SingleKitchenDoorPoint = new List<NewPoint>();
            CustomerEnteringKitchen = new List<NewCustomer>();

            startingPoint = new NewPoint(doorSpawnPoint);

            // because the point was not made the best way we need to initialize the points this way
            ListOfMovePoints.Add(new NewPoint(movePoint01));
            ListOfMovePoints.Add(new NewPoint(movePoint02));
            ListOfMovePoints.Add(new NewPoint(movePoint03));
            ListOfMovePoints.Add(new NewPoint(movePoint04));
            ListOfMovePoints.Add(new NewPoint(movePoint05));
            ListOfMovePoints.Add(new NewPoint(movePoint06));
            ListOfMovePoints.Add(new NewPoint(movePoint07));
            ListOfMovePoints.Add(new NewPoint(movePoint08));
            ListOfMovePoints.Add(new NewPoint(movePoint09));
            ListOfMovePoints.Add(new NewPoint(movePoint10));

            SingleKitchenDoorPoint.Add(new NewPoint(kitchenDoorPoint));

            kitchen = kitchenGameObject.GetComponentInChildren<Kitchen>();

            counterQueue = counterGameObject.GetComponentInChildren<CounterQueue>();

            // Check to see is CounterQueue can add a new customer
            InvokeRepeating("SendIdleCustomerToQueue", counterQueue.IntervalBetweenCustomerMovingToQueue, counterQueue.IntervalBetweenCustomerMovingToQueue);

        }

        // Update is called once per frame
        void Update()
        {
            foreach (NewCustomer c in ListOfCustomersWandering)
            {
                if (c.IsMoving)
                {
                    c.Move();
                }
                else
                {
                    NewPoint oldTarget = c.Target;

                    // infinante cycle if you have >= customers wandering to movepoints
                    bool freePointFound = false;

                    while (!freePointFound)
                    {
                        NewPoint point = ListOfMovePoints[Random.Range(0, 10)];                        
                        if (point.Free)
                        {
                            c.MoveTo(point);
                            point.Free = false;
                            freePointFound = true;
                        }                            
                    }
                    oldTarget.Free = true;
                }                  
            }
            // move customer from first in line to the kitchen door
            foreach (NewCustomer cek in CustomerEnteringKitchen)
            {
                if (cek.IsMoving)
                {
                    cek.Move();
                }
                else
                {
                    MoveInsideKitchen();
                    break;
                }
            }
        }

        // 2. Each x seconds (as defind in GameStarter script) 1 customer enters the store, moves to a pre defined free random point and stops
        public void AddCustomer(NewCustomer customer)
        {
            ListOfCustomersWandering.Add(customer);

            bool freePointFound = false;


            while (!freePointFound)
            {
                NewPoint point = ListOfMovePoints[Random.Range(0, 10)];

                if (point.Free)
                {
                    customer.PlaceAtPoint(startingPoint);
                    customer.MoveTo(point);
                    point.Free = false;
                    freePointFound = true;                    
                }
            }
        }

        public void SendIdleCustomerToQueue()
        {
            if (counterQueue.ListOfCustomersInQ.Count < counterQueue.ListOfPoints.Count && ListOfCustomersWandering.Count > 0)
            {
                NewCustomer customer = ListOfCustomersWandering[0];
                ListOfCustomersWandering.Remove(customer);
                customer.Target.Free = true;
                counterQueue.AddCustomer(customer);
            }            
        }
               
        public void AddToKitchenDoor(NewCustomer customer)
        {
            CustomerEnteringKitchen.Add(customer);
            NewPoint point = SingleKitchenDoorPoint[0];
            customer.MoveTo(point);
        }

        public void MoveInsideKitchen()
        {
            NewCustomer customer = CustomerEnteringKitchen[0];
            CustomerEnteringKitchen.Remove(customer);
            // add customer to kitchen list
            kitchen.AddCustomer(customer);
        }
    }
}
