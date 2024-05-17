using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//코드작성 : 지재원
public class Score : MonoBehaviour
{
    public TextMeshProUGUI perfectCount;
    public TextMeshProUGUI goodCount;
    public TextMeshProUGUI badCount;
    public TextMeshProUGUI missCount;
    public TextMeshProUGUI score;
    public TextMeshProUGUI Rank;

    private void Start()
    {
        perfectCount.text = string.Format("{0:n0}", GameManager.instance.perfectCnt);
        goodCount.text = string.Format("{0:n0}", GameManager.instance.goodCnt);
        badCount.text = string.Format("{0:n0}", GameManager.instance.badCnt);
        missCount.text = string.Format("{0:n0}", GameManager.instance.missCnt);
        score.text = string.Format("{0:D7}", GameManager.instance.score);
        if (GameManager.instance.score >= GameManager.instance.maxScore * 95 / 10)
            Rank.text = "SS";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 9)
            Rank.text = "S";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 8)
            Rank.text = "A";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 7)
            Rank.text = "B";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 6)
            Rank.text = "C";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 5)
            Rank.text = "D";
        else
            Rank.text = "F";
    }
}
