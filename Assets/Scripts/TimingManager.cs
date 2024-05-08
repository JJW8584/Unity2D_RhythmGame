using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();   //NoteManager ���� ������ ��Ʈ ���

    [SerializeField] Transform center = null; // ���� ������ �߽�
    [SerializeField] RectTransform[] timingRect = null; // �پ��� ���� ����
    Vector2[] timingBoxs = null; // ���� ���� �ּҰ� x, �ִ밪 y

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
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //��Ʈ ��ġ

            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y)
                {
                    //boxNoteList[i].GetComponent<Note>().HideNote();   //��Ʈ �����
                    boxNoteList.RemoveAt(i);    //����Ʈ���� ����

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