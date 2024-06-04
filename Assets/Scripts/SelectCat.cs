using UnityEngine;

public class SelectCat : MonoBehaviour
{
    public GameObject[] charSet;
    public GameObject[] charTextSet;

    private void OnEnable()
    {
        for (int i = 0; i < charSet.Length; i++)
        {
            charSet[i].SetActive(false);
            charTextSet[i].SetActive(false);
        }

        charSet[GameManager.instance.charType].SetActive(true);
        charTextSet[GameManager.instance.charType].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            charSet[GameManager.instance.charType].SetActive(false);
            charTextSet[GameManager.instance.charType].SetActive(false);
            GameManager.instance.charType = --GameManager.instance.charType < 0 ? 2 : GameManager.instance.charType;
            charSet[GameManager.instance.charType].SetActive(true);
            charTextSet[GameManager.instance.charType].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            charSet[GameManager.instance.charType].SetActive(false);
            charTextSet[GameManager.instance.charType].SetActive(false);
            GameManager.instance.charType = ++GameManager.instance.charType > 2 ? 0 : GameManager.instance.charType;
            charSet[GameManager.instance.charType].SetActive(true);
            charTextSet[GameManager.instance.charType].SetActive(true);
        }
    }
}
