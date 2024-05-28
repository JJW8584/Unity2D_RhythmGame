using UnityEngine;

//코드작성: 권지수
public class ParticleControl : MonoBehaviour
{

    public float ParticleLife = 1;

    void Update()
    {
        ParticleLife -= Time.deltaTime;

        if (ParticleLife < 0f)
        {
            ParticleLife = 1;
            gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        ParticleLife = 1;
    }
}
