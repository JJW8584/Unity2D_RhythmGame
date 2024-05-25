using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialSet;
    public GameObject[] songCtrl;
    public TextMeshProUGUI tutorialText;
    public string[] startTextSet;
    public string upNoteText;
    public string downNoteText;
    public string doubleNoteText;
    public string[] endingTextSet;

    private bool isTutorial;
    private bool isSongStart;
    private int textIndex;
    private float maxTextDelay;
    private float curTextDelay;

    void Start()
    {
        isTutorial = true;
        isSongStart = false;
        maxTextDelay = 2f;
        curTextDelay = 2f;
        textIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial)
        {
            TutorialText();
        }
        if(isSongStart)
        {
            tutorialSet.SetActive(true);
            isSongStart = false;
        }
        if(GameObject.FindWithTag("Note0") != null && GameObject.FindWithTag("Note0").transform.position.x <= -4.86)
        {
            GameManager.instance.isPause = true;
            UpNoteText();
        }
        if(GameObject.FindWithTag("Note1") != null && GameObject.FindWithTag("Note0").transform.position.x <= -4.86)
        {
            GameManager.instance.isPause = true;
            DownNoteText();
        }
        if(GameObject.FindWithTag("DoubleNote") != null && GameObject.FindWithTag("Note0").transform.position.x <= -4.86)
        {
            GameManager.instance.isPause = true;
            DoubleNoteText();
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
                isSongStart = true;
            }
        }
    }
    void UpNoteText()
    {
        tutorialText.text = upNoteText;
        Time.timeScale = 0f;
        songCtrl[0].GetComponent<PlayGame>().playSong.Pause();
        songCtrl[1].GetComponent<PlaySong>().playSong.Pause();
        if(GameObject.FindWithTag("Note0") == null)
        {
            GameManager.instance.isPause = false;
            Time.timeScale = 1f;
            tutorialText.text = "잘했다냥!!";
        }
    }
    void DownNoteText()
    {
        tutorialText.text = downNoteText;
        Time.timeScale = 0f;
        songCtrl[0].GetComponent<PlayGame>().playSong.Pause();
        songCtrl[1].GetComponent<PlaySong>().playSong.Pause();
        if (GameObject.FindWithTag("Note1") == null)
        {
            GameManager.instance.isPause = false;
            Time.timeScale = 1f;
            tutorialText.text = "잘했다냥!!";
        }
    }
    void DoubleNoteText()
    {
        tutorialText.text = doubleNoteText;
        Time.timeScale = 0f;
        songCtrl[0].GetComponent<PlayGame>().playSong.Pause();
        songCtrl[0].GetComponent<PlaySong>().playSong.Pause();
        if (GameObject.FindWithTag("DoubleNote") == null)
        {
            GameManager.instance.isPause = false;
            Time.timeScale = 1f;
            tutorialText.text = "잘했다냥!!";
        }
    }
}
