using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUpBase
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void powerUpEffect(CharacterInfo playerRef)
    {
        //put speed up code here
        playerRef.applySpeedBoost();

        Destroy(gameObject);
    }

    
}
