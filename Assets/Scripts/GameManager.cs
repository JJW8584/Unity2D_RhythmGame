using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public int score;
    public int speed;
    public int noteTiming;


    private void Awake()
    {
        //霸烙概聪历 教臂沛 积己
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
