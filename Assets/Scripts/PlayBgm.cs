using UnityEngine;

public class PlayBgm : MonoBehaviour
{
    public AudioSource BgmSong;

    void Awake()
    {
        BgmSong = SoundManager.instance.BgmSound();
        PlayBgmMusic();
    }

    void OnDisable()
    {
        StopBgmMusic();
    }

    public void PlayBgmMusic()
    {
        BgmSong.time = 0;
        BgmSong.Play();
    }

    public void StopBgmMusic()
    {
        BgmSong.Stop();
    }
}
