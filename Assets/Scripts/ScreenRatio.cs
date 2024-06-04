using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // ���� ī�޶��� ����Ʈ ������ ������
        Rect viewportRect = cam.rect;

        // ���ϴ� ���� ���� ������ ���
        float screenAspectRatio = (float)Screen.width / Screen.height;
        float targetAspectRatio = 16f / 9f; // 16:9

        // ȭ�� ���� ���� ���� ����
        if (screenAspectRatio < targetAspectRatio)
        {
            // ���ΰ� �� �� ��
            viewportRect.height = screenAspectRatio / targetAspectRatio;
            viewportRect.y = (1f - viewportRect.height) / 2f;
        }
        else
        {
            // ���ΰ� �� �� ��
            viewportRect.width = targetAspectRatio / screenAspectRatio;
            viewportRect.x = (1f - viewportRect.width) / 2f;
        }

        // ������ ������ ī�޶� ����
        cam.rect = viewportRect;
    }
}