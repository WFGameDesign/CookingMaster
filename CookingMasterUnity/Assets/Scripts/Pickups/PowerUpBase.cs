using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{

    private CharacterInfo intendedPlayer;

    [SerializeField] protected Material[] playerDesignatorMat;

    [SerializeField] protected MeshRenderer playerDesignatorMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //power up effect is applied here
    virtual protected void onPlayerHit(CharacterInfo hitPlayer)
    {
        //make sure that player is correct one
        if (checkIntendedPlayer(hitPlayer))
        {
            powerUpEffect(hitPlayer);
        }
    }

    virtual protected void powerUpEffect(CharacterInfo playerRef)
    {

    }

    //checks if player passed is the same as the intendedPlayer
    protected bool checkIntendedPlayer(CharacterInfo pToCheck)
    {
        bool isIntendedPlayer = false;

        if(pToCheck == intendedPlayer)
        {
            isIntendedPlayer = true;
        }

        return isIntendedPlayer;
    }

    //to be called when power up is spawned to let it know who it is for
    public void setIntendedPlayer(CharacterInfo newPlayerInfo)
    {
        intendedPlayer = newPlayerInfo;

        playerDesignatorMesh.material = playerDesignatorMat[newPlayerInfo.getPlayerIndex()-1];
    }

    public void OnTriggerEnter(Collider other)
    {
        CharacterInfo cInfoHolder = other.GetComponent<CharacterInfo>();

        if (cInfoHolder != null)
        {
            onPlayerHit(cInfoHolder);
        }
    }
}
