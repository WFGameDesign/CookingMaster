using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDollyPivot : MonoBehaviour
{

    //min and max pivot in degrees arounf the x axis that the camera arm will turn
    
    [SerializeField] private Quaternion minPivotRot;
    [SerializeField] private Quaternion maxPivotRot;

    //Note: variable was going to be used to interpolate rotation but cause bugs related to camera angle
    //setting rotation without interpolation was a functional solution that allowed camera solution to
    //still be smooth
    //
    //time in seconds to reach desired rotation
    //[SerializeField] private float pivotRotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pivot will be determined by the distance between the players relative
    //to a minimun and maximium trackable distance
    public void setRelativePivot(float distIndex)
    {
        //pivot is relative to min and max pivot
        //1 = maxPivot, 0 = minPivot
        float relativePivot = distIndex;

        //bounds checking redundancy
        if (relativePivot > 1f)
        {
            relativePivot = 1f;
        }
        else if (relativePivot < 0f)
        {
            relativePivot = 0f;
        }

        //calculate pivot rotation in degrees between min and max rotations
        float targetRot = Mathf.Lerp(minPivotRot.x, maxPivotRot.x, relativePivot);

        //apply target pivot rotation
        transform.localRotation = Quaternion.Euler(targetRot, 0f, 0f);
    }

}
