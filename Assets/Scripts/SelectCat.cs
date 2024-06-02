using UnityEngine;

public class SelectCat : MonoBehaviour
{
    public GameObject[] charSet;

    Click_Menu Click_Menu;

    private void Start()
    {
        Click_Menu = FindObjectOfType<Click_Menu>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < charSet.Length; i++)
        {
            charSet[i].SetActive(false);
        }

        charSet[GameManager.instance.charType].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            charSet[GameManager.instance.charType].SetActive(false);
            GameManager.instance.charType = --GameManager.instance.charType < 0 ? 2 : GameManager.instance.charType;
            charSet[GameManager.instance.charType].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            charSet[GameManager.instance.charType].SetActive(false);
            GameManager.instance.charType = ++GameManager.instance.charType > 2 ? 0 : GameManager.instance.charType;
            charSet[GameManager.instance.charType].SetActive(true);
        }
    }
}
