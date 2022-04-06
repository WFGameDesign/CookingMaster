using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDollyArm : MonoBehaviour
{
    //min and max dolly arm distances
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    [SerializeField] private float distChangeRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCamDistance(float distInterpVal)
    {

        //distance is relative to min and max distance
        //1 = max distance, 0 = min distance
        float interpHolder = distInterpVal;

        //bounds checking redundancy
        if(interpHolder > 1)
        {
            interpHolder = 1;
        }else if(interpHolder < 0)
        {
            interpHolder = 0;
        }

        float targetDist = Mathf.Lerp(minDistance, maxDistance, interpHolder);
        Vector3 targetPos = new Vector3(0f, 0f, targetDist);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime / distChangeRate);
    }
}
