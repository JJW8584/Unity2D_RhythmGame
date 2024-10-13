using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ani : MonoBehaviour
{
    public Vector3 minScale;  // 최소 크기
    public Vector3 maxScale;  // 최대 크기
    public float size = 0.03f; //크기 조절
    public float speed = 2f;  // 크기가 변하는 속도
    private bool scalingUp = true;  // 크기가 커지고 있는지 여부

    // Start is called before the first frame update
    void Start()
    {
        minScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        maxScale = minScale + new Vector3(size, size, size);
    }

    // Update is called once per frame
    void Update()
    {
        if (scalingUp)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * speed);
            // 최대 크기에 도달하면 방향을 반대로 변경
            if (Vector3.Distance(transform.localScale, maxScale) < 0.01f)
            {
                scalingUp = false;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, minScale, Time.deltaTime * speed);
            // 최소 크기에 도달하면 방향을 반대로 변경
            if (Vector3.Distance(transform.localScale, minScale) < 0.01f)
            {
                scalingUp = true;
            }
        }
    }
}
