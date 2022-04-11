using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreSerialize : MonoBehaviour
{
    [System.Serializable]
    public class ScoreNode
    {
        public int playerIndex;
        public int playerScore;
    }
}
