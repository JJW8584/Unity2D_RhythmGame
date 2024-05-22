using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSetting : MonoBehaviour
{
    public Transform[] controlPoints; // 스플라인을 지나는 점들

    private float t = 0; // 현재 위치를 나타내는 변수
    private int currentSegment = 0;

    private void Awake()
    {
        t = 0;
        currentSegment = 0;
    }
    void Update()
    {
        MoveObjectAlongSpline();
    }
    private void MoveObjectAlongSpline()
    {
        float distance = GameManager.instance.speed * Time.deltaTime; // 이동 거리 계산

        // 스플라인을 따라 이동할 거리만큼 t를 증가시킴
        t += distance;

        // t가 1 이상이면 다음 세그먼트로 넘어감
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

    // Catmull-Rom 스플라인에서 점을 계산하는 함수
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
