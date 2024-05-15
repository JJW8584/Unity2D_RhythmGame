using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float pos;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < pos)
        {
            transform.position = transform.position + new Vector3(-pos+18.8f, 0f, 0f);
        }

    }
}
