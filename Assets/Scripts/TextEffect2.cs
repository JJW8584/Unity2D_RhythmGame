using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextEffect2 : MonoBehaviour
{
    public RectTransform billboardRect; // 전광판 이미지의 RectTransform
    public float speed = 100f; // 텍스트 이동 속도
    private TextMeshProUGUI uiText;
    private List<RectTransform> charRects = new List<RectTransform>();
    private bool allCharsPassedCenter = false;
    private float totalWidth = 0f; // 전체 텍스트의 폭
    private float leftLimit;
    private float rightLimit;
    private float centerPosition;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        CreateCharRects();
        leftLimit = -billboardRect.rect.width / 2;
        rightLimit = billboardRect.rect.width / 2;
        centerPosition = 0;
    }

    void Update()
    {
        MoveChars();
    }

    void CreateCharRects()
    {
        string text = uiText.text;
        uiText.text = ""; // 원본 텍스트는 비워둡니다.

        List<float> charWidths = new List<float>();

        // 각 문자의 폭을 미리 계산합니다.
        for (int i = 0; i < text.Length; i++)
        {
            GameObject tempCharObj = new GameObject("TempChar_" + i);
            TextMeshProUGUI tempCharText = tempCharObj.AddComponent<TextMeshProUGUI>();
            tempCharText.font = uiText.font;
            tempCharText.fontSize = uiText.fontSize;
            tempCharText.text = text[i].ToString();

            float charWidth = tempCharText.preferredWidth;
            charWidths.Add(charWidth);
            totalWidth += charWidth;

            Destroy(tempCharObj);
        }

        float startX = rightLimit + totalWidth / 2;

        // 각 문자 객체를 생성하고 초기 위치를 설정합니다.
        float currentX = startX;
        for (int i = 0; i < text.Length; i++)
        {
            GameObject charObj = new GameObject("Char_" + i);
            charObj.transform.SetParent(transform, false); // 부모 객체의 로컬 스케일과 회전을 유지

            RectTransform charRect = charObj.AddComponent<RectTransform>();
            charRect.localScale = Vector3.one;
            charRect.localRotation = Quaternion.identity; // 로컬 회전을 초기화

            TextMeshProUGUI charText = charObj.AddComponent<TextMeshProUGUI>();
            charText.font = uiText.font;
            charText.fontSize = uiText.fontSize;
            charText.color = uiText.color;
            charText.alignment = TextAlignmentOptions.Center;
            charText.text = text[i].ToString();

            charRect.sizeDelta = new Vector2(charWidths[i], uiText.rectTransform.sizeDelta.y);

            // 초기 위치 설정
            charRect.anchoredPosition = new Vector2(currentX - charWidths[i] / 2, 0);
            currentX += charWidths[i];

            charRects.Add(charRect);
        }
    }

    public bool oneTime = false;
    void MoveChars()
    {
        for (int i = 0; i < charRects.Count; i++)
        {
            RectTransform charRect = charRects[i];
            charRect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // 모든 글자가 중앙을 통과했는지 체크
            if (!allCharsPassedCenter && charRect.anchoredPosition.x < centerPosition)
            {
                allCharsPassedCenter = true;
            }

            // 글자가 leftLimit를 벗어나면 다시 rightLimit로 이동
            if (allCharsPassedCenter)
            {
                if (!oneTime)
                {
                    oneTime = true;
                    RectTransform firstCharRect = charRects[0];
                    leftLimit = firstCharRect.anchoredPosition.x;
                }
                if(charRect.anchoredPosition.x < leftLimit)
                {
                    charRect.anchoredPosition = new Vector2(rightLimit, charRect.anchoredPosition.y);
                }
            }
        }
    }

    /*public RectTransform billboardRect; // 전광판 이미지의 RectTransform
    public float speed = 100f; // 텍스트 이동 속도
    private TextMeshProUGUI uiText;
    private List<RectTransform> charRects = new List<RectTransform>();
    private bool allCharsPassedCenter = false;
    private float totalWidth = 0f; // 전체 텍스트의 폭
    private float leftLimit;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        CreateCharRects();
    }

    void Update()
    {
        MoveChars();
    }

    void CreateCharRects()
    {
        string text = uiText.text;
        uiText.text = ""; // 원본 텍스트는 비워둡니다.

        List<float> charWidths = new List<float>();

        // 각 문자의 폭을 미리 계산합니다.
        for (int i = 0; i < text.Length; i++)
        {
            GameObject tempCharObj = new GameObject("TempChar_" + i);
            TextMeshProUGUI tempCharText = tempCharObj.AddComponent<TextMeshProUGUI>();
            tempCharText.font = uiText.font;
            tempCharText.fontSize = uiText.fontSize;
            tempCharText.text = text[i].ToString();

            float charWidth = tempCharText.preferredWidth;
            charWidths.Add(charWidth);
            totalWidth += charWidth;

            Destroy(tempCharObj);
        }

        float startX = billboardRect.rect.width / 2 + totalWidth / 2;

        // 각 문자 객체를 생성하고 초기 위치를 설정합니다.
        float currentX = startX;
        for (int i = text.Length - 1; i >= 0; i++)
        {
            GameObject charObj = new GameObject("Char_" + i);
            charObj.transform.SetParent(transform);

            RectTransform charRect = charObj.AddComponent<RectTransform>();
            charRect.localScale = Vector3.one;
            //charRect.localRotation = Quaternion.identity; // 로컬 회전을 초기화

            TextMeshProUGUI charText = charObj.AddComponent<TextMeshProUGUI>();
            charText.font = uiText.font;
            charText.fontSize = uiText.fontSize;
            charText.color = uiText.color;
            charText.alignment = TextAlignmentOptions.Center;
            charText.text = text[i].ToString();

            charRect.sizeDelta = new Vector2(charWidths[i], uiText.rectTransform.sizeDelta.y);

            // 초기 위치 설정
            charRect.anchoredPosition = new Vector2(currentX - charWidths[i] / 2, 0);
            currentX -= charWidths[i];

            charRects.Add(charRect);
        }

        //leftLimit = -billboardRect.rect.width / 2 - totalWidth; // 초기 leftLimit 설정
    }

    void MoveChars()
    {
        float rightLimit = billboardRect.rect.width / 2;
        float centerPosition = 0; // 전광판 중앙의 X 좌표

        bool allPassed = true;

        for (int i = 0; i < charRects.Count; i++)
        {
            RectTransform charRect = charRects[i];
            charRect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // 각 글자가 중앙을 통과했는지 체크
            if (charRect.anchoredPosition.x > centerPosition)
            {
                allPassed = false;
            }
        }

        // 모든 글자가 중앙을 통과했는지 여부를 업데이트
        if (allPassed)
        {
            allCharsPassedCenter = true;
        }

        // 모든 글자가 중앙을 통과했을 때 제일 앞에 있는 문자의 x값으로 leftLimit를 다시 설정
        if (allCharsPassedCenter)
        {
            RectTransform firstCharRect = charRects[0];
            leftLimit = firstCharRect.anchoredPosition.x;

            for (int i = 0; i < charRects.Count; i++)
            {
                RectTransform charRect = charRects[i];

                // 문자가 leftLimit를 통과하면 다시 오른쪽 끝으로 이동
                if (charRect.anchoredPosition.x < leftLimit)
                {
                    charRect.anchoredPosition = new Vector2(rightLimit + (charRect.anchoredPosition.x - leftLimit), charRect.anchoredPosition.y);
                }
            }
        }
    }*/
}
