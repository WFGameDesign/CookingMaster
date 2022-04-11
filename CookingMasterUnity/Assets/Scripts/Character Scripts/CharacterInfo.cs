using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    //holds general character info
    [SerializeField] private int playerIndex;

    //movement reference
    private CharacterMovement myMov;

    //player score holder
    [SerializeField] int playerScore;

    //refrence to GameManger
    private GameManager myManager;

    //player time holder
    //manipulated externally
    [SerializeField] private float matchTimer;
    public bool matchTimerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        myMov = GetComponent<CharacterMovement>();

        myManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(myMov == null || myManager == null)
        {
            print("Error characterinfo manager getvomponent failed");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (matchTimerActive)
        {

            matchTimer -= Time.deltaTime;

            if(matchTimer <= 0)
            {
                //put time end behavior here
                myManager.gameOverCheck(playerIndex, playerScore);

                //hide player
                myMov.lockPlayerMovement();

                //stop timer
                matchTimerActive = false;
            }
        }
    }

    //tells characterMovement component to apply a speed boost
    public void applySpeedBoost()
    {
        myMov.boostSpeed();
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    //player score manipulation functions
    public void addPlayerScore(int pointToAdd)
    {
        playerScore += pointToAdd;

        //add behavior to update ui here;
    }

    public int getPlayerScore()
    {
        return playerScore;
    }

    //player timer manipulators
    public void addTime(float extraTime)
    {
        matchTimer += extraTime;
    }

    public float getTimer()
    {
        return matchTimer;
    }

    public void startMatchTimer(float startingTime)
    {
        matchTimer = startingTime;
        matchTimerActive = true;

    }

    //checks if timer is running
    public bool isTimerDone()
    {
        bool isDone = true;

        if(matchTimer > 0)
        {
            isDone = false;
        }

        return isDone; ;
    }
}
