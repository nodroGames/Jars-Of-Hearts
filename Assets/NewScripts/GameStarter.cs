using Assets.NewScripts;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public int numberOfCustomers;
    public int intervalBetweenCustomerEnteringShop;
    public GameObject storeGameObject;
    public GameObject customerPrefab;

    public List<NewCustomer> ListOfCustomersOutside { get; set; }

    private StoreFront storeFront;

    private NewCustomer customerGo;


    // Start is called every time the script is enabled and before the first frame update
    void Start()
    {
        // Initialize outside list
        ListOfCustomersOutside = new List<NewCustomer>();

        // 1. Create x (14 in my case) Customers 
        // (Can change ammount in Unity inspector since this is a MonoBehaviour script and the GameObject is public)
        for (int i = 0; i < numberOfCustomers; i++)
        {
            var newCustomer = Instantiate(customerPrefab);
           
            var customerScript = newCustomer.GetComponent<NewCustomer>();
            ListOfCustomersOutside.Add(customerScript);
                                                  
            for (int n = 0; n < ListOfCustomersOutside.Count; n++)
            {
                ListOfCustomersOutside[n].name = "Customer #" + n.ToString();
            }

            if (OnCustomerSpawnedListeners != null)
            {
                OnCustomerSpawnedListeners(newCustomer.GetComponent<NewCustomer>());
            }

            newCustomer.SetActive(false);
        }

        // 2. Every x (3 seconds in my case) seconds 1 customer enters the store, moves to a pre defined free random point and stops
        // (Can change ammount in Unity inspector since this is a MonoBehaviour script and the GameObject is public)
        // Need a reference to the storefront.
        storeFront = storeGameObject.GetComponentInChildren<StoreFront>();
        InvokeRepeating("SendCustomerToStoreFront", intervalBetweenCustomerEnteringShop, intervalBetweenCustomerEnteringShop);
    }

    public delegate void OnCustomerSpawnedDelegate(NewCustomer health);
    public event OnCustomerSpawnedDelegate OnCustomerSpawnedListeners;

    // 2. Need to create the method to send a customer from the outside list to the storefront.
    private void SendCustomerToStoreFront()
    {
        if(ListOfCustomersOutside.Count > 0)
        {
            NewCustomer newCustomer = ListOfCustomersOutside[0];
            ListOfCustomersOutside.Remove(newCustomer);

            storeFront.AddCustomer(newCustomer);
        }
    }

    public void AddCustomer(NewCustomer customer)
    {
       ListOfCustomersOutside.Add(customer);
    }

}
