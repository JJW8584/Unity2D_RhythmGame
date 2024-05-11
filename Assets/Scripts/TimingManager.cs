using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimingManager : MonoBehaviour
{

    NoteManager theNoteManager;

    public List<GameObject> boxNoteList = new List<GameObject>();   //NoteManager 에서 생성된 노트 담기

    [SerializeField] Transform center = null; // 판정 범위의 중심
    [SerializeField] GameObject[] timingRect = null; // 다양한 판정 범위
    Vector2[] timingBoxs = null; // 판정 범위 최소값 x, 최대값 y


    void Awake()
    {
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(center.localPosition.x - timingRect[i].transform.lossyScale.x / 2,
                              center.localPosition.x + timingRect[i].transform.lossyScale.x / 2);
            //Debug.Log(timingBoxs[i]);
        }
    }

    //위
    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //노트 위치
            float t_notePosY = boxNoteList[i].transform.localPosition.y;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //위
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY >= 0.5) 
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            //이펙트
                            break;
                    }
                    return;
                }
                //아래
                else if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY < 0.5)
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            //이펙트
                            break;
                    }
                    return;
                }
                //동시
                else if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == theNoteManager.doubleNotePrefab)
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }

        //Debug.Log("Miss");
    }


    //롱노트
    public void CheckTiming_Long()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //노트 위치
            float t_notePosY = boxNoteList[i].transform.localPosition.y;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //위
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY >= 0.5 && boxNoteList[i] == theNoteManager.longNoteMidPrefab)
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            //이펙트
                            break;
                    }
                    return;
                }
                //아래
                else if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY < 0.5 && boxNoteList[i] == theNoteManager.longNoteMidPrefab)
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            //이펙트 콤보
                            break;
                        case 1:
                            Debug.Log("Good");
                            //이펙트 콤보
                            break;
                        case 2:
                            Debug.Log("Bad");
                            //이펙트
                            break;
                    }
                    return;
                }
            }
        }

        //Debug.Log("Miss");
    }
}
