using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolderBase : MonoBehaviour
{
    //a script for any GameObject that can have an ItemBase, or class derived thereof, GameObject placed on it

    //the anchor for the where the item is placed
    [SerializeField] protected Transform placedItemAnchor;

    //refrence to placed item
    [SerializeField]protected ItemBase placedItem;

    //multiple derived classes can spawn objects so that reference is in the base class
    [SerializeField] protected ItemBase Spawnable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //locks item to anchor and in derived classes will have expanded functionality
    virtual public void placeItem(ItemBase newItem)
    {
        //make sure another item isnt already placed
        if (!isItemPlaced())
        {

            //save reference to Item
            placedItem = newItem;

            //place at anchor
            placedItem.transform.position = placedItemAnchor.position;

            //parent to anchor
            placedItem.transform.parent = placedItemAnchor;

            //lock item position
            placedItem.lockToPosition();

            //set booleans for if item is held by player or placed on holder
            placedItem.hasBeenPlaced(true);
            placedItem.hasBeenPickedUp(false);

            //set item holder internal reference variable
            placedItem.setNewItemHolder(this);
        }
        
    }

    virtual public void placeItem(ItemBase newItem, CharacterMovement charMov)
    {
        placeItem(newItem);
    }

    public bool isItemPlaced()
    {
        //bool flag to be returned set to false initialy
        bool itemPlacedFlag = false;

        //if an item is placed switch flag to true
        if (placedItem != null)
        {
            itemPlacedFlag = true;
        }

        //return flag
        return itemPlacedFlag;
    }

    virtual public void clearPlacedItem()
    {
        placedItem = null;

    }

    //virtual spawn function
    //NOTE: currently unused, delete if remains unused
    virtual protected void initSpawnable()
    {
        
    }

    virtual protected void placeOnSelf(ItemBase itemToPlace)
    {
        placedItem = itemToPlace;

    }

    //catch all function for what happens when an object is removed from this holder
    virtual public void itemRemovedNotify()
    {

    }

    virtual public ItemBase giveItem()
    {
        return placedItem;
    }
}
