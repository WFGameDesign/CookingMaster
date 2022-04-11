using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    //array for customers
    [SerializeField] private CustomerTable[] customers;

    //time between customers
    [SerializeField] private float timeBetweenCustomers;

    // Start is called before the first frame update
    void Start()
    {
        clearCustomers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clearCustomers()
    {
        for(int i = 0; i < customers.Length; i++)
        {
            customers[i].hideCustomer();
        }
    }

    public void addOrder()
    {
        for(int i = 0; i < customers.Length; i++)
        {
            if(!customers[i].customerIsWaiting())
            {
                customers[i].customerArrive();
                StartCoroutine(customerBreak());
                return;
            }
        }

        StartCoroutine(customerBreak());
    }

    IEnumerator customerBreak()
    {
        yield return new WaitForSeconds(timeBetweenCustomers);

        addOrder();
    }

}
