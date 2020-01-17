using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class Kitchen : MonoBehaviour
    {
        // This was to be fast, not the best way to do it
        // Talk to Hugo about doing this better
        public GameObject kitchenDoorSpawnPoint;
        public GameObject kitchenMovePoint01;
        public GameObject kitchenMovePoint02;
        public GameObject kitchenMovePoint03;
        public GameObject kitchenMovePoint04;
        public GameObject kitchenMovePoint05;
        public GameObject kitchenMovePoint06;
        public GameObject kitchenMovePoint07;
        public GameObject kitchenMovePoint08;
        public GameObject kitchenMovePoint09;
        public GameObject kitchenMovePoint10;

        public GameObject ovenGameObject;

        public float kitchenNoise;

        public List<NewPoint> ListOfKitchenMovePoints { get; set; }
        public List<NewCustomer> ListOfCustomersWandering { get; set; }

        public static bool customerIsOnTheTour;

        private NewPoint startingPoint;

        public GameObject gameStarterGameObject;

        private GameStarter gs;

        //private float waitTime { get; set; }
        //public float startWaitTime;

        // 2. Each (as defind in GameStarter script) seconds 1 customer enters the store, moves to a pre defined free random point and stops
        // need to control points
        // Start is called every time the script is enabled and before the first frame update
        // Construct the list of move points
        void Start()
        {
            CustomerDeathEvent.RegisterListener(OnCustomerDied);

            gs = gameStarterGameObject.GetComponentInChildren<GameStarter>();
            // waitTime = startWaitTime;

            ListOfKitchenMovePoints = new List<NewPoint>();
            ListOfCustomersWandering = new List<NewCustomer>();

            startingPoint = new NewPoint(kitchenDoorSpawnPoint);

            // because the point was not made the best way we need to initialize the points this way
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint01));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint02));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint03));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint04));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint05));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint06));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint07));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint08));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint09));
            ListOfKitchenMovePoints.Add(new NewPoint(kitchenMovePoint10));

            customerIsOnTheTour = false;

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
                        NewPoint point = ListOfKitchenMovePoints[Random.Range(0, 10)];                        
                        if (point.Free)
                        {
                            c.MoveTo(point);
                            point.Free = false;
                            freePointFound = true;
                        }                            
                    }
                    SuspicionBarScript.suspicion += kitchenNoise;
                    oldTarget.Free = true;
                }                  
            }
        }

        // 2. Each x seconds (as defind in GameStarter script) 1 customer enters the kitchen, moves to a pre defined free random point and stops
        public void AddCustomer(NewCustomer customer)
        {
            ListOfCustomersWandering.Add(customer);

            bool freePointFound = false;

            while (!freePointFound)
            {
                NewPoint point = ListOfKitchenMovePoints[Random.Range(0, 10)];

                if (point.Free)
                {
                    customer.MoveTo(point);
                    point.Free = false;
                    freePointFound = true;
                }
            }
        }

        public void OnCustomerDied(CustomerDeathEvent customerDeath)
        {
            //Debug.Log("customersWandering size: " + ListOfCustomersWandering.Count);
            //NewCustomer customer = ListOfCustomersWandering[0];
            NewCustomer customer = customerDeath.DeadCustomerGO.GetComponent<NewCustomer>();
            ListOfCustomersWandering.Remove(customer);

            //add customer to OutsidePool list
            gs.AddCustomer(customer);
        }
    }
}
