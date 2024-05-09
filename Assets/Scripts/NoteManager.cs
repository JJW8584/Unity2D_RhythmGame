using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//코드작성 : 지재원
public class NoteManager : MonoBehaviour
{
    //오브젝트 풀링
    public GameObject note0Prefab;
    public GameObject note1Prefab;
    public GameObject doubleNotePrefab;
    public GameObject longNoteFrontPrefab;
    public GameObject longNoteBackPrefab;
    public GameObject noteEffectPrefab;

    GameObject[] note0;
    GameObject[] note1;
    GameObject[] doubleNote;
    GameObject[] longNoteFront;
    GameObject[] longNoteBack;
    GameObject[] noteEffect;

    GameObject[] targetPool;

    //노트 정보 저장
    private List<Tuple<float, int, int, float>> noteData;
    public Transform notePos0;
    public Transform notePos1;

    //플레이 노래
    AudioSource playSong;

    private void Awake()
    {
        note0 = new GameObject[40];
        note1 = new GameObject[40];
        doubleNote = new GameObject[30];
        longNoteFront = new GameObject[5];
        longNoteBack = new GameObject[30];
        noteEffect = new GameObject[20];

        Generate();

        noteData = new List<Tuple<float, int, int, float>>();
    }

    private void Start()
    {
        playSong.Play();
    }

    private void Update()
    {
        StartCoroutine(PlayGame());
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
        for (int i = 0; i < longNoteBack.Length; i++)
        {
            longNoteBack[i] = Instantiate(longNoteBackPrefab);
            longNoteBack[i].SetActive(false);
        }
        for (int i = 0; i < noteEffect.Length; i++)
        {
            noteEffect[i] = Instantiate(noteEffectPrefab);
            noteEffect[i].SetActive(false);
        }
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
            case "longNoteBack":
                targetPool = longNoteBack;
                break;
            case "noteEffect":
                targetPool = noteEffect;
                break;
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

    public void readNoteFile(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // 공백을 기준으로 문자열을 분리하여 각각의 값으로 변환하여 Tuple 생성
                string[] values = line.Split(' ');
                if (values.Length == 4)
                {
                    if (float.TryParse(values[0], out float val1) && //형 변환 후 리스트에 추가
                        int.TryParse(values[1], out int val2) &&
                        int.TryParse(values[2], out int val3) &&
                        float.TryParse(values[3], out float val4))
                    {
                        noteData.Add(new Tuple<float, int, int, float>(val1, val2, val3, val4));
                    }
                }
            }
        }
    }

    IEnumerator PlayGame()
    {
        yield return null;
        for(int i = 0; i < noteData.Count; i++)
        {
            while(true)
            {
                if (playSong.time >= noteData[i].Item1)
                {
                    switch (noteData[i].Item3)
                    {
                        //재생시간 생성위치 노트종류 지속시간
                        case 0: //단일노트
                            CreateNote(noteData[i].Item2, noteData[i].Item3);
                            break;
                        case 1: //이중노트
                            break;
                        case 2: //롱노트
                            break;
                    }
                    break;
                }
                yield return null;
            }
        }
    }

    void CreateNote(int noteLoc, int noteType)
    {
        GameObject note;
        switch (noteLoc)
        {
            case 0: //위쪽
                note = MakeObj("note0");
                note.gameObject.transform.position = notePos0.position;
                break;
            case 1: //아래쪽
                note = MakeObj("note1");
                note.gameObject.transform.position = notePos1.position;
                break;
        }
    }
}
