using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float longNoteTime = 0.5f;  //롱노트 기준

    private float elapsedTime_0 = 0.0f; //위
    private bool isClicked_0 = false;
    private float elapsedTime_1 = 0.0f; //아래
    private bool isClicked_1 = false;

    private float BothTime1 = 0.0f;
    private float BothTime2 = 0.0f;
    private float BothTime3 = 0.0f;

    TimingManager theTimingManager;
    Animator animator;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))    //위
        {
            theTimingManager.CheckTiming_0(); // 판정 체크
            isClicked_0 = true;
        }
        if (isClicked_0 == true)
        {
            elapsedTime_0 += Time.deltaTime;
            if (longNoteTime < elapsedTime_0)
            {
                //Debug.Log("long note");
                //롱노트가 존재하냐, 없으면 판정끝 hit 멈추기
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (longNoteTime < elapsedTime_0)
            {
                Debug.Log("long note end");
            }
            isClicked_0 = false;
            elapsedTime_0 = 0.0f;
        }


        if (Input.GetKeyDown(KeyCode.J))    //아래
        {
            theTimingManager.CheckTiming_1(); // 판정 체크
            isClicked_1 = true;
        }
        if (isClicked_1 == true)
        {
            elapsedTime_1 += Time.deltaTime;
            if (longNoteTime < elapsedTime_1)
            {
                //Debug.Log("long note");
                //롱노트 존재여부, 없으면 판정끝 hit 멈추기
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            if (longNoteTime < elapsedTime_1)
            {
                Debug.Log("long note end");
            }
            isClicked_1 = false;
            elapsedTime_1 = 0.0f;
        }

    }

    void attackMotion()   //코드작성 : 지재원
    {
        int attackType = UnityEngine.Random.Range(0, 1);

        switch (attackType)
        {
            case 0:
                //때리는 활성화
                break;
            case 1:
                //때리는 모션 활성화
                break;
        }
    }
}
