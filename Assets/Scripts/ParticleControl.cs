using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
