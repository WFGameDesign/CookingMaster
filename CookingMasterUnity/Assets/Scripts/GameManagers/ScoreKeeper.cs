using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    //array for tracking player scores
    [SerializeField]private CharacterInfo[] playerList;

    [SerializeField] private Text[] scoreUIArr;

    [SerializeField] private int[,] highScoreRef;

    //winner declaration text variables
    [SerializeField] private Text winnerText;
    [SerializeField] private GameObject winnerTextRoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < playerList.Length; i++)
        {
            scoreUIArr[i].text = playerList[i].getPlayerScore().ToString();
        }

    }

    public void initializePlayerScoreList(CharacterInfo[] playerNum)
    {

            playerList = playerNum;
        
    }

    public void addPointRemote(int index, int score)
    {
        playerList[index].addPlayerScore(score);
    }

    //get high score for current game
    public void getWinner()
    {
        int p1Score = playerList[0].getPlayerScore();
        int p2Score = playerList[1].getPlayerScore();

        if(p1Score == p2Score)
        {
            //tied 
            setWinnerText("Players are tied!!!");
        }else if (p1Score > p2Score)
        {
            //player 1 wins
            setWinnerText("Player 1 Wins!!!");
        }
        else
        {
            //player 2 wins
            setWinnerText("Player 2 Wins!!!");
        }
    }

    //function for high score board info
    public void initHighScoreVariables()
    {
        if (!PlayerPrefs.HasKey("hScorePlayerNum1"))
        {
            //if there is no referenece to the first place on the high score table
            //then initialize whole table

            int[] playerIndexes = new int[10];
            int[] playerScores = new int[10];

            for (int i = 0; i < playerIndexes.Length; i++)
            {
                //generate key for position values in playerPrefs
                string strHolder = "hScorePlayerNum" + (1 + i).ToString();
                print(strHolder);
                PlayerPrefs.SetInt(strHolder, 0);

                //generate key for score value holder
                strHolder = "hScorePlayerValue" + (1 + i).ToString();
                PlayerPrefs.SetInt(strHolder, 0);
                
            }

            downloadHighScoreBoard();

        }
        else
        {
            downloadHighScoreBoard();
        }
    }

    public void downloadHighScoreBoard()
    {
        //copies high scor board into multidimensional arr highScoreRef

        highScoreRef = new int[10, 2];

        //assign values
        for (int i = 0; i < 10; i++)
        {
            //record player index for score
            string strHolder = "hScorePlayerNum" + (1 + i).ToString();
            highScoreRef[i, 0] = PlayerPrefs.GetInt(strHolder);

            //record score
            strHolder = "hScorePlayerValue" + (1 + i).ToString();
            highScoreRef[i, 1] = PlayerPrefs.GetInt(strHolder);

        }

    }

    public void addScore(int index, int score)
    {

        //if highScoreRef hasnt been assigned then return
        if (highScoreRef == null)
        {
            print("ERROR: no high score table found");
            return;
        }

        int floatingScore = score;
        int floatingPlayerIndex = index;

        for (int i = 0; i < 10; i++)
        {
            //if entered score is greater than scor at array value
            if (floatingScore > highScoreRef[i, 1])
            {
                int tempIndex = highScoreRef[i, 0];
                int tempScore = highScoreRef[i, 1];

                highScoreRef[i, 0] = floatingPlayerIndex;
                highScoreRef[i, 1] = floatingScore;

                floatingPlayerIndex = tempIndex;
                floatingScore = tempScore;
                print(123);
            }

        }
    }

    public void submitHighScoreBoard()
    {
        if(PlayerPrefs.GetInt("hScorePlayerNum1") == null)
        {
            return;
        }
        
        for(int i = 0; i < 10; i++)
        {
            //record player index for score
            string strHolder = "hScorePlayerNum" + (1 + i).ToString();
            PlayerPrefs.SetInt(strHolder, highScoreRef[i, 0]);

            //record score
            strHolder = "hScorePlayerValue" + (1 + i).ToString();
            PlayerPrefs.SetInt(strHolder, highScoreRef[i, 1]);
        }
    }

    public void setWinnerText(string textData)
    {
        winnerTextRoot.SetActive(true);
        winnerText.text = textData;
    }

    public int[,] getHighScoreList()
    {
        return highScoreRef;
    }
}
