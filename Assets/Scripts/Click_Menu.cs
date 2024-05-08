using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//코드작성 : 권지수
public class Click_Menu : MonoBehaviour
{
    public GameObject Menu;
    public void btn_clicked()
    {
        Menu.SetActive(true);
    }
    public void back_clicked()
    {
        Menu.SetActive(false);
    }
}
