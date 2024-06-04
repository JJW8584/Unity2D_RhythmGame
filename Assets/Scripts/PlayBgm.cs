using UnityEngine;

public class PlayBgm : MonoBehaviour
{
    public AudioSource BgmSong;

    void Awake()
    {
        BgmSong = SoundManager.instance.BgmSound();
        PlayBgmMusic();
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
