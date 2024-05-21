using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;

    NoteManager NoteManager;
    PlaySong PlaySong;

    void Start()
    {
        PlaySong = FindObjectOfType<PlaySong>();
        NoteManager = FindObjectOfType<NoteManager>();
        GameManager.instance.isPause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOn();
        }
    }

    public void PauseOn()
    {
        if (GameManager.instance.isPause == false)
        {
            Time.timeScale = 0f;
            NoteManager.playSong.Pause();
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
            NoteManager.playSong.UnPause();
            PlaySong.playSong.UnPause();
            return;
        }
    }
}
