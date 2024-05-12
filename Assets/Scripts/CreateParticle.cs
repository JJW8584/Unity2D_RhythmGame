using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class CreateParticle : MonoBehaviour
{
    public Transform effectPos0;
    public Transform effectPos1;

    public GameObject notePerfectEffectPrefab;
    public GameObject noteGoodEffectPrefab;
    public GameObject noteBadEffectPrefab;

    GameObject[] notePerfectEffect;
    GameObject[] noteGoodEffect;
    GameObject[] noteBadEffect;

    GameObject[] targetPool;

    void Awake()
    {
        notePerfectEffect = new GameObject[20];
        noteGoodEffect = new GameObject[20];
        noteBadEffect = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < notePerfectEffect.Length; i++)
        {
            notePerfectEffect[i] = Instantiate(notePerfectEffectPrefab);
            notePerfectEffect[i].SetActive(false);
        }
        for (int i = 0; i < noteGoodEffect.Length; i++)
        {
            noteGoodEffect[i] = Instantiate(noteGoodEffectPrefab);
            noteGoodEffect[i].SetActive(false);
        }
        for (int i = 0; i < noteBadEffect.Length; i++)
        {
            noteBadEffect[i] = Instantiate(noteBadEffectPrefab);
            noteBadEffect[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string Type)
    {
        switch (Type)
        {
            case "notePerfectEffect":
                targetPool = notePerfectEffect;
                break;
            case "noteGoodEffect":
                targetPool = noteGoodEffect;
                break;
            case "noteBadEffect":
                targetPool = noteBadEffect;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public void CreateEffect(int effectLoc, int effectType)
    {
        GameObject effect;
        if (effectLoc == 0) //��
        {
            switch (effectType)
            {
                case 0:
                    effect = MakeObj("notePerfectEffect");
                    effect.gameObject.transform.position = effectPos0.position + new Vector3(0f, 1.0f, 0f);
                    break;
                case 1:
                    effect = MakeObj("noteGoodEffect");
                    effect.gameObject.transform.position = effectPos0.position + new Vector3(0f, 1.0f, 0f);
                    break;
                case 2:
                    effect = MakeObj("noteBadEffect");
                    effect.gameObject.transform.position = effectPos0.position + new Vector3(0f, 1.0f, 0f);
                    break;
            }
        }
        else //�Ʒ�
        {
            switch (effectType)
            {
                case 0:
                    effect = MakeObj("notePerfectEffect");
                    effect.gameObject.transform.position = effectPos1.position + new Vector3(0f, 1.0f, 0f);
                    break;
                case 1:
                    effect = MakeObj("noteGoodEffect");
                    effect.gameObject.transform.position = effectPos1.position + new Vector3(0f, 1.0f, 0f);
                    break;
                case 2:
                    effect = MakeObj("noteBadEffect");
                    effect.gameObject.transform.position = effectPos1.position + new Vector3(0f, 1.0f, 0f);
                    break;
            }
        }
        
    }

}
