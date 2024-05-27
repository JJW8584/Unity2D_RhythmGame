using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public Transform[] startNoteCurve;
    public Transform[] upNoteCurve;
    public Transform[] downNoteCurve;
    public Transform[] doubleNoteCurve;
    public Transform[] wheelCurve;

    //노트 정보 저장
    private List<Tuple<float, int, int, float>> noteData;
    private bool[] noteCreateCheck; //노트가 생성되었는지 체크
    public string songName;
    public Transform notePos0;
    public Transform notePos1;

    //플레이 노래
    public AudioSource playSong;
    TimingManager theTimingManager;

    private void Awake()
    {
        noteData = new List<Tuple<float, int, int, float>>();

        theTimingManager = FindObjectOfType<TimingManager>();

        ReadNoteFile();

        GameManager.instance.perfectScore = GameManager.instance.maxScore * 10 / noteData.Count;
        GameManager.instance.goodScore = GameManager.instance.maxScore * 7 / noteData.Count;
        GameManager.instance.badScore = GameManager.instance.maxScore * 3 / noteData.Count;
    }
    // Start is called before the first frame update
    void Start()
    {
        SongPlay();
        playSong.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GamePlay();
    }
    public void ReadNoteFile()
    {
        noteData.Clear();

        TextAsset textFile = Resources.Load(songName) as TextAsset; //텍스트로 파일 읽어옴
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

    private void SongPlay()
    {
        GameObject note = NoteManager.instance.MakeObj("startNote");
        note.GetComponent<Note>().controlPoints = startNoteCurve;
        note.gameObject.transform.position = startNoteCurve[0].position;
    }
    private void GamePlay()
    {
        for (int i = 0; i < noteData.Count; i++)
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
                    case 3: //톱니바퀴
                        CreateNote(noteData[i].Item2, noteData[i].Item3);
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
                    note = NoteManager.instance.MakeObj("note0");
                    theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
                    note.gameObject.transform.position = notePos0.position;
                    note.GetComponent<Note>().controlPoints = upNoteCurve;
                    break;
                case 1: //아래쪽
                    note = NoteManager.instance.MakeObj("note1");
                    theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
                    note.gameObject.transform.position = notePos1.position;
                    note.GetComponent<Note>().controlPoints = downNoteCurve;
                    break;
            }
        }
        else if (noteType == 1)
        {
            //더블노트 생성
            note = NoteManager.instance.MakeObj("doubleNote");
            theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
            note.gameObject.transform.position = new Vector2(10f, 0);
            note.GetComponent<Note>().controlPoints = doubleNoteCurve;
        }
        else if(noteType == 3) 
        {
            //톱니바퀴 생성
            note = NoteManager.instance.MakeObj("wheel");
            theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
            note.gameObject.transform.position = new Vector2(10f, 0);
            note.GetComponent<Note>().controlPoints = wheelCurve;
        }
    }
}
