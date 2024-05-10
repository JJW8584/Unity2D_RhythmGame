using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float elapsedTime = 0.0f;
    private float longClickTime = 2.0f;
    private bool isClicked = false;
    private float elapsedTime_0 = 0.0f;
    private float longClickTime_0 = 2.0f;
    private bool isClicked_0 = false;
    private float elapsedTime_1 = 0.0f;
    private float longClickTime_1 = 2.0f;
    private bool isClicked_1 = false;

    public void OnMouseDown()
    {
        isClicked = true;
    }

    public void OnMouseUp()
    {
        isClicked = false;

        if (longClickTime < elapsedTime) Debug.Log("long click");
    }

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isClicked_0 = true;
            if (Input.GetKeyUp(KeyCode.F))
            {
                isClicked_0 = false;
                elapsedTime += Time.deltaTime;
            }
            if (longClickTime < elapsedTime)
            {
                Debug.Log("long click");
            }
            else
            {
                theTimingManager.CheckTiming_0(); // 판정 체크
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            isClicked_1 = true;
            theTimingManager.CheckTiming_1(); // 판정 체크
        }
    }
}
