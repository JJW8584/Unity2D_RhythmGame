using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ani : MonoBehaviour
{
    public Vector3 minScale;  // �ּ� ũ��
    public Vector3 maxScale;  // �ִ� ũ��
    public float size = 0.03f; //ũ�� ����
    public float speed = 2f;  // ũ�Ⱑ ���ϴ� �ӵ�
    private bool scalingUp = true;  // ũ�Ⱑ Ŀ���� �ִ��� ����

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
            // �ִ� ũ�⿡ �����ϸ� ������ �ݴ�� ����
            if (Vector3.Distance(transform.localScale, maxScale) < 0.01f)
            {
                scalingUp = false;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, minScale, Time.deltaTime * speed);
            // �ּ� ũ�⿡ �����ϸ� ������ �ݴ�� ����
            if (Vector3.Distance(transform.localScale, minScale) < 0.01f)
            {
                scalingUp = true;
            }
        }
    }
}