using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageEffect : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(0.315f, 0.315f, 0.315f); // ��ǥ ũ��
    public float duration = 0.6f; // ������ ��ȭ�� �ɸ��� �ð�
    private Vector3 originalScale; // ���� ũ��
    private bool isScaling = false; // ������ ��ȭ ������ ����
    private float elapsedTime = 0f; // ��� �ð�

    void OnEnable()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        TriggerScaleEffect();

        if (isScaling)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // ù ��° ���ݿ����� ���� ũ�⿡�� ��ǥ ũ���, �� ���Ĵ� ��ǥ ũ�⿡�� ���� ũ��� ���ư��� ����
            if (progress < 0.5f)
            {
                transform.localScale = Vector3.Lerp(originalScale, targetScale, progress * 2);
            }
            else if (progress < 1f)
            {
                transform.localScale = Vector3.Lerp(targetScale, originalScale, (progress - 0.5f) * 2);
            }
            else
            {
                transform.localScale = originalScale;
                isScaling = false; // ȿ�� �Ϸ�
                elapsedTime = 0f;
            }
        }
    }

    public void TriggerScaleEffect()
    {
        if (!isScaling) // ȿ���� ���� ������ ���� ���� ����
        {
            isScaling = true;
            elapsedTime = 0f;
        }
    }
}