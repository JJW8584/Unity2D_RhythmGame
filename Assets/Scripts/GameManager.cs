using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int maxScore;
    public int perfectScore;
    public int goodScore;
    public int badScore;
    public int perfectCnt;
    public int goodCnt;
    public int badCnt;
    public int missCnt;
    public int score;
    public int speed;
    public int songType; //0~3까지 총 4개
    public string[] playSongList; //4가지 종류
    public int charType; //캐릭터 스킨

    public int combo = 0;

    private void Awake()
    {
        //게임매니저 싱글톤 생성
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        playSongList = new string[4] { "song1", "song2", "song3", "song4" };
        maxScore = 965748;
        GameManagerReset();
    }

    public void ComboPlus(int i)
    {
        combo += i;
    }

    public void GameManagerReset()
    {
        score = 0;
        combo = 0;
        perfectCnt = 0;
        goodCnt = 0;
        badCnt = 0;
        missCnt = 0;
    }
}
