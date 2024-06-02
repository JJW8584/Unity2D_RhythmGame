using System.Collections.Generic;
using UnityEngine;


public class TimingManager : MonoBehaviour
{
    CreateParticle CreateParticle;
    PlayerController playerController;

    public List<GameObject> boxNoteList = new List<GameObject>(); 

    [SerializeField] Transform center = null;
    [SerializeField] Transform center1 = null; 
    float centerValue = 0f;
    [SerializeField] GameObject[] timingRect = null;
    Vector2[] timingBoxs = null;
    
    public GameObject[] charSet;

    void Start()
    {
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

    private void OnEnable()
    {
        for (int i = 0; i < charSet.Length; i++)
        {
            charSet[i].SetActive(false);
        }

        charSet[GameManager.instance.charType].SetActive(true);
    }

    void Update()
    {

        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x; 
            float t_notePosY = boxNoteList[0].transform.localPosition.y;  
            if (t_notePosX < timingBoxs[2].x) 
            {
                boxNoteList.RemoveAt(0);   
                GameManager.instance.combo = 0;
                ++GameManager.instance.missCnt;
                playerController.isNote = false;
                playerController.isNotBoth = true;
                //Debug.Log("miss");
            }
            if(timingBoxs[2].x <= t_notePosX && t_notePosX <= timingBoxs[2].y)
            {
                playerController.isNote = true;
            }
            if (timingBoxs[2].x <= t_notePosX && t_notePosX <= timingBoxs[2].y && centerValue - 0.5 <= t_notePosY && t_notePosY <= centerValue + 0.5)   //동시노트 확인
            {
                playerController.isNotBoth = false;
            }
        }
    }

    public void CheckTiming0()
    {
        if (boxNoteList.Count != 0)
        {
            float t_notePosX = boxNoteList[0].transform.localPosition.x;   
            float t_notePosY = boxNoteList[0].transform.localPosition.y;  

            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //��
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY > centerValue + 0.5)
                {
                    //playerController.PlaySound("HIT");
                    SoundManager.instance.PlaySound("HIT");
                    CreateParticle.CreateHitEffect(0);
                    boxNoteList[0].SetActive(false);   //��Ʈ �����
                    boxNoteList.RemoveAt(0);    //����Ʈ���� ����
                    switch (j)
                    {
                        case 0:
                            //Debug.Log("Perfect");
                            GameManager.instance.score += GameManager.instance.perfectScore;
                            ++GameManager.instance.perfectCnt;
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(0, 0);
                            //����Ʈ �޺�
                            break;
                        case 1:
                            //Debug.Log("Good");
                            GameManager.instance.score += GameManager.instance.goodScore;
                            ++GameManager.instance.goodCnt;
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(0, 1);
                            //����Ʈ �޺�
                            break;
                        case 2:
                            //Debug.Log("Bad");
                            GameManager.instance.score += GameManager.instance.badScore;
                            ++GameManager.instance.badCnt;
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(0, 2);
                            //����Ʈ
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
            float t_notePosX = boxNoteList[0].transform.localPosition.x;    //��Ʈ ��ġ
            float t_notePosY = boxNoteList[0].transform.localPosition.y;    //��Ʈ ��ġ

            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //�Ʒ�
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && t_notePosY < centerValue - 0.5)
                {
                    //playerController.PlaySound("HIT");
                    SoundManager.instance.PlaySound("HIT");
                    CreateParticle.CreateHitEffect(1);
                    boxNoteList[0].SetActive(false);   //��Ʈ �����
                    boxNoteList.RemoveAt(0);    //����Ʈ���� ����
                    switch (j)
                    {
                        case 0:
                            //Debug.Log("Perfect");
                            GameManager.instance.score += GameManager.instance.perfectScore;
                            ++GameManager.instance.perfectCnt;
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(1, 0);
                            //����Ʈ �޺�
                            break;
                        case 1:
                            //Debug.Log("Good");
                            GameManager.instance.score += GameManager.instance.goodScore;
                            ++GameManager.instance.goodCnt;
                            GameManager.instance.ComboPlus(1);
                            CreateParticle.CreateEffect(1, 1);
                            //����Ʈ �޺�
                            break;
                        case 2:
                            //Debug.Log("Bad");
                            GameManager.instance.score += GameManager.instance.badScore;
                            ++GameManager.instance.badCnt;
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(1, 2);
                            //����Ʈ
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
            float t_notePosX = boxNoteList[0].transform.localPosition.x;    //��Ʈ ��ġ
            float t_notePosY = boxNoteList[0].transform.localPosition.y;    //��Ʈ ��ġ

            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //����
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && centerValue - 0.5 <= t_notePosY && t_notePosY <= centerValue + 0.5)
                {
                    //playerController.PlaySound("HIT");
                    SoundManager.instance.PlaySound("HIT");
                    CreateParticle.CreateHitEffect(2);
                    boxNoteList[0].SetActive(false);   //��Ʈ �����
                    boxNoteList.RemoveAt(0);    //����Ʈ���� ����

                    switch (j)
                    {
                        case 0:
                            //Debug.Log("Perfect");
                            GameManager.instance.score += GameManager.instance.perfectScore;
                            ++GameManager.instance.perfectCnt;
                            GameManager.instance.ComboPlus(2);
                            CreateParticle.CreateEffect(0, 0);
                            //����Ʈ �޺�
                            break;
                        case 1:
                            //Debug.Log("Good");
                            GameManager.instance.score += GameManager.instance.goodScore;
                            ++GameManager.instance.goodCnt;
                            GameManager.instance.ComboPlus(2);
                            CreateParticle.CreateEffect(0, 1);
                            //����Ʈ �޺�
                            break;
                        case 2:
                            //Debug.Log("Bad");
                            GameManager.instance.score += GameManager.instance.badScore;
                            ++GameManager.instance.badCnt;
                            GameManager.instance.combo = 0;
                            CreateParticle.CreateEffect(0, 2);
                            //����Ʈ
                            break;
                    }
                    return;
                }
            }
        }
        
    }

