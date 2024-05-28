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
            transform.position = transform.position + new Vector3(pos * -2, 0f, 0f);
        }

    }
}
