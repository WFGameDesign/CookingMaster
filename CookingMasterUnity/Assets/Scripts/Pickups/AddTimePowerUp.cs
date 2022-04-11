using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimePowerUp : PowerUpBase
{
    [SerializeField] private float timeToAdd;
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
        playerRef.addTime(timeToAdd);

        Destroy(gameObject);
    }
}
