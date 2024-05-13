using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//코드작성 : 지재원
public class PlaySong : MonoBehaviour
{
    public AudioSource playSong;

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StartNote")
        {
            playSong.Play();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}