using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SelectCat : MonoBehaviour
{
    public GameObject[] charSet;

    private void OnEnable()
    {
        for (int i = 0; i < charSet.Length; i++)
        {
            charSet[i].SetActive(false);
        }

        charSet[GameManager.instance.charType].SetActive(true);
    }
}
