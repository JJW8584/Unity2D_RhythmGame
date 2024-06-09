using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public TextMeshProUGUI ComboNum;

    void Update()
    {
        //콤보 수를 텍스트로 변환
        ComboNum.text = string.Format("{0:n0}", GameManager.instance.combo);
    }
}
