using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//코드작성 : 지재원
public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;

    //오브젝트 풀링
    public GameObject note0Prefab;
    public GameObject note1Prefab;
    public GameObject doubleNotePrefab;
    public GameObject longNoteFrontPrefab;
    public GameObject longNoteMidPrefab;
    public GameObject longNoteEndPrefab;
    public GameObject startNotePrefab;
    //public GameObject notePerfectEffectPrefab;
    //public GameObject noteGoodEffectPrefab;
    //public GameObject noteBadEffectPrefab;

    GameObject[] note0;
    GameObject[] note1;
    GameObject[] doubleNote;
    GameObject[] longNoteFront;
    GameObject[] longNoteMid;
    GameObject[] longNoteEnd;
    GameObject[] startNote;
    //GameObject[] notePerfectEffect;
    //GameObject[] noteGoodEffect;
    //GameObject[] noteBadEffect;

    GameObject[] targetPool;

    private void Awake()
    {
        //노트매니저 싱글톤 생성
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        note0 = new GameObject[40];
        note1 = new GameObject[40];
        doubleNote = new GameObject[30];
        longNoteFront = new GameObject[5];
        longNoteMid = new GameObject[30];
        longNoteEnd = new GameObject[5];
        startNote = new GameObject[1];
        //notePerfectEffect = new GameObject[20];
        //noteGoodEffect = new GameObject[20];
        //noteBadEffect = new GameObject[20];

        Generate();
    }
    void Generate()
    {
        for (int i = 0; i < note0.Length; i++)
        {
            note0[i] = Instantiate(note0Prefab);
            note0[i].SetActive(false);
        }
        for (int i = 0; i < note1.Length; i++)
        {
            note1[i] = Instantiate(note1Prefab);
            note1[i].SetActive(false);
        }
        for (int i = 0; i < doubleNote.Length; i++)
        {
            doubleNote[i] = Instantiate(doubleNotePrefab);
            doubleNote[i].SetActive(false);
        }
        for (int i = 0; i < longNoteFront.Length; i++)
        {
            longNoteFront[i] = Instantiate(longNoteFrontPrefab);
            longNoteFront[i].SetActive(false);
        }
        for (int i = 0; i < longNoteMid.Length; i++)
        {
            longNoteMid[i] = Instantiate(longNoteMidPrefab);
            longNoteMid[i].SetActive(false);
        }
        for (int i = 0; i < longNoteEnd.Length; i++)
        {
            longNoteEnd[i] = Instantiate(longNoteEndPrefab);
            longNoteEnd[i].SetActive(false);
        }
        for (int i = 0; i < startNote.Length; i++)
        {
            startNote[i] = Instantiate(startNotePrefab);
            startNote[i].SetActive(false);
        }
        /*for (int i = 0; i < notePerfectEffect.Length; i++)
        {
            notePerfectEffect[i] = Instantiate(notePerfectEffectPrefab);
            notePerfectEffect[i].SetActive(false);
        }
        for (int i = 0; i < noteGoodEffect.Length; i++)
        {
            noteGoodEffect[i] = Instantiate(noteGoodEffectPrefab);
            noteGoodEffect[i].SetActive(false);
        }
        for (int i = 0; i < noteBadEffect.Length; i++)
        {
            noteBadEffect[i] = Instantiate(noteBadEffectPrefab);
            noteBadEffect[i].SetActive(false);
        }*/
    }

    public GameObject MakeObj(string Type) //객체 생성하는 메소드
    {
        switch (Type)
        {
            case "note0":
                targetPool = note0;
                break;
            case "note1":
                targetPool = note1;
                break;
            case "doubleNote":
                targetPool = doubleNote;
                break;
            case "longNoteFront":
                targetPool = longNoteFront;
                break;
            case "longNoteMid":
                targetPool = longNoteMid;
                break;
            case "longNoteEnd":
                targetPool = longNoteEnd;
                break;
            case "startNote":
                targetPool = startNote;
                break;
            /*case "notePerfectEffect":
                targetPool = notePerfectEffect;
                break;
            case "noteGoodEffect":
                targetPool = noteGoodEffect;
                break;
            case "noteBadEffect":
                targetPool = noteBadEffect;
                break;*/
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public void ResetPool()
    {
        for (int j = 1; j < 8; j++)
        {
            switch (j)
            {
                case 1:
                    targetPool = note0;
                    break;
                case 2:
                    targetPool = note1;
                    break;
                case 3:
                    targetPool = doubleNote;
                    break;
                case 4:
                    targetPool = longNoteFront;
                    break;
                case 5:
                    targetPool = longNoteMid;
                    break;
                case 6:
                    targetPool = longNoteEnd;
                    break;
                case 7:
                    targetPool = startNote;
                    break;
            }
            for (int i = 0; i < targetPool.Length; i++)
            {
                if (targetPool[i].activeSelf)
                {
                    targetPool[i].SetActive(false);
                }
            }
        }
    }
}