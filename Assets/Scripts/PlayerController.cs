using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//코드작성: 권지수
public class PlayerController : MonoBehaviour
{
    public int MaxHP = 250;
    public int HP;
    public Image HPmeter;
    private float longNoteTime = 0.5f;  //롱노트 기준
    private float DoubleNoteTime = 0.3f; //더블 노트 기준

    private float elapsedTime_0 = 0.0f; //위
    public bool isClicked_0 = false;
    private float elapsedTime_1 = 0.0f; //아래
    public bool isClicked_1 = false;

    public bool isNotBoth = true;

    TimingManager theTimingManager;
    Animator animator;
    AudioSource audioSource;
    public AudioClip JumpSound;
    public AudioClip AttackSound;
    public AudioClip HitSound;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (!GameManager.instance.isTutorial)
        {
            HP = MaxHP;
            UpdateHealthBar();
        }
    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            //위
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.UP1]))
            {
                isClicked_0 = true;
                //Debug.Log("위판정시작");
                if (isNotBoth)
                    SoundManager.instance.PlaySound("JUMP");
                //PlaySound("JUMP");
                theTimingManager.CheckTiming0(); // 판정 체크
                attackMotion0();
            }
            if (isClicked_0 == true) //롱노트
            {
                elapsedTime_0 += Time.deltaTime;

                if (DoubleNoteTime > elapsedTime_0 && (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1])))
                {
                    //Debug.Log("동시입력");
                    //PlaySound("JUMP");
                    theTimingManager.CheckTiming_Both();
                    attackMotionBoth();
                }
                if (longNoteTime < elapsedTime_0)
                {
                    //Debug.Log("long note");
                    //롱노트 존재여부, 없으면 판정끝 hit 멈추기
                }
            }
            if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP0]) || Input.GetKeyUp(KeySetting.keys[KeyAction.UP1]))
            {
                if (longNoteTime < elapsedTime_0)
                {
                    //Debug.Log("long note end");
                }
                isClicked_0 = false;
                elapsedTime_0 = 0.0f;
            }


            //아래
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1]))
            {
                isClicked_1 = true;
                //Debug.Log("아래판정시작");
                if (isNotBoth)
                    SoundManager.instance.PlaySound("JUMP");
                //PlaySound("JUMP");
                theTimingManager.CheckTiming1(); // 판정 체크
                attackMotion();
            }
            if (isClicked_1 == true) //롱노트
            {
                elapsedTime_1 += Time.deltaTime;

                if (DoubleNoteTime > elapsedTime_0 && (Input.GetKeyDown(KeySetting.keys[KeyAction.UP0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.UP1])))
                {
                    //Debug.Log("동시입력");
                    //PlaySound("JUMP");
                    theTimingManager.CheckTiming_Both();
                    attackMotionBoth();
                }
                if (longNoteTime < elapsedTime_1)
                {
                    //Debug.Log("long note");
                    //롱노트 존재여부, 없으면 판정끝 hit 멈추기
                }
            }
            if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN0]) || Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN1]))
            {
                if (longNoteTime < elapsedTime_1)
                {
                    //Debug.Log("long note end");
                }
                isClicked_1 = false;
                elapsedTime_1 = 0.0f;
            }
        }
    }

    public void PlaySound(string action)
    {
        switch(action)
        {
            case "JUMP":
                audioSource.clip = JumpSound;
                break;
            case "ATTACK":
                audioSource.clip = AttackSound;
                break;
            case "HIT":
                audioSource.clip = HitSound;
                break;
        }
        audioSource.Play();
    }


    void attackMotion() //아래 모션   //코드작성 : 지재원
    {
        int attackType = UnityEngine.Random.Range(0, 2);

        if (isNotBoth)
        {
            switch (attackType)
            {
                case 0:
                    //때리는 활성화
                    animator.SetBool("isBodyShot", true);
                    break;
                case 1:
                    //때리는 모션 활성화
                    animator.SetBool("isScratch", true);
                    break;
            }
        }
    }

    void attackMotion0() //위 모션
    {
        int attackType = UnityEngine.Random.Range(0, 2);

        if (isNotBoth)
        {
            switch (attackType)
            {
                case 0:
                    //때리는 활성화
                    animator.SetBool("isBodyShot0", true);
                    break;
                case 1:
                    //때리는 모션 활성화
                    animator.SetBool("isScratch0", true);
                    break;
            }
        }
    }
    void attackMotionBoth()
    {
        if (!isNotBoth)
        {
            animator.SetBool("isUpperCut", true);
        }
    }

    public void SetBodyShot() { animator.SetBool("isBodyShot", false); }
    public void SetScratch() { animator.SetBool("isScratch", false); }
    public void SetBodyShot0() { animator.SetBool("isBodyShot0", false); }
    public void SetScratch0() { animator.SetBool("isScratch0", false); }
    public void SetUpperCut() { animator.SetBool("isUpperCut", false); }
    public void SetisNotBoth() { isNotBoth = true; }


    void UpdateHealthBar()
    {
        HPmeter.fillAmount = (float)HP / (float)MaxHP;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
        }
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isTutorial)
        {
            if (collision.gameObject.tag == "Note0" || collision.gameObject.tag == "Note1" || collision.gameObject.tag == "DoubleNote")
            {
                TakeDamage(10);
            }
        }
        
    }
}
