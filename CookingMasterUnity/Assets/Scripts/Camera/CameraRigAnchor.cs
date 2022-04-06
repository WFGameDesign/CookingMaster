using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigAnchor : MonoBehaviour
{
    //This script manages the dynamic camera rig

    //references to keep track of player position
    [SerializeField] private Transform PlayerTracker1;
    [SerializeField] private Transform PlayerTracker2;

    //how fast the anchor tracks players
    [SerializeField] private float trackInterpValue;

    //Camera Dolly values
    [SerializeField] private CameraDollyPivot cameraPivot;
    [SerializeField] private CameraDollyArm cameraArm;

    //minimum and maximum actionable distance between players for the purposes of camera adjustments
    //serve as bounding values for boundedPlayerDistance variable
    [SerializeField] private float minDollyTrackDistance;
    [SerializeField] private float maxDollyTrackDistance;

    private float dollyInterpValue;

    //distance between players
    //is bounded for use in camera tracking functions
    private float boundedPlayerDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the distance between the players with bounds checking
        setBoundedPlayerDistance();

        //interpolation value for current distance in relation to its place between 
        //the min and max trackable distance values
        calcBoundedInterpDistValue();

        
    }

    private void FixedUpdate()
    {
        //interpolate to position between players 1 and 2
        transform.position = Vector3.Lerp(transform.position, calcAnchorPosition(), trackInterpValue);

        //call function for determining camera dolly arm pivot while passing
        cameraPivot.setRelativePivot(dollyInterpValue);

        //call function to set camera distance arm
        cameraArm.setCamDistance(dollyInterpValue);
    }

    //determine where the camera anchor should be moving to
    private Vector3 calcAnchorPosition()
    {
        //get position between players 1 and 2
        Vector3 newAnchorPosition = Vector3.Lerp(PlayerTracker1.position, PlayerTracker2.position, .5f);

        //return calculated position
        return newAnchorPosition;
    }

    private void setBoundedPlayerDistance()
    {
        //get player distance
        //value used in other calculations for determining camera distance, tilt, and pan
        boundedPlayerDistance = Vector3.Distance(PlayerTracker1.position, PlayerTracker2.position);

        //bounds checking for min and max tracking distance between players
        if (boundedPlayerDistance > maxDollyTrackDistance)
        {
            boundedPlayerDistance = maxDollyTrackDistance;
        }
        else if (boundedPlayerDistance < minDollyTrackDistance)
        {
            boundedPlayerDistance = minDollyTrackDistance;
        }
    }

    //determin interpolation value for current distance relative to minDollyTrackDistance and maxDollyTrackDistance
    public void calcBoundedInterpDistValue()
    {
        float interpValHolder = 0f;

        interpValHolder = (boundedPlayerDistance - minDollyTrackDistance) / (maxDollyTrackDistance - minDollyTrackDistance);

        dollyInterpValue = interpValHolder;
    }
}
