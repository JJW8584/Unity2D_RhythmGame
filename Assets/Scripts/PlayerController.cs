using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//코드작성: 권지수
public class PlayerController : MonoBehaviour
{
    private float longNoteTime = 0.5f;  //롱노트 기준
    private float DoubleNoteTime = 0.3f; //더블 노트 기준

    private float elapsedTime_0 = 0.0f; //위
    private bool isClicked_0 = false;
    private float elapsedTime_1 = 0.0f; //아래
    private bool isClicked_1 = false;

    [HideInInspector]
    public bool BothNote = false;

    TimingManager theTimingManager;
    NoteManager theNoteManager;
    Animator animator;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theNoteManager = FindObjectOfType<NoteManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //위
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("위판정시작");
            //theTimingManager.BothNote = true;
            attackMotion0();
            theTimingManager.CheckTiming0(); // 판정 체크
            
            isClicked_0 = true;
        }
        if (isClicked_0 == true) //롱노트
        {
            elapsedTime_0 += Time.deltaTime;

            if (DoubleNoteTime > elapsedTime_0 && Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("동시입력");
                theTimingManager.CheckTiming_Both();
                animator.SetBool("isUpperCut", true);
                theTimingManager.BothNote = false;
            }
            if (longNoteTime < elapsedTime_0)
            {
                //Debug.Log("long note");
                //롱노트 존재여부, 없으면 판정끝 hit 멈추기
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


        //아래
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("아래판정시작");
            //theTimingManager.BothNote = true;
            attackMotion();
            theTimingManager.CheckTiming1(); // 판정 체크
            isClicked_1 = true;
        }
        if (isClicked_1 == true) //롱노트
        {
            elapsedTime_1 += Time.deltaTime;

            if (DoubleNoteTime > elapsedTime_0 && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("동시입력");
                theTimingManager.CheckTiming_Both();
                animator.SetBool("isUpperCut", true);
                theTimingManager.BothNote = false;
            }
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

        /*//동시노트
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("동시입력");
            theTimingManager.CheckTiming(); // 판정 체크
            theTimingManager.CheckTiming_Both();
        }
        else if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            Debug.Log("동시입력");
            theTimingManager.CheckTiming(); // 판정 체크
            theTimingManager.CheckTiming_Both();
        }*/

    }


    void attackMotion()   //코드작성 : 지재원
    {
        int attackType = UnityEngine.Random.Range(0, 2);

        if (attackType == 0 && theTimingManager.BothNote == false)
        {
            animator.SetBool("isBodyShot", true);
        }
        else if (attackType == 1 && theTimingManager.BothNote == false)
        {
            animator.SetBool("isScratch", true);
        }

        /*switch (attackType)
        {
            case 0:
                //때리는 활성화
                animator.SetBool("isBodyShot", true);
                break;
            case 1:
                //때리는 모션 활성화
                animator.SetBool("isScratch", true);
                break;
        }*/
    }

    void attackMotion0()
    {
        int attackType = UnityEngine.Random.Range(0, 2);

        if (attackType == 0 && theTimingManager.BothNote == false) 
        {
            animator.SetBool("isBodyShot0", true);
        }
        else if (attackType == 1 && theTimingManager.BothNote == false)
        {
            animator.SetBool("isScratch0", true);
        }

        /*switch (attackType)
        {
            case 0:
                //때리는 활성화
                animator.SetBool("isBodyShot0", true);
                break;
            case 1:
                //때리는 모션 활성화
                animator.SetBool("isScratch0", true);
                break;
        }*/
    }

    public void SetBodyShot() { animator.SetBool("isBodyShot", false); }
    public void SetScratch() { animator.SetBool("isScratch", false); }
    public void SetBodyShot0() { animator.SetBool("isBodyShot0", false); }
    public void SetScratch0() { animator.SetBool("isScratch0", false); }
    public void SetUpperCut() { animator.SetBool("isUpperCut", false); }
}
