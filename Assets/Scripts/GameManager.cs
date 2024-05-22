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
    public float speed;
    public int songType; //0~2���� �� 3��
    public int charType; //ĳ���� ��Ų
    public bool isPause;

    public int combo = 0;

    private void Awake()
    {
        //���ӸŴ��� �̱��� ����
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        maxScore = 965748;
        speed = 1.5f;
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
