using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ڵ��ۼ� : �����
public class Note : MonoBehaviour
{
    public int speed = 7;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}