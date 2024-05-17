using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//코드작성 : 지재원
public class PlaySong : MonoBehaviour
{
    public AudioSource playSong;

    private bool isSongPlaying;

    private void Awake()
    {
        gameObject.SetActive(true);
        isSongPlaying = true;
    }
    private void Update()
    {
        if(!playSong.isPlaying && !isSongPlaying)
        {
            LoadingSceneManager.LoadScene("EndingScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StartNote")
        {
            playSong.Play();
            collision.gameObject.SetActive(false);
            isSongPlaying = false;
        }
    }
}