using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //reference to players
    [SerializeField] private CharacterInfo[] playerArr;

    private PickupSpawner spawnManager;
    private gameTimer timeManager;
    private ScoreKeeper scoreManager;
    private CustomerManager custManager;

    //reference to timer

    //reference to pickup spawner

    // Start is called before the first frame update
    void Start()
    {
        //get references to various managers
        spawnManager = GetComponent<PickupSpawner>();
        timeManager = GetComponent<gameTimer>();
        scoreManager = GetComponent<ScoreKeeper>();
        custManager = GetComponent<CustomerManager>();

        //check all managers were gotten
        if(spawnManager == null || timeManager == null || scoreManager == null || custManager == null)
        {
            print("ERROR: a sub-manager reference was not set properly in GameManager Start()");
            return;
        }

        //initialize player score list
        scoreManager.initializePlayerScoreList(playerArr);

        //start match time in each player
        startMatchTimers();

        //start first order
        custManager.addOrder();

        //send player list to timemanager
        timeManager.setPlayers(playerArr);

        //get high score variables
        scoreManager.initHighScoreVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callSpawnPowerUp(CharacterInfo charRef)
    {
        spawnManager.spawnPowerUp(charRef);
    }

    private void startMatchTimers()
    {
        for(int i= 0; i < playerArr.Length; i++)
        {
            timeManager.startTimer(playerArr[i]);
        }
    }

    public void gameOverCheck(int index, int score)
    {

        //add scores to high score board
        scoreManager.addScore(index, score);
        //print(timeManager.areAllTimersDone());
        if (timeManager.areAllTimersDone(playerArr))
        {
            //if game over 
            scoreManager.getWinner();
            scoreManager.submitHighScoreBoard();

            StartCoroutine(moveToHighScoreBoard());
        }
        
    }

    public void addPointCall(int index, int score)
    {
        scoreManager.addPointRemote(index, score);
    }

    IEnumerator moveToHighScoreBoard()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("HighScoreBoard");
    }
}
