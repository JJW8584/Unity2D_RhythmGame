using UnityEngine;

//코드작성: 권지수
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
        //튜토리얼 이라면
        if (GameManager.instance.isTutorial)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TutorialPauseOn();
            }
        }
        //튜토리얼이 아니라면
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
        //일시정지 상태가 아니라면 > 일시정지 하기
        if (GameManager.instance.isPause == false)
        {
            Time.timeScale = 0f; //게임 시간 멈춤
            PlayGame.playSong.Pause();
            PlaySong.playSong.Pause(); //노래 일시정지
            GameManager.instance.isPause = true;
            PausePanel.SetActive(true); //일시정지 패널 활성화
            return;
        }
        //일시정지 상태라면 > 일시정지 풀기
        if (GameManager.instance.isPause == true)
        {
            PausePanel.SetActive(false); //일시정지 패널 비활성화
            Time.timeScale = 1f; //게임 시간 재개
            GameManager.instance.isPause = false;
            PlayGame.playSong.UnPause();
            PlaySong.playSong.UnPause(); //노래 재개
            return;
        }
    }
    public void TutorialPauseOn()
    {
        //일시정지 상태가 아니라면 > 일시정지 하기
        if (GameManager.instance.isPause == false)
        {
            Time.timeScale = 0f; //게임 시간 멈춤
            GameManager.instance.isPause = true;
            PausePanel.SetActive(true); //일시정지 패널 활성화
            return;
        }
        //일시정지 상태라면 > 일시정지 풀기
        if (GameManager.instance.isPause == true)
        {
            PausePanel.SetActive(false); //일시정지 패널 비활성화
            Time.timeScale = 1f; //게임 시간 재개
            GameManager.instance.isPause = false;
            return;
        }
    }
}
