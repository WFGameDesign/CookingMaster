using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingBoard : ItemHolderBase
{

    //reference to player currently using cutting board
    private CharacterMovement activePlayerMov;

    //values for timer to cut veggies
    [SerializeField] private float chopTimerMax;
    private float activeChopTimer = 0f;
    private bool isTimerActive;

    //reference for any placed choppedVegetables
    [SerializeField]private ChoppedVegetable chopRef;

    [SerializeField] private Image progressFill;
    [SerializeField] private GameObject fillBarRoot;

    //things on plate till be added to chopped vegetable mix
    [SerializeField] private Plate plateRef;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeChopTimer > 0 && isTimerActive)
        {
            activeChopTimer -= Time.deltaTime;

            //update progress bar
            progressFill.fillAmount = activeChopTimer / chopTimerMax;

            //check if timer has run out
            if (activeChopTimer <= 0)
            {
                isTimerActive = false;
                //place behavior for when player is done chopping here
                activePlayerMov.unlockPlayerMovement();

                //hider progress bar
                hideProgressBar();

                //clear referenc to CharacterMovment Component
                activePlayerMov = null;


                //check if placed object is a vegetable and that there is no chopped vegetables 
                //already on the cuttoing board
                if (vegetableCheck() && chopRef == null)
                {
                    //instantiate chopped vegetable object
                    ItemBase instHolder = Instantiate(Spawnable, placedItemAnchor.position, Quaternion.identity);

                    //get reference to ChoppedVegetable and to currently held vegetable
                    ChoppedVegetable cHolder = instHolder.gameObject.GetComponent<ChoppedVegetable>();
                    Vegetable vHolder = placedItem.gameObject.GetComponent<Vegetable>();

                    if(cHolder != null && vHolder != null)
                    {

                        //add existing vegetable to the chopped vegetable mix;
                        //delete vegetable then attack choppedVegetable to chopping board
                        cHolder.addVegToMix(vHolder);
                        placeItem(cHolder);

                        //move vegetable to its own designated reference holder and clear placed
                        //item variable
                        chopRef = cHolder;
                        placedItem = null;
                    }


                }else if(chopRef != null)
                {
                    chopRef.addVegToMix(placedItem.gameObject);
                }
            }
        }
    }

    //access to the player's CharacteMovement component
    override public void placeItem(ItemBase newItem, CharacterMovement charMov)
    {
        ChoppedVegetable cVChecker = newItem.gameObject.GetComponent<ChoppedVegetable>();

        
        //virtual method still used with newItem parameter
        base.placeItem(newItem);

        //if the object isnt chopped vegetables then chop them as normal
        if (cVChecker == null)
        {
            activePlayerMov = charMov;

            activePlayerMov.lockPlayerMovement();

            startChopTimer();
        }
        else if(chopRef == null)
        {
            chopRef = cVChecker;
            placedItem = null;
        }
        else
        {
            //otherwise combine ingredients
            chopRef.addVegToMix(cVChecker);
        }
        
    }

    private void startChopTimer()
    {
        //set timer and start countdown
        isTimerActive = true;
        activeChopTimer = chopTimerMax;

        //make progress bar visible
        showProgressBar();
    }

    // check if placed item is a vegetable
    private bool vegetableCheck()
    {
        Vegetable vHolder = placedItem.GetComponent<Vegetable>();

        bool isVeggie = false;

        if(vHolder != null)
        {
            isVeggie = true;
        }
        return isVeggie;
    }

    private bool choppedVegetableCheck(ItemBase itemToCheck)
    {
        bool isChopVeggies = false;

        ChoppedVegetable cVHolder = itemToCheck.gameObject.GetComponent<ChoppedVegetable>();

        if(cVHolder != null)
        {
            isChopVeggies = true;
        }

        return isChopVeggies;
    }

    public void clearChopRef()
    {
        chopRef = null;
    }

    private void hideProgressBar()
    {
        fillBarRoot.SetActive(false);
    }

    private void showProgressBar()
    {
        fillBarRoot.SetActive(true);
    }

    public void grabFromPlate()
    {
        if(plateRef == null)
        {
            return;
        }

        ItemBase newItemHolder = plateRef.giveItem();

        if (newItemHolder != null)
        {
            newItemHolder.removeSelfFromHolder();

            chopRef.addVegToMix(newItemHolder.gameObject);
        }
    }
}
