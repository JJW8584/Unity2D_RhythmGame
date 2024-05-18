using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//코드작성 : 지재원
public class NoteManager : MonoBehaviour
{
    //오브젝트 풀링
    public GameObject note0Prefab;
    public GameObject note1Prefab;
    public GameObject doubleNotePrefab;
    public GameObject longNoteFrontPrefab;
    public GameObject longNoteMidPrefab;
    public GameObject longNoteEndPrefab;
    public Transform[] upNoteCurve;
    public Transform[] downNoteCurve;
    public Transform[] doubleNoteCurve;
    //public GameObject notePerfectEffectPrefab;
    //public GameObject noteGoodEffectPrefab;
    //public GameObject noteBadEffectPrefab;

    GameObject[] note0;
    GameObject[] note1;
    GameObject[] doubleNote;
    GameObject[] longNoteFront;
    GameObject[] longNoteMid;
    GameObject[] longNoteEnd;
    //GameObject[] notePerfectEffect;
    //GameObject[] noteGoodEffect;
    //GameObject[] noteBadEffect;

    GameObject[] targetPool;

    //노트 정보 저장
    private List<Tuple<float, int, int, float>> noteData;
    private bool[] noteCreateCheck; //노트가 생성되었는지 체크
    public Transform notePos0;
    public Transform notePos1;

    //플레이 노래
    public AudioSource playSong;
    TimingManager theTimingManager;

    private void Awake()
    {
        note0 = new GameObject[40];
        note1 = new GameObject[40];
        doubleNote = new GameObject[30];
        longNoteFront = new GameObject[5];
        longNoteMid = new GameObject[30];
        longNoteEnd = new GameObject[5];
        //notePerfectEffect = new GameObject[20];
        //noteGoodEffect = new GameObject[20];
        //noteBadEffect = new GameObject[20];

        Generate();

        noteData = new List<Tuple<float, int, int, float>>();

        theTimingManager = FindObjectOfType<TimingManager>();

        ReadNoteFile();
    }

    private void Start()
    {
        playSong.Play();
    }

    private void Update()
    {
        PlayGame();
    }
    void Generate()
    {
        for (int i = 0; i < note0.Length; i++)
        {
            note0[i] = Instantiate(note0Prefab);
            note0[i].GetComponent<Note>().controlPoints = upNoteCurve;
            note0[i].SetActive(false);
        }
        for (int i = 0; i < note1.Length; i++)
        {
            note1[i] = Instantiate(note1Prefab);
            note1[i].GetComponent<Note>().controlPoints = downNoteCurve;
            note1[i].SetActive(false);
        }
        for (int i = 0; i < doubleNote.Length; i++)
        {
            doubleNote[i] = Instantiate(doubleNotePrefab);
            doubleNote[i].GetComponent<Note>().controlPoints = doubleNoteCurve;
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
                theTimingManager.boxNoteList.Add(targetPool[i]); //타이밍 리스트에 추가
                return targetPool[i];
            }
        }

        return null;
    }

    public void ReadNoteFile()
    {
        noteData.Clear();

        TextAsset textFile = Resources.Load("song1") as TextAsset; //텍스트로 파일 읽어옴
        StringReader stringReader = new StringReader(textFile.text);

        string line;
        while ((line = stringReader.ReadLine()) != null) //한 줄씩 읽음
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
        noteCreateCheck = new bool[noteData.Count];
        for (int i = 0; i < noteData.Count; i++) //노트 생성 체크 초기화
        {
            noteCreateCheck[i] = false;
        }
    }

    void PlayGame()
    {
        for(int i = 0; i < noteData.Count; i++)
        {
            if ((playSong.time >= noteData[i].Item1) && !noteCreateCheck[i]) //노트가 생성되지 않았을 때 시간에 맞춰서 생성
            {
                //Debug.Log(noteData[i].Item1);
                noteCreateCheck[i] = true;
                switch (noteData[i].Item3)
                {
                    //재생시간 생성위치 노트종류 지속시간
                    case 0: //단일노트
                        CreateNote(noteData[i].Item2, noteData[i].Item3);
                        break;
                    case 1: //이중노트
                        CreateNote(noteData[i].Item2, noteData[i].Item3);
                        break;
                    case 2: //롱노트
                        break;
                }
                break;
            }
        }
    }

    void CreateNote(int noteLoc, int noteType)
    {
        GameObject note;
        if (noteType == 0)
        {
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
        else if(noteType == 1)
        {
            //더블노트 생성
            note = MakeObj("doubleNote");
            note.gameObject.transform.position = new Vector2(10f, 0);
        }
    }
}
