using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            theTimingManager.CheckTiming_1(); // 판정 체크
        }
    }
}
