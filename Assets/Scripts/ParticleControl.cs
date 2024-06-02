using UnityEngine;

//코드작성: 권지수
public class ParticleControl : MonoBehaviour
{
    public float defaultLife = 1;
    public float ParticleLife;

    void Update()
    {
        ParticleLife -= Time.deltaTime;

        if (ParticleLife < 0f)
        {
            ParticleLife = defaultLife;
            gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        ParticleLife = defaultLife;
    }
}
