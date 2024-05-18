using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int speed;
    public int songType; //0~3���� �� 4��
    public string[] playSongList; //4���� ����
    public int charType;

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

        playSongList = new string[4] { "song1", "song2", "song3", "song4" };

        combo = 0;
    }


    void Update()
    {
    }

    public void ComboPlus(int i)
    {
        combo += i;
    }
}
