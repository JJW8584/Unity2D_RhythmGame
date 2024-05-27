using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//코드작성 : 지재원
public class NoteRecording : MonoBehaviour
{
    public AudioSource mAudio;
    public string filePath;
    public string fileName; //파일입력
    private float audioCurTime0;
    private float audioCurTime1;
    private float audioCurTime2;
    private float audioCurTime3;
    private float audioCurTime4;
    private float audioCurTime5;
    private string noteData;
    private List<Tuple<float, int, int, float>> noteSet; //재생시간 생성위치 노트종류 지속시간
                                                         //노트종류 0 : 단일노트
                                                         //         1 : 동시노트
                                                         //         2 : 롱노트
                                                         //         3 : 톱니바퀴

    // Start is called before the first frame update
    void Start()
    {
        mAudio.Play();
        noteSet = new List<Tuple<float, int, int, float>>();
    }

    // Update is called once per frame
    void Update()
    {
        //단일노트 입력
        if(Input.GetKeyDown(KeyCode.D))
        {
            audioCurTime0 = mAudio.time;
            noteSet.Add(Tuple.Create(audioCurTime0, 0, 0, 0f));
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            audioCurTime1 = mAudio.time;
            noteSet.Add(Tuple.Create(audioCurTime1, 0, 0, 0f));
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            audioCurTime2 = mAudio.time;
            noteSet.Add(Tuple.Create(audioCurTime2, 1, 0, 0f));
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            audioCurTime3 = mAudio.time;
            noteSet.Add(Tuple.Create(audioCurTime3, 1, 0, 0f));
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioCurTime3 = mAudio.time;
            noteSet.Add(Tuple.Create(audioCurTime3, 0, 3, 0f));
        }
        //롱노트 입력
        if(Input.GetKeyDown(KeyCode.G))
        {
            audioCurTime4 = mAudio.time;
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            noteSet.Add(Tuple.Create(audioCurTime4, 0, 2, mAudio.time - audioCurTime4));
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            audioCurTime5 = mAudio.time;
        }
        if(Input.GetKeyUp(KeyCode.H))
        {
            noteSet.Add(Tuple.Create(audioCurTime5, 1, 2, mAudio.time - audioCurTime5));
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            noteSet.Sort();
            RecordNote();
        }
    }

    public void RecordNote()
    {
        string file = Path.Combine(filePath, fileName); 
        for(int i = 0; i < noteSet.Count; i++)
        {
            //재생시간 생성위치 노트종류 지속시간
            //동시노트 처리
            if ((noteSet[i].Item3 == 0) && (i < noteSet.Count - 1) && (noteSet[i].Item2 != noteSet[i + 1].Item2) && (noteSet[i + 1].Item1 - noteSet[i].Item1 < 0.05f)) 
            //단일노트 && 다음인덱스 존재 && 생성위치 다름 && 입력 시간 차이가 0.05초 미만
            //시간은 소수점 3자리 형식
            {
                noteData = noteSet[i++].Item1.ToString("0.000") + " 0 1 0.000";
            }
            else
            {
                noteData = noteSet[i].Item1.ToString("0.000") + " " + noteSet[i].Item2.ToString() + " " + noteSet[i].Item3.ToString() + " " + noteSet[i].Item4.ToString("0.000");
            }
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                writer.WriteLine(noteData);
            }
        }
    }
}
