using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextEffect2 : MonoBehaviour
{
    public RectTransform billboardRect; // ������ �̹����� RectTransform
    public float speed = 100f; // �ؽ�Ʈ �̵� �ӵ�
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
        leftLimit_ = -billboardRect.rect.width / 2;
        rightLimit = billboardRect.rect.width / 2;
        centerPosition = 0;
    }

    void Update()
    {
        MoveChars();
    }

    void CreateCharData()
    {
        string text = uiText.text;
        uiText.text = ""; // ���� �ؽ�Ʈ�� ����Ӵϴ�.

        List<float> charWidths = new List<float>();

        // �� ������ ���� �̸� ����մϴ�.
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

        // �� ���� ��ü�� �����ϰ� �ʱ� ��ġ�� �����մϴ�.
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

            // �ʱ� ��ġ ����
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

            // ���ڰ� ȭ�� ���� ������ ������ ��Ȱ��ȭ
            if (charRect.anchoredPosition.x < leftLimit_)
            {
                charRect.gameObject.SetActive(false);
            }

            // �� ���ڰ� �߾��� ����ߴ��� üũ
            if (!charDataList[i].hasPassedCenter && charRect.anchoredPosition.x < centerPosition)
            {
                charDataList[i].hasPassedCenter = true;
            }

            // ��� ���ڰ� �߾��� ����ߴ��� Ȯ��
            if (!charDataList[i].hasPassedCenter)
            {
                allCharsCurrentlyPassedCenter = false;
            }

            // ���ڰ� leftLimit�� ����� �ٽ� rightLimit�� �̵�
            if (allCharsPassedCenter && charRect.anchoredPosition.x < leftLimit)
            {
                charRect.gameObject.SetActive(true); // �ٽ� Ȱ��ȭ
                charRect.anchoredPosition = new Vector2(rightLimit + charDataList[i].width / 2, charRect.anchoredPosition.y);
            }
        }

        // ��� ���ڰ� �߾��� ���������, ù ������ ��ġ�� leftLimit�� ����
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