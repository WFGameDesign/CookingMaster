using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : ItemHolderBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void placeItem(ItemBase newItem, CharacterMovement charMov)
    {
        base.placeItem(newItem);
        newItem.destroyItem();

        CharacterInfo infoHolder = charMov.GetComponent<CharacterInfo>();

        if(infoHolder != null)
        {
            infoHolder.addPlayerScore(-10);
        }
    }
}
