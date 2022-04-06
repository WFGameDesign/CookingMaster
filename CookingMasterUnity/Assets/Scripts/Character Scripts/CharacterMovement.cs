using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    //movment variables to be set in editor
    [SerializeField] private float moveSpeed;       
    [SerializeField] private float acceleration;    //speed that player reaches their max movement speed
    [SerializeField] private float rotSpeed;        //speed that player reaches intended rotation

    private Rigidbody rgbRef;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rgbRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing player movement velocity
        //print(rgbRef.velocity.magnitude);
    }

    //movement behavior to be called from CharacterController
    public void Move(float fAxis, float rAxis)
    {

        //determine the direction of movement
        //vector is normalized so speed is uniform when moving diagonally, horizontally, or orthogonally relative to the viewing plane
        Vector3 targetVelocity = new Vector3(rAxis, 0f, fAxis).normalized;

        Vector3 targetForwardVect = rgbRef.transform.forward;

        //set the previously determined direction of movement to determine the target rotation of the player
        //if input axis are 0 the leave rotation as is
        if (fAxis != 0 || rAxis != 0)
        {
            targetForwardVect = targetVelocity;
        }

        //apply the intended movement speed to targetVelocity
        targetVelocity = targetVelocity * moveSpeed;

        //apply target velocity smoothly and with adjustable acceleration
        rgbRef.velocity = Vector3.SmoothDamp(rgbRef.velocity, targetVelocity, ref velocity, acceleration);

        //stop any unintentional angular velocity
        rgbRef.angularVelocity = Vector3.zero;

        //apply target rotation 
        //rgbRef.transform.forward = targetForwardRotation;
        rgbRef.transform.forward = Vector3.SmoothDamp(rgbRef.transform.forward, targetForwardVect, ref rotVelocity, rotSpeed);
    }
}
