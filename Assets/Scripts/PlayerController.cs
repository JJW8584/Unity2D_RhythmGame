using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
            //theTimingManager.CheckTiming(); // 판정 체크
            Motion();
=======
>>>>>>> Stashed changes

            theTimingManager.CheckTiming_0(); // 판정 체크
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            theTimingManager.CheckTiming_1(); // 판정 체크
<<<<<<< Updated upstream
=======
>>>>>>> 45f347536ee05bff237270e923c715d5f9f67051
>>>>>>> Stashed changes
        }
    }

    void Motion() //노트 클릭시 캐릭터 모션                                     //코드작성 : 지재원
    {                                                                           //..
        int attackType = Random.Range(0, 2);                                    //..
        switch (attackType)                                                     //..
        {                                                                       //..
            case 0:                                                             //..
                animator.SetBool("isScratch", true);                            //..
                break;                                                          //..
            case 1:                                                             //..
                animator.SetBool("isBodyShot", true);                           //..
                break;                                                          //..
        }                                                                       //..
    }                                                                           //끝

    //달리는 모션으로 돌아오기 위한 메소드
    public void ReturnScratch() { animator.SetBool("isScratch", false); }       //코드작성 : 지재원
    public void ReturnBodyshot() { animator.SetBool("isBodyShot", false); }     //..
    public void ReturnHit() { animator.SetBool("isHit", false); }               //..
    public void ReturnUppercut() { animator.SetBool("isUppercut", false); }     //끝
}
