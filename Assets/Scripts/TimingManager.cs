using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

//코드작성: 권지수
public class TimingManager : MonoBehaviour
{

    NoteManager theNoteManager;
    CreateParticle CreateParticle;
    PlayerController playerController;

    public List<GameObject> boxNoteList = new List<GameObject>();   //NoteManager 에서 생성된 노트 담기

    [SerializeField] Transform center = null; // 판정 범위의 중심
    [SerializeField] Transform center1 = null; // 판정 범위의 중심
    float centerValue = 0f; //판정 중간값
    [SerializeField] GameObject[] timingRect = null; // 다양한 판정 범위
    Vector2[] timingBoxs = null; // 판정 범위 최소값 x, 최대값 y

    void Start()
    {
        theNoteManager = FindObjectOfType<NoteManager>();
        CreateParticle = FindObjectOfType<CreateParticle>();
        playerController = FindObjectOfType<PlayerController>();

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(center.localPosition.x - timingRect[i].transform.lossyScale.x / 2,
                center.localPosition.x + timingRect[i].transform.lossyScale.x / 2);

            //Debug.Log(timingRect[i].GetComponent<SpriteRenderer>().bounds.size.x);
            //Debug.Log(timingRect[i].transform.lossyScale.x);
            //Debug.Log(timingBoxs[i].x);
            //Debug.Log(timingBoxs[i].y);

        }

        centerValue = (center.localPosition.y + center1.localPosition.y) / 2;


    }

    void Update()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x;    //노트 위치
            float t_notePosY = boxNoteList[0].transform.localPosition.y;    //노트 위치
            if (t_notePosX < timingBoxs[2].x) //bad를 벗어나면
            {
                boxNoteList.RemoveAt(0);    //리스트에서 삭제
                GameManager.instance.combo = 0;
                playerController.isNotBoth = true;
                //Debug.Log("miss");
            }
            if (timingBoxs[2].x <= t_notePosX && t_notePosX <= timingBoxs[2].y && centerValue - 0.5 <= t_notePosY && t_notePosY <= centerValue + 0.5)
            {
                playerController.isNotBoth = false;
            }
        }
    }

    public void CheckTiming0()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x;    //노트 위치
            float t_notePosY = boxNoteList[0].transform.localPosition.y;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //위
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY > centerValue + 0.5)
                {
                    CreateParticle.CreateHitEffect(0);
                    boxNoteList[0].SetActive(false);   //노트 지우기
                    boxNoteList.RemoveAt(0);    //리스트에서 삭제
                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(0, 0);
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(0, 1);
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(0, 2);
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }
    }
    public void CheckTiming1()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x;    //노트 위치
            float t_notePosY = boxNoteList[0].transform.localPosition.y;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //아래
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY < centerValue - 0.5)
                {
                    CreateParticle.CreateHitEffect(1);
                    boxNoteList[0].SetActive(false);   //노트 지우기
                    boxNoteList.RemoveAt(0);    //리스트에서 삭제
                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(1, 0);
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(1, 1);
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(1, 2);
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }
    }
    public void CheckTiming_Both()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x;    //노트 위치
            float t_notePosY = boxNoteList[0].transform.localPosition.y;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //동시
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && centerValue - 0.5 <= t_notePosY && t_notePosY <= centerValue + 0.5)
                {
                    CreateParticle.CreateHitEffect(2);
                    boxNoteList[0].SetActive(false);   //노트 지우기
                    boxNoteList.RemoveAt(0);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            GameManager.instance.ComboPlus(2);
                            CreateParticle.CreateEffect(0, 0);
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            GameManager.instance.ComboPlus(2);
                            CreateParticle.CreateEffect(0, 1);
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(0, 2);
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }
        
    }

    /*//위
    public void CheckTiming0()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //위
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == GameObject.Find("CanNote(Clone)"))
                {
                    CreateParticle.CreateHitEffect(0);
                    boxNoteList[i].SetActive(false);   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            combo++;
                            CreateParticle.CreateEffect(0, 0);
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            combo++;
                            CreateParticle.CreateEffect(0, 1);
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            CreateParticle.CreateEffect(0, 2);
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }

        //Debug.Log("Miss");
    }

    public void CheckTiming1()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //노트 위치


            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //아래
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == GameObject.Find("BellNote(Clone)"))
                {
                    CreateParticle.CreateHitEffect(1);
                    boxNoteList[i].SetActive(false);   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            combo++;
                            CreateParticle.CreateEffect(1, 0);
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            combo++;
                            CreateParticle.CreateEffect(1, 1);
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            CreateParticle.CreateEffect(1, 2);
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }

        //Debug.Log("Miss");
    }

    public void CheckTiming_Both()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //동시
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == GameObject.Find("DoubleNote(Clone)"))
                {

                    boxNoteList[i].SetActive(false);   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            combo += 2;
                            CreateParticle.CreateEffect(0, 0);
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            combo += 2;
                            CreateParticle.CreateEffect(0, 1);
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            CreateParticle.CreateEffect(0, 2);
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }

        //Debug.Log("Miss");
    }*/
}
