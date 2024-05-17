using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//코드작성 : 권지수
public class Click_Menu : MonoBehaviour
{
    public GameObject[] charSet;
    public GameObject Menu;
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
    //끝
}
