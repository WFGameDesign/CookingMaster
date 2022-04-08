using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterController : MonoBehaviour
{
    //string names for the input axis used
    //values are set in editor so that each player can use same controller script
    [SerializeField] private string forwardAxis;
    [SerializeField] private string rightAxis;
    [SerializeField] private string pickupAxis;

    //Holders for input axis value
    //These will be sent to other scripts to facilitate movement and item interactions
    private float forwardMove;
    private float rightMove;
    private float doPickup;

    //bool to check if pickup button is being held down
    private bool isPickupHeld = false;


    //Reference to CharacterMovement script 
    //to be set as part of prefab
    [SerializeField] private CharacterMovement movRef;

    //Reference to CharacterPickup script 
    //to be set as part of prefab
    [SerializeField] private CharacterPickup pickupRef;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get and store input for character movement
        forwardMove = Input.GetAxisRaw(forwardAxis);
        rightMove = Input.GetAxisRaw(rightAxis);

        //get and store input for item pickup
        doPickup = Input.GetAxisRaw(pickupAxis);
    }

    private void FixedUpdate()
    {
        //call to move function with input axis variables
        movRef.Move(forwardMove, rightMove);

        //call to pickup item 
        //only call function once per button press
        if (isPickupHeld == false && doPickup != 0)
        {
            pickupRef.Pickup();
            isPickupHeld = true;
        }
        else if(isPickupHeld == true && doPickup == 0)
        {
            //reset isPickupHeld when button is let go
            isPickupHeld = false;
        }
    }
}
