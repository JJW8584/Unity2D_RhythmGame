using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public Transform[] upNoteCurve;
    public Transform[] downNoteCurve;
    public Transform[] doubleNoteCurve;

    //��Ʈ ���� ����
    private List<Tuple<float, int, int, float>> noteData;
    private bool[] noteCreateCheck; //��Ʈ�� �����Ǿ����� üũ
    public string songName;
    public Transform notePos0;
    public Transform notePos1;

    //�÷��� �뷡
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

        TextAsset textFile = Resources.Load(songName) as TextAsset; //�ؽ�Ʈ�� ���� �о��
        StringReader stringReader = new StringReader(textFile.text);

        string line;
        while ((line = stringReader.ReadLine()) != null) //�� �پ� ����
        {
            // ������ �������� ���ڿ��� �и��Ͽ� ������ ������ ��ȯ�Ͽ� Tuple ����
            string[] values = line.Split(' ');
            if (values.Length == 4)
            {
                if (float.TryParse(values[0], out float val1) && //�� ��ȯ �� ����Ʈ�� �߰�
                    int.TryParse(values[1], out int val2) &&
                    int.TryParse(values[2], out int val3) &&
                    float.TryParse(values[3], out float val4))
                {
                    noteData.Add(new Tuple<float, int, int, float>(val1, val2, val3, val4));
                }
            }
        }
        noteCreateCheck = new bool[noteData.Count];
        for (int i = 0; i < noteData.Count; i++) //��Ʈ ���� üũ �ʱ�ȭ
        {
            noteCreateCheck[i] = false;
        }
    }

    private void GamePlay()
    {
        for (int i = 0; i < noteData.Count; i++)
        {
            if ((playSong.time >= noteData[i].Item1) && !noteCreateCheck[i]) //��Ʈ�� �������� �ʾ��� �� �ð��� ���缭 ����
            {
                //Debug.Log(noteData[i].Item1);
                noteCreateCheck[i] = true;
                switch (noteData[i].Item3)
                {
                    //����ð� ������ġ ��Ʈ���� ���ӽð�
                    case 0: //���ϳ�Ʈ
                        CreateNote(noteData[i].Item2, noteData[i].Item3);
                        break;
                    case 1: //���߳�Ʈ
                        CreateNote(noteData[i].Item2, noteData[i].Item3);
                        break;
                    case 2: //�ճ�Ʈ
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
                case 0: //����
                    note = NoteManager.instance.MakeObj("note0");
                    theTimingManager.boxNoteList.Add(note); //Ÿ�̹� ����Ʈ�� �߰�
                    note.gameObject.transform.position = notePos0.position;
                    note.GetComponent<Note>().controlPoints = upNoteCurve;
                    break;
                case 1: //�Ʒ���
                    note = NoteManager.instance.MakeObj("note1");
                    theTimingManager.boxNoteList.Add(note); //Ÿ�̹� ����Ʈ�� �߰�
                    note.gameObject.transform.position = notePos1.position;
                    note.GetComponent<Note>().controlPoints = downNoteCurve;
                    break;
            }
        }
        else if (noteType == 1)
        {
            //������Ʈ ����
            note = NoteManager.instance.MakeObj("doubleNote");
            theTimingManager.boxNoteList.Add(note); //Ÿ�̹� ����Ʈ�� �߰�
            note.gameObject.transform.position = new Vector2(10f, 0);
            note.GetComponent<Note>().controlPoints = doubleNoteCurve;
        }
    }
}