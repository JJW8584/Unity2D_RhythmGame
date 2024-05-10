using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float longClickTime = 2.0f;

    private float elapsedTime_0 = 0.0f;
    private bool isClicked_0 = false;
    private float elapsedTime_1 = 0.0f;
    private bool isClicked_1 = false;

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            theTimingManager.CheckTiming_0(); // 판정 체크
            isClicked_0 = true;
        }
        if (isClicked_0 == true)
        {
            elapsedTime_0 += Time.deltaTime;
            //Debug.Log(elapsedTime_0);
            if (longClickTime < elapsedTime_0)
            {
                Debug.Log("long click");
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (longClickTime < elapsedTime_0)
            {
                Debug.Log("long click end");
            }
            isClicked_0 = false;
            elapsedTime_0 = 0.0f;
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            theTimingManager.CheckTiming_1(); // 판정 체크
            isClicked_1 = true;
        }
        if (isClicked_1 == true)
        {
            elapsedTime_1 += Time.deltaTime;
            //Debug.Log(elapsedTime_1);
            if (longClickTime < elapsedTime_1)
            {
                Debug.Log("long click");
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            if (longClickTime < elapsedTime_1)
            {
                Debug.Log("long click end");
            }
            isClicked_1 = false;
            elapsedTime_1 = 0.0f;
        }

    }
}
