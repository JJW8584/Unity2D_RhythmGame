using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    public Transform[] upNoteCurve;
    public Transform[] downNoteCurve;
    public Transform[] DoubleNoteCurve;
    public GameObject[] songCtrl;
    public TextMeshProUGUI tutorialText;
    public string[] startTextSet;
    public string upNoteText;
    public string downNoteText;
    public string doubleNoteText;
    public string[] endingTextSet;

    private bool isTutorial;
    private bool isUpNote;
    private bool isDownNote;
    private bool isDoubleNote;
    private bool isEnding;
    private int textIndex;
    private float maxTextDelay;
    private float noteDelay;
    private float curTextDelay;

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        isTutorial = true;
        isUpNote = false;
        isDownNote = false;
        isDoubleNote = false;
        isEnding = false;
        maxTextDelay = 2f;
        curTextDelay = 2f;
        noteDelay = 4f;
        textIndex = 0;
        GameManager.instance.GameManagerReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial)
        {
            TutorialText();
        }
        if(isUpNote)
        {
            UpNoteText();
        }
        if(isDownNote)
        {
            DownNoteText();
        }
        if(isDoubleNote)
        {
            DoubleNoteText();
        }
        if(isEnding)
        {
            TutorialExit();
        }
    }

    void TutorialText()
    {
        curTextDelay += Time.deltaTime;
        if (curTextDelay >= maxTextDelay)
        {
            tutorialText.text = startTextSet[textIndex++];
            curTextDelay = 0f;
            if (textIndex >= startTextSet.Length)
            {
                textIndex = 0;
                isTutorial = false;
                isUpNote = true;
                curTextDelay = noteDelay;
                GameManager.instance.GameManagerReset();
            }
        }
    }
    void UpNoteText()
    {
        curTextDelay += Time.deltaTime;
        if(curTextDelay >= noteDelay)
        {
            Debug.Log("UpNote");
            tutorialText.text = upNoteText;
            curTextDelay = 0f;
            GameObject note = NoteManager.instance.MakeObj("note0");
            note.GetComponent<Note>().controlPoints = upNoteCurve;
            note.gameObject.transform.position = upNoteCurve[0].position;
            theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
        }
        if(GameManager.instance.perfectCnt > 0 || GameManager.instance.goodCnt > 0)
        {
            tutorialText.text = "잘했다냥!!";
            isUpNote = false;
            isDownNote = true;
            curTextDelay = noteDelay - 1f;
            GameManager.instance.GameManagerReset();
        }
    }
    void DownNoteText()
    {
        curTextDelay += Time.deltaTime;
        if (curTextDelay >= noteDelay)
        {
            tutorialText.text = downNoteText;
            curTextDelay = 0f;
            GameObject note = NoteManager.instance.MakeObj("note1");
            note.GetComponent<Note>().controlPoints = downNoteCurve;
            note.gameObject.transform.position = downNoteCurve[0].position;
            theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
        }
        if (GameManager.instance.perfectCnt > 0 || GameManager.instance.goodCnt > 0)
        {
            
            tutorialText.text = "잘했다냥!!";
            isDownNote = false;
            isDoubleNote = true;
            curTextDelay = noteDelay - 1f;
            GameManager.instance.GameManagerReset();
        }
    }
    void DoubleNoteText()
    {
        curTextDelay += Time.deltaTime;
        if (curTextDelay >= noteDelay)
        {
            tutorialText.text = doubleNoteText;
            curTextDelay = 0f;
            GameObject note = NoteManager.instance.MakeObj("doubleNote");
            note.GetComponent<Note>().controlPoints = DoubleNoteCurve;
            note.gameObject.transform.position = DoubleNoteCurve[0].position;
            theTimingManager.boxNoteList.Add(note); //타이밍 리스트에 추가
        }
        if (GameManager.instance.perfectCnt > 0 || GameManager.instance.goodCnt > 0)
        {
            tutorialText.text = "잘했다냥!!";
            isDoubleNote = false;
            isEnding = true;
            GameManager.instance.GameManagerReset();
            curTextDelay = 0f;
        }
    }
    void TutorialExit()
    {
        curTextDelay += Time.deltaTime;
        if (curTextDelay >= maxTextDelay)
        {
            tutorialText.text = endingTextSet[textIndex++];
            curTextDelay = 0f;
            if (textIndex >= endingTextSet.Length)
            {
                GameManager.instance.isTutorial = false;
                GameManager.instance.GameManagerReset();
                LoadingSceneManager.LoadScene("StartScene");
            }
        }
    }
}
