using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //reference to rigidbody
    protected Rigidbody rgbRef;

    //bools if is being held by a player
    protected bool isHeld = false;

    //bool for if item has been placed on a holder
    protected bool isPlaced = false;

    //item holder reference
    protected ItemHolderBase iHolderRef;

    // Start is called before the first frame update
    virtual protected void Awake()
    {
        rgbRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //for locking to anchors when held or placed on an itemHolder
    public void lockToPosition()
    {
        rgbRef.isKinematic = true;
        rgbRef.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Collider>().isTrigger = true;
    }

    //for unlocking when an item has been dropped
    public void unlockPosition()
    {
        rgbRef.isKinematic = false;
        rgbRef.constraints = RigidbodyConstraints.None;
        GetComponent<Collider>().isTrigger = false;
    }

    public void hasBeenPickedUp(bool newState)
    {
        isHeld = newState;
    }

    public void hasBeenPlaced(bool newState)
    {
        isPlaced = newState;
    }

    public bool checkIsPlaced()
    {
        return isPlaced;
    }

    public bool checkIsHeld()
    {
        return isHeld;
    }

    public bool canBePickedUp()
    {
        bool pickupFlag = true;

        if(checkIsHeld())
        {
            pickupFlag = false;
        }

        return pickupFlag;
    }

    public void setNewItemHolder(ItemHolderBase newHolder)
    {
        iHolderRef = newHolder;
    }

    virtual public void removeSelfFromHolder()
    {
        
        if(iHolderRef != null)
        {

            iHolderRef.clearPlacedItem();
            iHolderRef.itemRemovedNotify();
            setNewItemHolder(null);
        }
    }

    virtual public void destroyItem()
    {
        //clear self from any itemHolders
        //then destroy self
        //playerPickup script will clear its own references 
        //whenever necessary
        iHolderRef.clearPlacedItem();
        Destroy(this.gameObject);
    }
}
