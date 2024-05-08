using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();   //NoteManager 에서 생성된 노트 담기

    [SerializeField] Transform center = null; // 판정 범위의 중심
    [SerializeField] RectTransform[] timingRect = null; // 다양한 판정 범위
    Vector2[] timingBoxs = null; // 판정 범위 최소값 x, 최대값 y

    void Start()
    {
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(center.localPosition.x - timingRect[i].rect.width / 2,
                              center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //노트 위치

            // 판정 순서 : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y)
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //노트 지우기
                    boxNoteList.RemoveAt(i);    //리스트에서 삭제

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            break;
                        case 1:
                            Debug.Log("Good");
                            break;
                        case 2:
                            Debug.Log("Bad");
                            break;
                    }
                    return;
                }
            }
        }

        Debug.Log("Miss");
    }
}
