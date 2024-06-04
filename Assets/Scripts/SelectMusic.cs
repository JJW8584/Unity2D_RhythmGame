using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMusic : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] playSongListBox;
    public Transform[] SongPos;
    public Transform songTypePos;
    public float animationDuration_ = 0.3f; // 애니메이션 지속 시간
    public float animationDuration = 0.3f; // 애니메이션 지속 시간
    public float[] rotationAngles = { 0, 10, -10, 1, 0 }; // 애니메이션 중 각 회전 각도
    public float[] keyframes = { 0, 0.075f, 0.15f, 0.225f, 0.3f }; // 키프레임 시간
    public bool isMove = false;
    //public bool[] isPlay;

    public RectTransform[] uiImageArray; // UI 이미지의 RectTransform을 참조합니다.

    public RectTransform SelectSongBox;
    public GameObject SelectUp;
    public GameObject SelectDown;

    public AudioSource playSong;

    Click_Menu Click_Menu;

    public int songType;

    void Start()
    {
        Click_Menu = FindObjectOfType<Click_Menu>();
        GameManager.instance.firstSong = 0;
        GameManager.instance.lastSong = playSongListBox.Length - 1;
        setting();
    }

    private void setting()
    {
        while (playSongListBox[GameManager.instance.songType].transform.position.y != songTypePos.position.y)
        {
            if (playSongListBox[GameManager.instance.songType].transform.position.y > songTypePos.position.y)
            {
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    playSongListBox[i].transform.position -= new Vector3(0, 180f, 0);
                }
                if(GameManager.instance.firstSong == GameManager.instance.songType)
                {
                    for (int i = 0; i < playSongListBox.Length; i++)
                    {
                        playSongListBox[GameManager.instance.lastSong].transform.position += new Vector3(0, 180f, 0);
                    }
                    GameManager.instance.firstSong = GameManager.instance.lastSong;
                    GameManager.instance.lastSong = --GameManager.instance.lastSong < 0 ? playSongListBox.Length - 1 : GameManager.instance.lastSong;
                }
            }
            else if (playSongListBox[GameManager.instance.songType].transform.position.y < songTypePos.position.y)
            {
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    playSongListBox[i].transform.position += new Vector3(0, 180f, 0);
                }
                if (GameManager.instance.lastSong == GameManager.instance.songType)
                {
                    for (int i = 0; i < playSongListBox.Length; i++)
                    {
                        playSongListBox[GameManager.instance.firstSong].transform.position -= new Vector3(0, 180f, 0);
                    }
                    GameManager.instance.lastSong = GameManager.instance.firstSong;
                    GameManager.instance.firstSong = ++GameManager.instance.firstSong > playSongListBox.Length - 1 ? 0 : GameManager.instance.firstSong;
                }
            }
        }
        isMove = false; // 이동이 완료되면 isMove를 false로 설정
    }

    void Update()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput > 0 || Input.GetKey(KeyCode.DownArrow) || isDown)
        {
            isDown = false;
            // 휠을 당겨 올렸을 때의 처리 ↓
            ScrollDOWN();
        }
        else if (wheelInput < 0 || Input.GetKey(KeyCode.UpArrow) || isUp)
        {
            isUp = false;
            // 휠을 밀어 돌렸을 때의 처리 ↑
            ScrollUP();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Click_Menu.startButton();
        }
    }


    public void PlaySong()
    {
        playSong = SoundManager.instance.PlayBgmSound(GameManager.instance.songType);
        //BgmSong.time = 0;
        playSong.Play();
    }

    public void StopSong()
    {
        playSong.Stop();
    }

    public void ScrollDOWN()
    {
        if (isMove == false)
        {
            isMove = true;
            if (playSongListBox[GameManager.instance.lastSong].transform.position.y == SongPos[1].position.y)
            {
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    playSongListBox[GameManager.instance.firstSong].transform.position -= new Vector3(0, 180f, 0);
                }
                GameManager.instance.lastSong = GameManager.instance.firstSong;
                GameManager.instance.firstSong = ++GameManager.instance.firstSong > playSongListBox.Length - 1 ? 0 : GameManager.instance.firstSong;
            }
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateShake(uiImageArray[i]));
            }
            GameManager.instance.songType = ++GameManager.instance.songType > playSongListBox.Length - 1 ? 0 : GameManager.instance.songType;
            
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateMovement(playSongListBox[i].transform, new Vector3(0, 180f, 0)));
            }
            PlaySong();
        }
    }
    public void ScrollUP()
    {
        if (isMove == false)
        {
            isMove = true;
            if (playSongListBox[GameManager.instance.firstSong].transform.position.y == SongPos[0].position.y)
            {
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    playSongListBox[GameManager.instance.lastSong].transform.position += new Vector3(0, 180f, 0);
                }
                GameManager.instance.firstSong = GameManager.instance.lastSong;
                GameManager.instance.lastSong = --GameManager.instance.lastSong < 0 ? playSongListBox.Length - 1 : GameManager.instance.lastSong;
            }
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateShake(uiImageArray[i]));
            }
            GameManager.instance.songType = --GameManager.instance.songType < 0 ? playSongListBox.Length - 1 : GameManager.instance.songType;
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateMovement(playSongListBox[i].transform, new Vector3(0, -180f, 0)));
            }
            PlaySong();
        }
    }
    
    bool isUp = false;
    bool isDown = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        //RectTransform clickedObjectPos = clickedObject.GetComponent<RectTransform>();
        if (clickedObject == SelectDown)
        {
            isDown=true;
        }
        if (clickedObject == SelectUp)
        {
            isUp=true;
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

        target.localEulerAngles = new Vector3(0, 0, 0); // 애니메이션 후 원래 각도로 복귀
    }
    private float GetAngleAtKeyframe(float t)
    {
        for (int i = 1; i < keyframes.Length; i++) // 키프레임 배열을 순회
        {
            if (t < keyframes[i]) // 현재 비율 t가 키프레임 i보다 작으면
            {
                // 이전 키프레임과 현재 키프레임 사이의 진행 비율 계산
                float progress = (t - keyframes[i - 1]) / (keyframes[i] - keyframes[i - 1]);
                // 이전 키프레임과 현재 키프레임 사이의 회전 각도 보간
                return Mathf.Lerp(rotationAngles[i - 1], rotationAngles[i], progress);
            }
        }
        return rotationAngles[rotationAngles.Length - 1]; // 마지막 키프레임의 회전 각도 반환
    }

}