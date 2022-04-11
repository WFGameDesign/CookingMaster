using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBoard : MonoBehaviour
{
    [SerializeField] private int[,] scoreBoardData;
    [SerializeField] private ScoreKeeper scoreManager;

    [SerializeField] private Text[] playerIndexText;
    [SerializeField] private Text[] playerScoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager.initHighScoreVariables();
        scoreBoardData = scoreManager.getHighScoreList();

        displayHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void displayHighScore()
    {
        for(int i = 0; i < 10; i++)
        {
            playerIndexText[i].text = "Player " + scoreBoardData[i, 0].ToString();
            playerScoreText[i].text = scoreBoardData[i, 1].ToString(); 
        }
    }
}
