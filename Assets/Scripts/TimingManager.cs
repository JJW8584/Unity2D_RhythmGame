using System.Collections.Generic;
using UnityEngine;

//코드 작성: 권지수
public class TimingManager : MonoBehaviour
{
    CreateParticle CreateParticle;
    PlayerController playerController;
    TouchTest touchTest;

    public List<GameObject> boxNoteList = new List<GameObject>(); //생성된 노트를 담을 리스트

    [SerializeField] Transform center = null;   //판정 기준점
    [SerializeField] Transform center1 = null;  //판정 기준점
    float centerValue = 0f; //중심 좌표
    [SerializeField] GameObject[] timingRect = null; //판정 범위
    Vector2[] timingBoxs = null; //판정 범위 좌표
    
    public GameObject[] charSet;

    void Start()
    {
        CreateParticle = FindObjectOfType<CreateParticle>();
        playerController = FindObjectOfType<PlayerController>();
        touchTest = FindObjectOfType<TouchTest>();

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            //판정 범위 설정
            timingBoxs[i].Set(center.localPosition.x - timingRect[i].transform.lossyScale.x / 2,
                center.localPosition.x + timingRect[i].transform.lossyScale.x / 2);
        }

        //중심 좌표 설정
        centerValue = (center.localPosition.y + center1.localPosition.y) / 2;
    }

    private void OnEnable()
    {
        for (int i = 0; i < charSet.Length; i++)
        {
            charSet[i].SetActive(false);
        }

        //현재 선택된 캐릭터 활성화
        charSet[GameManager.instance.charType].SetActive(true);
    }

    void Update()
    {
        //노트가 존재하면
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x; //노트의 x좌표
            float t_notePosY = boxNoteList[0].transform.localPosition.y; //노트의 y좌표

            //노트가 판정 범위를 벗어난 경우
            if (t_notePosX < timingBoxs[2].x) 
            {
                boxNoteList.RemoveAt(0); //리스트에서 노트 제거
                GameManager.instance.combo = 0; //콤보 실패
                ++GameManager.instance.missCnt; //미스 카운트 증가
                touchTest.isNotBoth = true;
                //Debug.Log("miss");
            }

            //동시노트 확인
            if (timingBoxs[2].x <= t_notePosX && t_notePosX <= timingBoxs[2].y && centerValue - 0.5 <= t_notePosY && t_notePosY <= centerValue + 0.5)
            {
                touchTest.isNotBoth = false;
            }
        }
    }

    //위 노트
    public void CheckTiming0()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x; //노트의 x 좌표 
            float t_notePosY = boxNoteList[0].transform.localPosition.y; //노트의 y 좌표

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY > centerValue + 0.5)
                {
                    //SoundManager.instance.PlaySound("HIT"); //타격 효과음 재생
                    CreateParticle.CreateHitEffect(0); //타격 이펙트 재생
                    boxNoteList[0].SetActive(false); //노트 비활성화
                    boxNoteList.RemoveAt(0); //리스트에서 노트 제거
                    switch (j)
                    {
                        case 0:
                            //Debug.Log("Perfect");
                            GameManager.instance.score += GameManager.instance.perfectScore;
                            ++GameManager.instance.perfectCnt;
                            GameManager.instance.ComboPlus(1); //콤보수 증가
                            CreateParticle.CreateEffect(0, 0);
                            break;
                        case 1:
                            //Debug.Log("Good");
                            GameManager.instance.score += GameManager.instance.goodScore;
                            ++GameManager.instance.goodCnt;
                            GameManager.instance.ComboPlus(1); //콤보수 증가
                            CreateParticle.CreateEffect(0, 1);
                            break;
                        case 2:
                            //Debug.Log("Bad");
                            GameManager.instance.score += GameManager.instance.badScore;
                            ++GameManager.instance.badCnt;
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(0, 2);
                            break;
                    }
                    return;
                }
            }
        }
    }

    //아래 노트
    public void CheckTiming1()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x; //노트의 x 좌표 
            float t_notePosY = boxNoteList[0].transform.localPosition.y; //노트의 y 좌표

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY < centerValue - 0.5)
                {
                    //SoundManager.instance.PlaySound("HIT"); //타격 효과음 재생
                    CreateParticle.CreateHitEffect(1); //타격 이펙트 재생
                    boxNoteList[0].SetActive(false); //노트 비활성화
                    boxNoteList.RemoveAt(0); //리스트에서 노트 제거
                    switch (j)
                    {
                        case 0:
                            //Debug.Log("Perfect");
                            GameManager.instance.score += GameManager.instance.perfectScore;
                            ++GameManager.instance.perfectCnt;
                            GameManager.instance.ComboPlus(1); //콤보수 증가
                            CreateParticle.CreateEffect(1, 0);
                            break;
                        case 1:
                            //Debug.Log("Good");
                            GameManager.instance.score += GameManager.instance.goodScore;
                            ++GameManager.instance.goodCnt;
                            GameManager.instance.ComboPlus(1); //콤보수 증가
                            CreateParticle.CreateEffect(1, 1);
                            break;
                        case 2:
                            //Debug.Log("Bad");
                            GameManager.instance.score += GameManager.instance.badScore;
                            ++GameManager.instance.badCnt;
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(1, 2);
                            break;
                    }
                    return;
                }
            }
        }
    }

    // 더블 노트
    public void CheckTiming_Both()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x; //노트의 x 좌표 
            float t_notePosY = boxNoteList[0].transform.localPosition.y; //노트의 y 좌표

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && centerValue - 0.5 <= t_notePosY && t_notePosY <= centerValue + 0.5)
                {
                    //SoundManager.instance.PlaySound("HIT"); //타격 효과음 재생
                    CreateParticle.CreateHitEffect(2); //타격 이펙트 재생
                    boxNoteList[0].SetActive(false); //노트 비활성화
                    boxNoteList.RemoveAt(0); //리스트에서 노트 제거

                    switch (j)
                    {
                        case 0:
                            //Debug.Log("Perfect");
                            GameManager.instance.score += GameManager.instance.perfectScore;
                            ++GameManager.instance.perfectCnt;
                            GameManager.instance.ComboPlus(2); //콤보수 증가
                            CreateParticle.CreateEffect(0, 0);
                            break;
                        case 1:
                            //Debug.Log("Good");
                            GameManager.instance.score += GameManager.instance.goodScore;
                            ++GameManager.instance.goodCnt;
                            GameManager.instance.ComboPlus(2); //콤보수 증가
                            CreateParticle.CreateEffect(0, 1);
                            break;
                        case 2:
                            //Debug.Log("Bad");
                            GameManager.instance.score += GameManager.instance.badScore;
                            ++GameManager.instance.badCnt;
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(0, 2);
                            break;
                    }
                    return;
                }
            }
        }
        
    }
}
