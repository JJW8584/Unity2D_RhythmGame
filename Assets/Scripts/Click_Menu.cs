using UnityEngine;

//코드작성 : 권지수
public class Click_Menu : MonoBehaviour
{
    public GameObject[] charSet;
    public GameObject Menu;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlaySound("Click");
        }
    }
    public void total_btn_clicked()
    {
        if (!Menu.activeSelf)
        {
            Menu.SetActive(true);
        }
        else if (Menu.activeSelf)
        {
            Menu.SetActive(false);
        }
    }

    public void btn_clicked()
    {
        Menu.SetActive(true);
    }
    public void back_clicked()
    {
        Menu.SetActive(false);
    }
    //코드작성 : 지재원
    //시작
    public void startButton()
    {
        switch(GameManager.instance.songType)
        {
            case 0:
                LoadingSceneManager.LoadScene("PlayScene_0");
                break;
            case 1:
                LoadingSceneManager.LoadScene("PlayScene_1");
                break;
            case 2:
                LoadingSceneManager.LoadScene("PlayScene_2");
                break;
            case 3:
                LoadingSceneManager.LoadScene("PlayScene_3");
                break;
        }
    }
    public void CharacterSelectLeft()
    {
        charSet[GameManager.instance.charType].SetActive(false);
        GameManager.instance.charType = --GameManager.instance.charType < 0 ? 2 : GameManager.instance.charType;
        charSet[GameManager.instance.charType].SetActive(true);
    }   
    public void CharacterSelectRight()
    {
        charSet[GameManager.instance.charType].SetActive(false);
        GameManager.instance.charType = ++GameManager.instance.charType > 2 ? 0 : GameManager.instance.charType;
        charSet[GameManager.instance.charType].SetActive(true);
    }
    public void Tutorial()
    {
        LoadingSceneManager.LoadScene("TutorialScene");
        GameManager.instance.isTutorial = true;
        GameManager.instance.GameManagerReset();
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        switch (GameManager.instance.songType)
        {
            case 0:
                LoadingSceneManager.LoadScene("PlayScene_0");
                break;
            case 1:
                LoadingSceneManager.LoadScene("PlayScene_1");
                break;
            case 2:
                LoadingSceneManager.LoadScene("PlayScene_2");
                break;
            case 3:
                LoadingSceneManager.LoadScene("PlayScene_3");
                break;
        }
        GameManager.instance.GameManagerReset();
    }
    public void ReStart()
    {
        Time.timeScale = 1f;
        LoadingSceneManager.LoadScene("StartScene");
        GameManager.instance.isTutorial = false;
        GameManager.instance.GameManagerReset();
    }
    public void EndBtn()
    {
        Time.timeScale = 1f;
        LoadingSceneManager.LoadScene("EndingScene");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    //끝
}
