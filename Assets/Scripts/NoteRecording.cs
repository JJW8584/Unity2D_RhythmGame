using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//코드작성 : 지재원
public class NoteRecording : MonoBehaviour
{
    public AudioSource mAudio;
    private string filePath; //파일입력
    private float audioCurTime0;
    private float audioCurTime1;
    private float audioCurTime2;
    private float audioCurTime3;
    private float audioCurTime4;
    private float audioCurTime5;
    private List<Tuple<float, int, int, float>> noteSet; //재생시간 생성위치 노트종류 지속시간

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
        //롱노트 입력
        if(Input.GetKeyDown(KeyCode.G))
        {
            audioCurTime4 = mAudio.time;
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            noteSet.Add(Tuple.Create(audioCurTime4, 0, 0, mAudio.time - audioCurTime4));
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            audioCurTime5 = mAudio.time;
        }
        if(Input.GetKeyUp(KeyCode.H))
        {
            noteSet.Add(Tuple.Create(audioCurTime5, 1, 0, mAudio.time - audioCurTime5));
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            noteSet.Sort();
            RecordNote();
        }
    }

    public void RecordNote()
    {
        filePath = Path.Combine("X:\\", "song1.txt"); 
        for(int i = 0; i < noteSet.Count; i++)
        {
            //재생시간 생성위치 노트종류 지속시간
            string noteData = noteSet[i].Item1.ToString("0.000") + " " + noteSet[i].Item2.ToString() + " " + noteSet[i].Item3.ToString() + " " + noteSet[i].Item4.ToString("0.000");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(noteData);
            }
        }
    }
}
