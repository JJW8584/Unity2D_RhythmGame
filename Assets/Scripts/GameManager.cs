using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int speed;
    public int noteTiming;
    public int songType; //0~3까지 총 4개
    public string[] playSongList; //4가지 종류

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
    }
}
