using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        LoadingSceneManager.LoadScene("PlayScene_0");
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
        LoadingSceneManager.LoadScene("PlayScene_0");
        GameManager.instance.GameManagerReset();
    }
    public void ReStart()
    {
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
