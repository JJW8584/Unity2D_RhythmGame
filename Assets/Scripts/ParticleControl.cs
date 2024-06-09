using UnityEngine;

//코드작성: 권지수
public class ParticleControl : MonoBehaviour
{
    public float defaultLife = 1; //파티클 기본 수명
    public float ParticleLife; //현재 파티클의 수명

    void Update()
    {
        ParticleLife -= Time.deltaTime;

        //파티클 수명이 0보다 작아지면
        if (ParticleLife < 0f)
        {
            ParticleLife = defaultLife; //파티클 수명을 기본으로 초기화
            gameObject.SetActive(false); //파티클 오브젝트 비활성화
        }
    }

    //파티클이 활성화될 때마다 수명을 기본 수명으로 초기화
    void OnEnable()
    {
        ParticleLife = defaultLife;
    }
}
