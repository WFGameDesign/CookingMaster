using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    //items held and in pocket will be placed at these Transform positions then parented to them
    [SerializeField] private Transform heldItemAnchor;
    [SerializeField] private Transform pocketItemAnchor;

    //references to whatever items are held 
    //the second held item picked up is refered to as pocketItem
    [SerializeField]  private ItemBase heldItem;
    [SerializeField]  private ItemBase pocketItem;

    //box collider component will be used to find items with area in front of player
    [SerializeField] public BoxCollider itemScanVolume;

    //layermask for use in scanning for items
    [SerializeField] private LayerMask itemLayerMask;

    //layermask for item holders
    [SerializeField] private LayerMask itemHolderLayerMask;

    //reference to CharacterController
    [SerializeField] private CharacterMovement charMovRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup()
    {
        //get all items within itemScanVolume
       Collider[] itemArr = Physics.OverlapBox(itemScanVolume.transform.position, itemScanVolume.size/2, itemScanVolume.transform.rotation, itemLayerMask);

;        //if no items found drop held item if there is one
        if(itemArr.Length <= 0)
        {
            return;
        }

        //check that held item is the only item found
        //if so drop item
        if (heldItem != null && itemArr.Length == 1)
        {

            //if item holder is detecgted place item there instead of just droppign item
            if(detectItemHolder())
            {
                placeItemOnHolder();
            }
            else
            {
                //drop item then return
                dropHeldItem();
            }

            return;
        }

        //if there are no items held and/or items are detected that arent being held by the player
        //find which item detected is nearest to player and pick that one up
        //set index and distance of nearest item to first in array then check against rest of array
        int nearestIndex = 0;
        float nearestDistance = 1000;    //value set to number higher than any distance than can ocur

        //check distance of nearest item to the distance of every other item that was detected
        //i set to 1 as item at index 0 was set as the default nearest item at nearestIndex variable initialization
        for(int i = 0; i < itemArr.Length; i++)
        {

            float newDistance = Vector3.Distance(transform.position, itemArr[i].transform.position);

            ItemBase itemTemp = itemArr[i].GetComponent<ItemBase>();

            //check distance between current determined nearest item and the next detected item in array that isn't already being held
            if (nearestDistance >= newDistance && itemTemp.canBePickedUp())
            {
                //if an item is found that is closer then that index is 
                //saved and the nearestDistance value is updated
                nearestIndex = i;
                nearestDistance = newDistance;

            }

        }

        //store determined nearest item
        ItemBase nearestItem = itemArr[nearestIndex].GetComponent<ItemBase>();

        
        if(mixCheck(nearestItem)  && heldItem != null)
        {
            //if the object to be picked up is choppedvegetables on a cutting board then dont store it
            //instead place the item in hand down 
            placeItemOnHolder();
        }
        else if (heldItem == null && nearestItem.canBePickedUp())
        {
            //the item determined to be the nearest to the player that isn't already being held
            //is stored as the heldItem or pocketItem or not at all if both slots are filled
            storeItem(ref heldItem, ref nearestItem, heldItemAnchor);

        }
        else if(pocketItem == null && nearestItem != heldItem && nearestItem.canBePickedUp())
        {        

            storeItem(ref pocketItem, ref nearestItem, pocketItemAnchor);

        }

    }

    private void dropHeldItem()
    {
        //check for held item, redundancy
        if(heldItem != null)
        {
            //unparent item from anchor
            heldItem.transform.parent = null;

            //unfreeze position and physics constraints
            heldItem.unlockPosition();

            //set internal isHeld bool to false
            heldItem.hasBeenPickedUp(false);

            //clear held item reference
            heldItem = null;

            //if there is an item in the pocket set it to held item
            if(pocketItem != null)
            {
                storeItem(ref heldItem, ref pocketItem, heldItemAnchor);

                //then clear pocket of item
                pocketItem = null;
            }
        }
    }

    //became redundant as items in pocket become held items before they can be dropped
    //NOTE: remove before final version if no use is found
    private void dropPocketItem()
    {
        //check there is an item in pocket
        if(pocketItem != null)
        {
            //unparent item from anchor
            pocketItem.transform.parent = null;

            //unfreeze position and physics constraints
            pocketItem.unlockPosition();

            //clear held item reference
            pocketItem = null;

        }
    }

    private void storeItem(ref ItemBase itemRef, ref ItemBase newItem, Transform posAnchor)
    {
        //set held item reference
        itemRef = newItem;

        //place item at heldItemAnchor
        itemRef.transform.position = posAnchor.position;

        //prohibit item from moving as it is held
        itemRef.lockToPosition();

        //set parent of item to anchor
        itemRef.transform.parent = posAnchor;

        //set itembase internal isHeld bool to true
        itemRef.hasBeenPickedUp(true);

        //if item was on an itemHolder then clear the item reference of that holder
        if(itemRef.checkIsPlaced())
        {
            itemRef.removeSelfFromHolder();
        }
    }

    //returns true if itemHolderBase class GameObject is detected in itemScanVolume
    private bool detectItemHolder()
    {
        //get all items within itemScanVolume
        Collider[] scanArr = Physics.OverlapBox(itemScanVolume.transform.position, itemScanVolume.size / 2, itemScanVolume.transform.rotation, itemHolderLayerMask);

        //bool flag set to false
        bool itemHolderDetected = false;

        //scan through array
        for(int i = 0; i < scanArr.Length; i++)
        {
            //if itemHollderBase found set flag to true
            if(scanArr[i].GetComponent<ItemHolderBase>() != null)
            {
                itemHolderDetected = true;
            }
        }

        //return flag
        return itemHolderDetected;
    }

    //return GameObject of type ItemHolderBase that is closest to player
    private ItemHolderBase getClosestHolder()
    {
        //holder reference variable to be returned
        ItemHolderBase holderRef = null;

        //scan for item holders using layermask
        Collider[] scanArr = Physics.OverlapBox(itemScanVolume.transform.position, itemScanVolume.size / 2, itemScanVolume.transform.rotation, itemHolderLayerMask);

        //nearest distance and index set to values that will always be everriden
        //distance set to high value so the first item holder detected will be closer
        float nearestDist = 100;
        int nearestIndex = 0;

        for (int i = 0; i < scanArr.Length; i++)
        {
            //get reference to new array element's ItemHolderBase Component
            ItemHolderBase iHolderTemp = scanArr[i].GetComponent<ItemHolderBase>();

            //determine distance to player
            float distanceHolder = Vector3.Distance(transform.position, iHolderTemp.transform.position);

            //if item holder is closer to player than previous in array 
            //then set references to point to new array index and record new closest distance
            if (iHolderTemp != null && nearestDist > distanceHolder)
            {
                nearestDist = distanceHolder;
                nearestIndex = i;

                //record reference to Item Holder at array element
                holderRef = iHolderTemp;
            }
        }

        //return reference to closest item holder
        return holderRef;
    }

    private void placeItemOnHolder()
    {
        //get reference to closest item holder
        ItemHolderBase activeHolder = getClosestHolder();

        //local reference to helditem because the heldItem refernce will be cleared next
        ItemBase itemTemp = heldItem;

        //drop item from player
        dropHeldItem();

        //place item on item Holder using local variable
        activeHolder.placeItem(itemTemp, charMovRef);
       
    }

    //returns true if item is a ChoppedVegetable item that is on a CuttingBoard
    private bool mixCheck(ItemBase itemToBeChecked)
    {
        bool doMix = false;

        ChoppedVegetable chopCheck = itemToBeChecked.gameObject.GetComponent<ChoppedVegetable>();

        if(chopCheck != null && chopCheck.checkIsPlaced())
        {
            doMix = true;
        }

        return doMix;
    }
}
