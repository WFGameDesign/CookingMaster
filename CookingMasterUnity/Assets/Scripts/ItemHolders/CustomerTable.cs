using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTable : ItemHolderBase
{
    //the type  of salad that the customer wants
    //array element denotes vegetablee type
    //array value denotes amount
    //0 = Lettuce
    //1 = Carrot
    //2 = Tomato
    //3 = Onion
    //4 = Radish
    //5 = Asparagus
    private int[] custOrder = new int[6];

    //maximum and minimum number of ingredients customer can order in their salad
    [SerializeField] private int maxVegNum;
    [SerializeField] private int minVegNum;

    //reference to the in game ui for what the customer wnats on their salad
    [SerializeField] private VegetableReadout orderReadout;

    //refernce to the customer boduy that the player sees
    [SerializeField] private GameObject customerBodyCoreRef;

    //timer variables
    private float activeTimer;
    private float maxTimer;
    [SerializeField] private float timePerIngredient;
    private bool timerIsActive;

    //fillbar reference
    [SerializeField] private GameObject fillBarRoot;
    [SerializeField] private Image timerFillBar;

    //Reference to game manager so info can be passed when players give correct or incorrect salads
    [SerializeField] private GameManager myManager;

    private bool isMad;
    private float timerRate = 1;
    [SerializeField] private float madRate;

    //reference to angry eyebrows
    [SerializeField] private GameObject eyebrows;

    //reference for point penalty
    private bool[] attemptedPlayers = new bool[2];

    private int noServePenalty = 5;
    private int wrongServePenalty = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsActive)
        {
            activeTimer -= Time.deltaTime * timerRate;

            //run timer ui here
            timerFillBar.fillAmount = activeTimer / maxTimer;

            //behavior for once timer runs out
            if(activeTimer <= 0)
            {
                timerIsActive = false;

                hideCustomer();

                //check were to deduct points
                pointPenalty();
                
            }
        }
    }

    public void generateOrder()
    {
        //local randomized variable that holds how many total ingredients wil be in customers order
        int totalVegNumToAdd = Random.Range(minVegNum, maxVegNum + 1);

        //set timer for how long the customer will wait
        activeTimer = totalVegNumToAdd * timePerIngredient;
        maxTimer = activeTimer;

        timerIsActive = true;

        //reset order before adding to it
        resetOrder();

        //while there are still more ingredients to add continue adding ingredient amount to random vegetables
        while (totalVegNumToAdd > 0)
        {
            //get random amount up to the remainder of how many total ingredients will be in order
            int singleVegNumToAdd = Random.Range(1, totalVegNumToAdd + 1);           

            //add amount to random custOrder element
            custOrder[Random.Range(0, custOrder.Length)] = singleVegNumToAdd;

            //remove amount added from total amount to add
            totalVegNumToAdd -= singleVegNumToAdd;
            
        }

        //set readout to reflect the customers order
        orderReadout.setReadout(custOrder);
        
    }

    public override void placeItem(ItemBase newItem, CharacterMovement charMov)
    {
        //get characterinfo
        CharacterInfo cInfoHolder = charMov.gameObject.GetComponentInParent<CharacterInfo>();

        base.placeItem(newItem);

        //place item on holder then check if the order is correct
        if (checkOrder(placedItem))
        {
            //behavior if salad served was correct
            hideCustomer();

            timerIsActive = false;

            //award points
            cInfoHolder.addPlayerScore(10);

            if (cInfoHolder != null)
            {

                //award pickup if served before timer falls below 70%
                if ((activeTimer / maxTimer) >= .7)
                {
                    myManager.callSpawnPowerUp(cInfoHolder);
                }

                placedItem.destroyItem();
            }
        }
        else
        {
            //incorrect order behavior
            placedItem.destroyItem();

            isMad = true;
            timerRate = madRate;

            eyebrows.SetActive(true);

            attemptedPlayers[cInfoHolder.getPlayerIndex() - 1] = true;
        }
    }

    public override void placeItem(ItemBase newItem)
    {
        //get characterinfo
        CharacterInfo cInfoHolder = newItem.gameObject.GetComponentInParent<CharacterInfo>();

        base.placeItem(newItem);

        //place item on holder then check if the order is correct
        if (checkOrder(placedItem))
        {
            print("order correct!");
            hideCustomer();

            timerIsActive = false;
            
            if (cInfoHolder != null)
            {
                myManager.callSpawnPowerUp(cInfoHolder);
                placedItem.destroyItem();
            }
        }
        else
        {
            print("Order Incorrect");
            placedItem.destroyItem();
        }
        
    }

    private bool checkOrder(ItemBase salad)
    {
        //order is assumed to be true then false values are tested for
        bool orderCorrect = true;

        ChoppedVegetable cHolder = salad.GetComponent<ChoppedVegetable>();

        if(cHolder != null)
        {
            for(int i = 0; i < custOrder.Length; i++)
            {
                if(custOrder[i] != cHolder.getIngredientAmount(i))
                {
                    orderCorrect = false;
                }
            }
        }
        else
        {
            orderCorrect = false;
        }

        return orderCorrect;
    }

    //hide customer body and readout
    public void hideCustomer()
    {
        customerBodyCoreRef.SetActive(false);

        orderReadout.clearReadout();

        fillBarRoot.SetActive(false);

    }

    //show customer body
    public void customerArrive()
    {
        customerBodyCoreRef.SetActive(true);

        generateOrder();

        fillBarRoot.SetActive(true);
        isMad = false;
        timerRate = 1;

        eyebrows.SetActive(false);

        for(int i = 0; i < attemptedPlayers.Length; i++)
        {
            attemptedPlayers[i] = false;
        }
    }

    //show if customer has a waiting order
    public bool customerIsWaiting()
    {
        bool isWaiting = timerIsActive;

        return isWaiting;
    }

    //reset salad request
    private void resetOrder()
    {
        for(int i = 0; i < custOrder.Length; i++)
        {
            custOrder[i] = 0;
        }
    }

    private void pointPenalty()
    {
        bool anyAttempted = false;

        for(int i = 0; i < attemptedPlayers.Length; i++)
        {
            if(attemptedPlayers[i] == true)
            {
                myManager.addPointCall(i, -wrongServePenalty);
                anyAttempted = true;
            }
        }

        if(!anyAttempted)
        {
            myManager.addPointCall(0, -noServePenalty);
            myManager.addPointCall(1, -noServePenalty);
        }

    }
}
