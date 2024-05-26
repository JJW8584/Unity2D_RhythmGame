

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect), typeof(Mask), typeof(Image))]
public class TextEffect2 : MonoBehaviour
{
    enum ScrollMoveDirection
    {
        ToFront,
        ToEnd,
    }

    private ScrollRect scrollRect;
    private Mask mask;
    private Image maskImage;
    private RectTransform rectTransform; // RectTransform cache

    [Header("Text Component in child"), SerializeField]
    private Text text;

    [Header("If true, Text will go back to front immediately")]
    public bool scrollToFrontImmediately;

    [Header("How much will text go back to front?"), Range(0.001f, 1f)]
    public float scrollToFrontSpeed = 0.005f;

    [Header("How much will text move to end each update"), Range(0.001f, 1f)]
    public float scrollToEndSpeed = 0.005f;

    [Header("How long to pause at the end of the text?")]
    public float endStopTime = 0.5f;
    private float currentEndStopTime = 0.0f;

    [Header("How long to pause at the front of the text?")]
    public float frontStopTime = 0.5f;
    private float currentFrontStopTime = 0.0f;


    private ScrollMoveDirection direction = ScrollMoveDirection.ToEnd;
    private Color maskImageColor = new Color(1f, 1f, 1f, 0.01f);
    private float position = 0.0f;

    private void Awake()
    {
        if (!scrollRect)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
        if (!mask)
        {
            mask = GetComponent<Mask>();
        }
        if (!maskImage)
        {
            maskImage = GetComponent<Image>();
        }
        if (!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        maskImage.raycastTarget = false;
        mask.enabled = true;
        text.maskable = true;
    }

    private void OnValidate()
    {
        if (!scrollRect)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
        if (!mask)
        {
            mask = GetComponent<Mask>();
        }
        if (!maskImage)
        {
            maskImage = GetComponent<Image>();
        }
        if (!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        if (scrollRect && !(scrollRect.content))
        {
            GameObject go = new GameObject("Text");
            text = go.AddComponent<Text>();
            text.text = "";
            text.alignment = TextAnchor.MiddleCenter;
            text.raycastTarget = false; // Set this true if need to raycast text.

            RectTransform textRectTransform = text.rectTransform;
            textRectTransform.parent = transform;
            textRectTransform.sizeDelta = new Vector2(text.preferredWidth, rectTransform.rect.height);
            textRectTransform.localPosition = Vector2.zero;

            scrollRect.content = textRectTransform;
        }

        // Turn off the mask so that you can see the text in the editor.
        if (mask)
        {
            mask.showMaskGraphic = false;
        }
        if (text)
        {
            text.maskable = false;
        }
        if (maskImage)
        {
            maskImage.color = maskImageColor;
        }
    }

    private void LateUpdate()
    {
        if (Mathf.Approximately(position, 1.0f))
        {
            // Reset the stop timer before text goes front
            currentFrontStopTime = 0f;
            direction = ScrollMoveDirection.ToFront;
        }
        else if (Mathf.Approximately(position, 0.0f))
        {
            // Reset the stop timer before text goes end
            currentEndStopTime = 0f;
            direction = ScrollMoveDirection.ToEnd;
        }

        switch (direction)
        {
            case ScrollMoveDirection.ToFront:
                ScrollTextToFront();
                break;
            case ScrollMoveDirection.ToEnd:
                ScrollTextToEnd();
                break;
        }

        scrollRect.horizontalNormalizedPosition = position;
    }

    private void ScrollTextToFront()
    {
        if (scrollToFrontImmediately)
        {
            position = 0;
            return;
        }

        if (currentEndStopTime < endStopTime)
        {
            currentEndStopTime += Time.deltaTime;
        }
        else
        {
            position = Mathf.Clamp(position - scrollToFrontSpeed, 0f, 1f);
        }
    }

    private void ScrollTextToEnd()
    {
        if (currentFrontStopTime < frontStopTime)
        {
            currentFrontStopTime += Time.deltaTime;
        }
        else
        {
            position = Mathf.Clamp(position + scrollToEndSpeed, 0f, 1f);
        }
    }
}*/

/*using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextEffect2 : MonoBehaviour
{
    public RectTransform billboardRect; // 전광판 이미지의 RectTransform
    public float speed = 100f; // 텍스트 이동 속도
    private TextMeshProUGUI uiText;
    private List<CharData> charDataList = new List<CharData>();
    private float leftLimit;
    private float rightLimit;
    private float centerPosition;
    private bool allCharsPassedCenter = false;
    private float totalTextWidth = 0f;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        CreateCharData();
        leftLimit = -billboardRect.rect.width / 2;
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
        uiText.text = ""; // 원본 텍스트는 비워둡니다.

        List<float> charWidths = new List<float>();

        // 각 문자의 폭을 미리 계산합니다.
        for (int i = 0; i < text.Length; i++)
        {
            // TMP_Text의 GetPreferredValues를 사용하여 문자 폭을 계산합니다.
            Vector2 charSize = uiText.GetPreferredValues(text[i].ToString());
            float charWidth = charSize.x;
            charWidths.Add(charWidth);
            totalTextWidth += charWidth;
        }

        // 각 문자 객체를 생성하고 초기 위치를 설정합니다.
        float currentX = rightLimit;
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
            charRect.anchoredPosition = new Vector2(currentX + charWidths[i] / 2, 0);
            currentX += charWidths[i];

            charDataList.Add(new CharData { rectTransform = charRect, width = charWidths[i], hasPassedCenter = false });
        }
    }
    
    void MoveChars()
    {
        bool allCharsCurrentlyPassedCenter = true;
        float totalRightLimit = rightLimit;

        for (int i = 0; i < charDataList.Count; i++)
        {
            RectTransform charRect = charDataList[i].rectTransform;
            charRect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

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
                // 모든 글자가 중앙을 통과한 후에 rightLimit로 이동시킬 때 위치를 정확히 설정
                
                charRect.anchoredPosition = new Vector2(totalRightLimit + charDataList[i].width / 2, 0);
                totalRightLimit += charDataList[i].width;
            }
        }

        // 모든 글자가 중앙을 통과했으면, 첫 글자의 위치를 leftLimit로 설정
        if (!allCharsPassedCenter && allCharsCurrentlyPassedCenter)
        {
            allCharsPassedCenter = true;

            if (totalTextWidth > billboardRect.rect.width)
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
*/

