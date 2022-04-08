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
            //drop item then return
            dropHeldItem();
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

            //check distance between current determined nearest item and the next detected item in array
            if (nearestDistance >= newDistance && heldItem != itemArr[i].GetComponent<ItemBase>())
            {
                //if an item is found that is closer then that index is 
                //saved and the nearestDistance value is updated
                nearestIndex = i;
                nearestDistance = newDistance;

            }

        }

        //store determined nearest item
        ItemBase nearestItem = itemArr[nearestIndex].GetComponent<ItemBase>();

        //the item determined to be the nearest to the player
        //is stored as the heldItem or pocketItem or not at all if both slots are filled
        if (heldItem == null)
        {

            storeItem(ref heldItem, ref nearestItem, heldItemAnchor);

        }
        else if(pocketItem == null && nearestItem != heldItem)
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
    }

}
