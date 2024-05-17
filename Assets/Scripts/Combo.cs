using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public TextMeshProUGUI ComboNum;

    // Update is called once per frame
    void Update()
    {
        ComboNum.text = string.Format("{0:n0}", GameManager.instance.combo);
    }
}