    /*//��
    public void CheckTiming0()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //��Ʈ ��ġ

            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //��
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == GameObject.Find("CanNote(Clone)"))
                {
                    CreateParticle.CreateHitEffect(0);
                    boxNoteList[i].SetActive(false);   //��Ʈ �����
                    boxNoteList.RemoveAt(i);    //����Ʈ���� ����

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            combo++;
                            CreateParticle.CreateEffect(0, 0);
                            //����Ʈ �޺�
                            break;
                        case 1:
                            Debug.Log("Good");
                            combo++;
                            CreateParticle.CreateEffect(0, 1);
                            //����Ʈ �޺�
                            break;
                        case 2:
                            Debug.Log("Bad");
                            CreateParticle.CreateEffect(0, 2);
                            //����Ʈ
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
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //��Ʈ ��ġ


            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //�Ʒ�
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == GameObject.Find("BellNote(Clone)"))
                {
                    CreateParticle.CreateHitEffect(1);
                    boxNoteList[i].SetActive(false);   //��Ʈ �����
                    boxNoteList.RemoveAt(i);    //����Ʈ���� ����

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            combo++;
                            CreateParticle.CreateEffect(1, 0);
                            //����Ʈ �޺�
                            break;
                        case 1:
                            Debug.Log("Good");
                            combo++;
                            CreateParticle.CreateEffect(1, 1);
                            //����Ʈ �޺�
                            break;
                        case 2:
                            Debug.Log("Bad");
                            CreateParticle.CreateEffect(1, 2);
                            //����Ʈ
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
            float t_notePosX = boxNoteList[i].transform.localPosition.x;    //��Ʈ ��ġ

            // ���� ���� : Perfect -> Good -> Bad
            for (int j = 0; j < timingBoxs.Length; j++)
            {
                //����
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y && boxNoteList[i] == GameObject.Find("DoubleNote(Clone)"))
                {

                    boxNoteList[i].SetActive(false);   //��Ʈ �����
                    boxNoteList.RemoveAt(i);    //����Ʈ���� ����

                    switch (j)
                    {
                        case 0:
                            Debug.Log("Perfect");
                            combo += 2;
                            CreateParticle.CreateEffect(0, 0);
                            //����Ʈ �޺�
                            break;
                        case 1:
                            Debug.Log("Good");
                            combo += 2;
                            CreateParticle.CreateEffect(0, 1);
                            //����Ʈ �޺�
                            break;
                        case 2:
                            Debug.Log("Bad");
                            CreateParticle.CreateEffect(0, 2);
                            //����Ʈ
                            break;
                    }
                    return;
                }
            }
        }

        //Debug.Log("Miss");
    }*/
}
