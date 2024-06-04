using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testView : MonoBehaviour
{
    public GameObject[] playSongListBox;
    // Start is called before the first frame update
    void Start()
    {
        testSomting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testSomting()
    {
        for(int i = 0; i < playSongListBox.Length; i++)
        {
            Camera mainCamera = Camera.main;
            Vector3 currentWorldPosition = playSongListBox[i].transform.position;
            Vector3 currentViewportPosition = mainCamera.WorldToViewportPoint(currentWorldPosition);
            Debug.Log(currentViewportPosition.x + " " + currentViewportPosition.y + " " + currentViewportPosition.z);
        }
    }
}
