using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedSetting : MonoBehaviour
{
    public Transform[] controlPoints; // ���ö����� ������ ����
    public TextMeshProUGUI speedText;

    private float t = 0; // ���� ��ġ�� ��Ÿ���� ����
    private int currentSegment = 0;

    private void Awake()
    {
        t = 0;
        currentSegment = 0;
    }
    void Update()
    {
        MoveObjectAlongSpline();
        speedText.text = string.Format("{0:N0}", GameManager.instance.speed * 10);
    }
    private void MoveObjectAlongSpline()
    {
        float distance = GameManager.instance.speed * Time.deltaTime; // �̵� �Ÿ� ���

        // ���ö����� ���� �̵��� �Ÿ���ŭ t�� ������Ŵ
        t += distance;

        // t�� 1 �̻��̸� ���� ���׸�Ʈ�� �Ѿ
        while (t >= 1)
        {
            t -= 1;
            currentSegment++;
            if (currentSegment >= controlPoints.Length - 3)
            {
                currentSegment = 0;
            }
        }

        Vector3 p0 = controlPoints[currentSegment].position;
        Vector3 p1 = controlPoints[currentSegment + 1].position;
        Vector3 p2 = controlPoints[currentSegment + 2].position;
        Vector3 p3 = controlPoints[currentSegment + 3].position;

        transform.position = CalculateCatmullRomPosition(t, p0, p1, p2, p3);
    }

    // Catmull-Rom ���ö��ο��� ���� ����ϴ� �Լ�
    private Vector3 CalculateCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float a0 = -0.5f * t3 + t2 - 0.5f * t;
        float a1 = 1.5f * t3 - 2.5f * t2 + 1.0f;
        float a2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
        float a3 = 0.5f * t3 - 0.5f * t2;

        return a0 * p0 + a1 * p1 + a2 * p2 + a3 * p3;
    }

    public void SpeedDecrease()
    {
        GameManager.instance.speed = GameManager.instance.speed - 0.1f < 1f ? 1f : GameManager.instance.speed - 0.1f;
    }

    public void SpeedIncrease()
    {
        GameManager.instance.speed = GameManager.instance.speed + 0.1f > 3f ? 3f : GameManager.instance.speed + 0.1f;
    }
}