using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //reference to rigidbody
    protected Rigidbody rgbRef;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        rgbRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lockToPosition()
    {
        rgbRef.isKinematic = true;
        rgbRef.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Collider>().isTrigger = true;
    }

    public void unlockPosition()
    {
        rgbRef.isKinematic = false;
        rgbRef.constraints = RigidbodyConstraints.None;
        GetComponent<Collider>().isTrigger = false;
    }
}
