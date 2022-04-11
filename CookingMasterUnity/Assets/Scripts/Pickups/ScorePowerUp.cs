using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerUp : PowerUpBase
{
    //amount to increas score by
    [SerializeField] private int scoreBoost;

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
        playerRef.addPlayerScore(scoreBoost);

            Destroy(gameObject);
    }
}
