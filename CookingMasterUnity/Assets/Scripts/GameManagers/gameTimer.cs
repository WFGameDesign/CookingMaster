using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTimer : MonoBehaviour
{
    //default length of game in seconds
    [SerializeField] private float gameDefaultTimer;

    //reference to player
    private CharacterInfo[] playerArr;

    //ui references
    [SerializeField] private Text[] uiTimers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < playerArr.Length; i++)
        {
            uiTimers[i].text = Mathf.Round( playerArr[i].getTimer()).ToString();
        }
    }


    public float getTimeRemaining(CharacterInfo charRef)
    {
        return charRef.getTimer();
    }

    public void startTimer(CharacterInfo charRef)
    {
        charRef.startMatchTimer(gameDefaultTimer);
    }
    
    public bool areAllTimersDone(CharacterInfo[] charRef)
    {
        bool allTimersDone = true;

        for(int i = 0; i < charRef.Length; i++)
        {
            if(!charRef[i].isTimerDone())
            {
                allTimersDone = false;
            }
        }

        return allTimersDone;
    }

    public void setPlayers(CharacterInfo[] info)
    {
        playerArr = info;
    }
}
