using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextEffect2 : MonoBehaviour
{
    public RectTransform billboardRect; // 전광판 이미지의 RectTransform
    public float speed = 100f; // 텍스트 이동 속도
    private TextMeshProUGUI uiText;
    private List<CharData> charDataList = new List<CharData>();
    private float leftLimit;
    private float leftLimit_;
    private float rightLimit;
    private float centerPosition;
    private bool allCharsPassedCenter = false;
    private float totalTextWidth = 0f;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        CreateCharData();
        leftLimit = -billboardRect.rect.width / 2;
        leftLimit_ = -billboardRect.rect.width / 2 + 10;
        rightLimit = billboardRect.rect.width / 2 - 10;
        centerPosition = 0;
    }

    void Update()
    {
        MoveChars();
    }

    void CreateCharData()
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
            totalTextWidth += charWidth;

            Destroy(tempCharObj);
        }

        float startX = rightLimit + billboardRect.rect.width / 2;

        // 각 문자 객체를 생성하고 초기 위치를 설정합니다.
        float currentX = startX;
        for (int i = 0; i < text.Length; i++)
        {
            GameObject charObj = new GameObject("Char_" + i);
            charObj.transform.SetParent(transform, false);

            RectTransform charRect = charObj.AddComponent<RectTransform>();
            charRect.localScale = Vector3.one;
            charRect.localRotation = Quaternion.identity;

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

            charDataList.Add(new CharData { rectTransform = charRect, width = charWidths[i], hasPassedCenter = false });
        }
    }

    void MoveChars()
    {
        bool allCharsCurrentlyPassedCenter = true;

        for (int i = 0; i < charDataList.Count; i++)
        {
            RectTransform charRect = charDataList[i].rectTransform;
            charRect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // 글자가 화면 왼쪽 밖으로 나가면 비활성화
            if (charRect.anchoredPosition.x < leftLimit_)
            {
                charRect.gameObject.SetActive(false);
            }

            // 각 글자가 중앙을 통과했는지 체크
            if (!charDataList[i].hasPassedCenter && charRect.anchoredPosition.x < centerPosition)
            {
                charDataList[i].hasPassedCenter = true;
            }

            // 모든 글자가 중앙을 통과했는지 확인
            if (!charDataList[i].hasPassedCenter)
            {
                allCharsCurrentlyPassedCenter = false;
            }

            // 글자가 leftLimit를 벗어나면 다시 rightLimit로 이동
            if (allCharsPassedCenter && charRect.anchoredPosition.x < leftLimit)
            {
                charRect.gameObject.SetActive(true); // 다시 활성화
                charRect.anchoredPosition = new Vector2(rightLimit + charDataList[i].width / 2, charRect.anchoredPosition.y);
            }
        }

        // 모든 글자가 중앙을 통과했으면, 첫 글자의 위치를 leftLimit로 설정
        if (!allCharsPassedCenter && allCharsCurrentlyPassedCenter)
        {
            allCharsPassedCenter = true;

            if (totalTextWidth > billboardRect.rect.width / 2)
            {
                RectTransform firstCharRect = charDataList[0].rectTransform;
                leftLimit = firstCharRect.anchoredPosition.x;
            }
        }
    }

    private class CharData
    {
        public RectTransform rectTransform;
        public float width;
        public bool hasPassedCenter;
    }
}
