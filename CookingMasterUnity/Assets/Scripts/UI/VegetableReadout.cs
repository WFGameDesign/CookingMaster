using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegetableReadout : MonoBehaviour
{

    //images array of sprite icons
    //0 = Lettuce
    //1 = Carrot
    //2 = Tomato
    //3 = Onion
    //4 = Radish
    //5 = Asparagus
    //index values are matched up to values in Vegetable "veggieIndex" variable
    [SerializeField] private GameObject[] veggieSpriteArr = new GameObject[6];

    //array of text objects for showing ingredient counts
    //index values are matched up to values in Vegetable "veggieIndex" variable
    [SerializeField] private Text[] vegCountTextArr = new Text[6];

    //height about taget where ui should float
    [SerializeField] private float hoverHeight;

    //camera reference  for active camera so readout can turn towards it
    private Transform mainCameraRef;

    // Start is called before the first frame update
    void Awake()
    {
        clearReadout();

        mainCameraRef = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //set position to hover over target
        transform.position = transform.parent.position + new Vector3(0f, hoverHeight, 0f);

        //turn towards camera
        transform.LookAt(mainCameraRef.position);
    }

    //takes array input and sets readout to show which ingredients are present and in what amounts
    public void setReadout(int[] vegArr)
    {
        //if vegArr is longer of shorter than the number of possible vegetables give an error
        //then return
        if(vegArr.Length > 6 || vegArr.Length <6)
        {
            print("ERROR: input array too large or too small length must be 6 or less");
            return;
        }

        //reset readout so only data indicated by vegArr is visible
        clearReadout();

        for(int i = 0; i < vegArr.Length; i++)
        {
            if(vegArr[i] > 0)
            {
                //make vegetealbe icon visible
                veggieSpriteArr[i].SetActive(true);

                //make number readout under icon visible
                vegCountTextArr[i].gameObject.SetActive(true);

                //make number under vegetable read the amount required/present
                vegCountTextArr[i].text = vegArr[i].ToString();
            }
        }
            
        
    }

    //makes all readout elements invisible
    public void clearReadout()
    {
        for(int i = 0; i < veggieSpriteArr.Length; i++)
        {
            //make vegetealbe icon invisible
            veggieSpriteArr[i].SetActive(false);

            //make number readout under icon invisible
            vegCountTextArr[i].gameObject.SetActive(false);

        }
    }
}
