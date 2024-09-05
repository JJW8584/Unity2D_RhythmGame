using UnityEngine;

//코드작성: 권지수
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgm_player;
    public AudioSource sfx_player;
    public AudioSource playSong_player;

    public AudioClip bgm_audio_clips;
    public AudioClip[] PlaySong_audio_clips;
    public AudioClip[] audio_clips;

    private void Awake()
    {
        //볼륨매니저 싱글톤 생성
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void PlaySound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "JUMP": index = 0; break;
            case "ATTACK": index = 1; break;
            case "HIT": index = 2; break;
            case "Click": index = 3; break;
        }

        sfx_player.clip = audio_clips[index];
        sfx_player.Play();
    }

    public AudioSource PlayBgmSound(int i)
    {
        playSong_player.clip = PlaySong_audio_clips[i];
        return playSong_player;
    }

    public AudioSource BgmSound()
    {
        bgm_player.clip = bgm_audio_clips;
        return bgm_player;
    }
}
