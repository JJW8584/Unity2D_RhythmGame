using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // 현재 카메라의 뷰포트 영역을 가져옴
        Rect viewportRect = cam.rect;

        // 원하는 가로 세로 비율을 계산
        float screenAspectRatio = (float)Screen.width / Screen.height;
        float targetAspectRatio = 16f / 9f; // 16:9

        // 화면 가로 세로 비율 조정
        if (screenAspectRatio < targetAspectRatio)
        {
            // 세로가 더 길 때
            viewportRect.height = screenAspectRatio / targetAspectRatio;
            viewportRect.y = (1f - viewportRect.height) / 2f;
        }
        else
        {
            // 가로가 더 길 때
            viewportRect.width = targetAspectRatio / screenAspectRatio;
            viewportRect.x = (1f - viewportRect.width) / 2f;
        }

        // 조정된 비율을 카메라에 설정
        cam.rect = viewportRect;
    }
}
