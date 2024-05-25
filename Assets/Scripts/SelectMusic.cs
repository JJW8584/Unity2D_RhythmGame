using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusic : MonoBehaviour
{
    public GameObject[] playSongListBox;
    public Transform[] SongPos;
    //public Transform songTypePos;
    public float animationDuration_ = 0.3f; // �ִϸ��̼� ���� �ð�
    public float animationDuration = 0.3f; // �ִϸ��̼� ���� �ð�
    public float[] rotationAngles = { 0, 10, -10, 1, 0 }; // �ִϸ��̼� �� �� ȸ�� ����
    public float[] keyframes = { 0, 0.075f, 0.15f, 0.225f, 0.3f }; // Ű������ �ð�
    public bool isMove = false;
    //public bool[] isPlay;

    public RectTransform[] uiImageArray; // UI �̹����� RectTransform�� �����մϴ�.

    private void Start()
    {
        
    }
    void Update()
    {
        for (int i = 0; i < playSongListBox.Length; i++)
        {
            if (playSongListBox[i].transform.position.y >= SongPos[0].position.y || playSongListBox[i].transform.position.y <= SongPos[1].position.y)
            {
                playSongListBox[i].SetActive(false);
            }
            else
            {
                playSongListBox[i].SetActive(true);
            }
        }

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput > 0 || Input.GetKey(KeyCode.UpArrow))
        {
            // ���� ��� �÷��� ���� ó�� ��
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateShake(uiImageArray[i]));
            }
            if (playSongListBox[playSongListBox.Length - 3].transform.position.y >= SongPos[0].position.y)
            {
                return;
            }
            if (isMove == false)
            {
                isMove = true;
                GameManager.instance.songType = ++GameManager.instance.songType > playSongListBox.Length - 1 ? 0 : GameManager.instance.songType;
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    StartCoroutine(AnimateMovement(playSongListBox[i].transform, new Vector3(0, 180f, 0)));
                }
            }
        }
        else if (wheelInput < 0 || Input.GetKey(KeyCode.DownArrow))
        {
            // ���� �о� ������ ���� ó�� ��
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateShake(uiImageArray[i]));
            }
            if (playSongListBox[2].transform.position.y <= SongPos[1].position.y)
            {
                return;
            }
            if (isMove == false)
            {
                isMove = true;
                GameManager.instance.songType = --GameManager.instance.songType < 0 ? playSongListBox.Length - 1 : GameManager.instance.songType;
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    StartCoroutine(AnimateMovement(playSongListBox[i].transform, new Vector3(0, -180f, 0)));
                }
            }
        }
    }

    private IEnumerator AnimateMovement(Transform target, Vector3 offset)
    {
        Vector3 initialPosition = target.position;
        Vector3 targetPosition = initialPosition + offset;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration_)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration_;
            target.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }

        target.position = targetPosition;
        isMove = false;
    }
    private IEnumerator AnimateShake(RectTransform target)
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            float angle = Mathf.LerpAngle(GetAngleAtKeyframe(t), GetAngleAtKeyframe(t + Time.deltaTime / animationDuration), t);
            target.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        target.localEulerAngles = new Vector3(0, 0, 0); // �ִϸ��̼� �� ���� ������ ����
    }
    private float GetAngleAtKeyframe(float t)
    {
        for (int i = 1; i < keyframes.Length; i++) // Ű������ �迭�� ��ȸ
        {
            if (t < keyframes[i]) // ���� ���� t�� Ű������ i���� ������
            {
                // ���� Ű�����Ӱ� ���� Ű������ ������ ���� ���� ���
                float progress = (t - keyframes[i - 1]) / (keyframes[i] - keyframes[i - 1]);
                // ���� Ű�����Ӱ� ���� Ű������ ������ ȸ�� ���� ����
                return Mathf.Lerp(rotationAngles[i - 1], rotationAngles[i], progress);
            }
        }
        return rotationAngles[rotationAngles.Length - 1]; // ������ Ű�������� ȸ�� ���� ��ȯ
    }
}