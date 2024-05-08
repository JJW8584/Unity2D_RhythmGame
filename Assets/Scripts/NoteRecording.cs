using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//�ڵ��ۼ� : �����
public class NoteRecording : MonoBehaviour
{
    public AudioSource mAudio;
    public string filePath;
    public string fileName; //�����Է�
    private float audioCurTime0;
    private float audioCurTime1;
    private float audioCurTime2;
    private float audioCurTime3;
    private float audioCurTime4;
    private float audioCurTime5;
    private string noteData;
    private List<Tuple<float, int, int, float>> noteSet; //����ð� ������ġ ��Ʈ���� ���ӽð�
                                                         //��Ʈ���� 0 : ���ϳ�Ʈ
                                                         //         1 : ���ó�Ʈ
                                                         //         2 : �ճ�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        mAudio.Play();
        noteSet = new List<Tuple<float, int, int, float>>();
    }

    // Update is called once per frame
    void Update()
    {
        //���ϳ�Ʈ �Է�
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
        //�ճ�Ʈ �Է�
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
            //����ð� ������ġ ��Ʈ���� ���ӽð�
            //���ó�Ʈ ó��
            if ((noteSet[i].Item3 == 0) && (i < noteSet.Count - 1) && (noteSet[i].Item2 != noteSet[i + 1].Item2) && (noteSet[i + 1].Item1 - noteSet[i].Item1 < 0.05f)) 
            //���ϳ�Ʈ && �����ε��� ���� && ������ġ �ٸ� && �Է� �ð� ���̰� 0.05�� �̸�
            //�ð��� �Ҽ��� 3�ڸ� ����
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