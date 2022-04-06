using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterController : MonoBehaviour
{
    //string names for the input axis used
    //values are set in editor so that each player can use same controller script
    [SerializeField] private string forwardAxis;
    [SerializeField] private string rightAxis;

    //Holders for input axis value
    //These will be sent to CharacterMovement component through Move function
    private float forwardMove;
    private float rightMove;

    //Reference to CharacterMovement script from same GameObject
    //to be set as part of prefab
    [SerializeField] private CharacterMovement movRef;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        forwardMove = Input.GetAxisRaw(forwardAxis);

        rightMove = Input.GetAxisRaw(rightAxis);
    }

    private void FixedUpdate()
    {
        //call to move function with input axis variables
        movRef.Move(forwardMove, rightMove);
    }
}
