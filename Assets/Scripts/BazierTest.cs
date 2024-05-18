using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierTest : MonoBehaviour
{
    public Transform[] controlPoints; // ���ö����� ������ ����
    public int segments = 5; // �� �� ������ ���׸�Ʈ ��

    private void OnDrawGizmos()
    {
        if (controlPoints == null || controlPoints.Length < 4)
        {
            Debug.LogWarning("Catmull-Rom ���ö����� �׸����� �ּ� 4���� ��Ʈ�� ����Ʈ�� �ʿ��մϴ�.");
            return;
        }

        // Gizmos�� ����Ͽ� ���ö����� �׸��ϴ�.
        Gizmos.color = Color.green;
        for (int i = 0; i < controlPoints.Length - 3; i++)
        {
            Vector3 p0 = controlPoints[i].position;
            Vector3 p1 = controlPoints[i + 1].position;
            Vector3 p2 = controlPoints[i + 2].position;
            Vector3 p3 = controlPoints[i + 3].position;

            Vector3 previousPosition = p1;
            for (int j = 1; j <= segments; j++)
            {
                float t = j / (float)segments;
                Vector3 newPosition = CalculateCatmullRomPosition(t, p0, p1, p2, p3);
                Gizmos.DrawLine(previousPosition, newPosition);
                previousPosition = newPosition;
            }
        }
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
}