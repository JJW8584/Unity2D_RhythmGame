using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;

    PlayGame PlayGame;
    PlaySong PlaySong;

    void Start()
    {
        PlaySong = FindObjectOfType<PlaySong>();
        PlayGame = FindObjectOfType<PlayGame>();
        GameManager.instance.isPause = false;
    }

    void Update()
    {
        if (GameManager.instance.isTutorial)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TutorialPauseOn();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOn();
            }
        }
    }

    public void PauseOn()
    {
        if (GameManager.instance.isPause == false)
        {
            Time.timeScale = 0f;
            PlayGame.playSong.Pause();
            PlaySong.playSong.Pause();
            GameManager.instance.isPause = true;
            PausePanel.SetActive(true);
            return;
        }
        if (GameManager.instance.isPause == true)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
            GameManager.instance.isPause = false;
            PlayGame.playSong.UnPause();
            PlaySong.playSong.UnPause();
            return;
        }
    }
    public void TutorialPauseOn()
    {
        if (GameManager.instance.isPause == false)
        {
            Time.timeScale = 0f;
            GameManager.instance.isPause = true;
            PausePanel.SetActive(true);
            return;
        }
        if (GameManager.instance.isPause == true)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
            GameManager.instance.isPause = false;
            return;
        }
    }
}
