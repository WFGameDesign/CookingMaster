using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppedVegetable : ItemBase
{
    //place in array determines the type of vegetable
    //value of array element determines amount of that vegetable in mix
    //index of vegetable at array element values (i.e. veggieMixArr[2] returning 3 means that there are 3 tomatoes)
    //0 = Lettuce
    //1 = Carrot
    //2 = Tomato
    //3 = Onion
    //4 = Radish
    //5 = Asparagus
    //index values are matched up to values in Vegetable "veggieIndex" variable
    [SerializeField] private int[] veggieMixArr = new int[6]; 

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //parameter is reference to array element of veggieMixArr[]
    //returns amount of specified ingredient
    public int getIngredientAmount(int veggieIndex)
    {
        int amntHolder = 0;

        //array bounds check
        if (veggieIndex < veggieMixArr.Length)
        {
            amntHolder = veggieMixArr[veggieIndex];
        }

        //returns 0 on out of bounds or of veggie amount is 0
        return amntHolder;
    }

    public void addVegToMix(Vegetable newVeg)
    {
        //get index from vegetable
        int indexHolder = newVeg.getVegIndex();

        //increase vegetable counter of that type by 1
        veggieMixArr[indexHolder] = ++veggieMixArr[indexHolder];

        //destroy vegetable
        newVeg.destroyItem();
    }

    

    public void addVegToMix(ChoppedVegetable newChop)
    {
        for(int i = 0; i < veggieMixArr.Length ; i++)
        {
            //add any ingredients from input newChop to local veggieMixArr[]
            veggieMixArr[i] += newChop.getIngredientAmount(i);
        }

        //destroy newChop
        newChop.destroyItem();
    }

    public void addVegToMix(GameObject newThing)
    {
        Vegetable vegHolder = newThing.GetComponent<Vegetable>();
        ChoppedVegetable chopHolder = newThing.GetComponent<ChoppedVegetable>();

        if(vegHolder != null)
        {
            addVegToMix(vegHolder);
        }else if(chopHolder != null)
        {
            addVegToMix(chopHolder);
        }
        else
        {
            print("ERROR: GameObject passed to addVegToMix() that is neither vegetable nor ChopppedVegetable");
        }
    }

    public override void removeSelfFromHolder()
    {
        //dont look for cutting board if not placed on item holder
        if (iHolderRef != null)
        {
            CuttingBoard cBHolder = iHolderRef.gameObject.GetComponent<CuttingBoard>();

            if (cBHolder != null)
            {
                hasBeenPlaced(false);
                cBHolder.clearChopRef();
            }
        }

        base.removeSelfFromHolder();
    }
}
