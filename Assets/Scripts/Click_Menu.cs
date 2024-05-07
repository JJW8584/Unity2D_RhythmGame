using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Menu : MonoBehaviour
{
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_clicked()
    {
        Menu.SetActive(true);
    }
    public void back_clicked()
    {
        Menu.SetActive(false);
    }
}
