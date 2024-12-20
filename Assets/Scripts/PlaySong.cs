using UnityEngine;

//코드작성 : 지재원
public class PlaySong : MonoBehaviour
{
    public AudioSource playSong;

    private bool isSongPlaying;

    private void Awake()
    {
        gameObject.SetActive(true);
        playSong = SoundManager.instance.PlayBgmSound(GameManager.instance.songType);
        playSong.time = 0;
        isSongPlaying = true;
        GameManager.instance.isPlay = true;
    }

    private void Update()
    {
        if(!playSong.isPlaying && !isSongPlaying && !GameManager.instance.isPause)
        {
            GameManager.instance.isPlay = false;
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

    public void SongStop()
    {
        playSong.Stop();
    }
}