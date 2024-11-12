using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageEffect : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(0.315f, 0.315f, 0.315f); // 목표 크기
    public float duration = 0.6f; // 스케일 변화에 걸리는 시간
    private Vector3 originalScale; // 원래 크기
    private bool isScaling = false; // 스케일 변화 중인지 여부
    private float elapsedTime = 0f; // 경과 시간

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

            // 첫 번째 절반에서는 원래 크기에서 목표 크기로, 그 이후는 목표 크기에서 원래 크기로 돌아가는 로직
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
                isScaling = false; // 효과 완료
                elapsedTime = 0f;
            }
        }
    }

    public void TriggerScaleEffect()
    {
        if (!isScaling) // 효과가 진행 중이지 않을 때만 시작
        {
            isScaling = true;
            elapsedTime = 0f;
        }
    }
}
