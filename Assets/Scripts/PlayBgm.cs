using UnityEngine;

//코드작성: 권지수
public class PlayBgm : MonoBehaviour
{
    public AudioSource BgmSong;

    void Awake()
    {
        BgmSong = SoundManager.instance.BgmSound();
        PlayBgmMusic();
    }

    //음악 재생
    public void PlayBgmMusic()
    {
        BgmSong.time = 0;   //음악 재생 위치 초기화
        BgmSong.Play(); //음악 재생
    }

    //음악 멈춤
    public void StopBgmMusic()
    {
        BgmSong.Stop();
    }
}
