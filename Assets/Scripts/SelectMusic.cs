using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

//코드작성: 권지수
public class SelectMusic : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] playSongListBox; //곡 목록
    public Transform[] SongPos; //목록 끝 위치 좌표
    public Transform songTypePos; //선택된 곡 위치 기준
    public float animationDuration_ = 0.3f; // 애니메이션 지속 시간
    public float animationDuration = 0.3f; // 애니메이션 지속 시간
    public float[] rotationAngles = { 0, 10, -10, 1, 0 }; // 애니메이션 중 각 회전 각도
    public float[] keyframes = { 0, 0.075f, 0.15f, 0.225f, 0.3f }; // 키프레임 시간
    public bool isMove = false; //이동 중인지 여부

    public float swipeThreshold = 50f;

    public RectTransform[] uiImageArray; // UI 이미지의 RectTransform을 참조합니다.

    public GameObject SelectUp;
    public GameObject SelectDown;

    public AudioSource playSong; //재생 중인 곡의 오디오 소스

    Click_Menu Click_Menu;

    void Start()
    {
        Click_Menu = FindObjectOfType<Click_Menu>();
        GameManager.instance.firstSong = 0; //리스트의 첫 곡 인덱스 초기화
        GameManager.instance.lastSong = playSongListBox.Length - 1; //리스트의 마지막 곡 인덱스 초기화
        setting();
    }

    //초기세팅
    private void setting()
    {
        // 현재 선택된 곡의 위치와 기준 위치(목표 위치) 사이의 차이를 계산
        int  positionDifference = (int)playSongListBox[GameManager.instance.songType].transform.position.y - (int)songTypePos.position.y;

        if (positionDifference != 0)
        {
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                playSongListBox[i].transform.position -= new Vector3(0, positionDifference, 0);
            }

            if (positionDifference > 0) // 선택된 곡이 기준 위치보다 위에 있으면
            {
                //리스트의 첫 곡이 선택된 곡이라면
                if (GameManager.instance.firstSong == GameManager.instance.songType)
                {
                    //리스트의 마지막 곡을 제일 위로 보냄
                    for (int i = 0; i < playSongListBox.Length; i++)
                    {
                        playSongListBox[GameManager.instance.lastSong].transform.position += new Vector3(0, 180f, 0);
                    }
                    GameManager.instance.firstSong = GameManager.instance.lastSong;
                    GameManager.instance.lastSong = --GameManager.instance.lastSong < 0 ? playSongListBox.Length - 1 : GameManager.instance.lastSong;
                }
            }
            else // 선택된 곡이 기준 위치보다 아래에 있으면
            {
                //리스트의 마지막 곡이 선택된 곡이라면
                if (GameManager.instance.lastSong == GameManager.instance.songType)
                {
                    //리스트의 첫 곡을 제일 밑으로 보냄
                    for (int i = 0; i < playSongListBox.Length; i++)
                    {
                        playSongListBox[GameManager.instance.firstSong].transform.position -= new Vector3(0, 180f, 0);
                    }
                    //첫 곡, 마지막 곡 인덱스 초기화
                    GameManager.instance.lastSong = GameManager.instance.firstSong;
                    GameManager.instance.firstSong = ++GameManager.instance.firstSong > playSongListBox.Length - 1 ? 0 : GameManager.instance.firstSong;
                }
            }
        }

        isMove = false; // 이동이 완료되면 isMove를 false로 설정
    }

    void Update()
    {
        ComputerMode();
    }


    public void ComputerMode()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput > 0 || Input.GetKey(KeyCode.DownArrow))
        {
            // 휠을 당겨 올렸을 때의 처리 ↓
            ScrollDOWN();
        }
        else if (wheelInput < 0 || Input.GetKey(KeyCode.UpArrow))
        {
            // 휠을 밀어 돌렸을 때의 처리 ↑
            ScrollUP();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopSong(); //노래 미리듣기 멈춤
            Click_Menu.startButton(); //게임 시작
        }
    }

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    public void MobileMode()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //초기 터치 좌표 저장
                startTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                //현재 터치 좌표 저장
                currentTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDirection = currentTouchPosition - startTouchPosition;

                if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
                {
                    if (swipeDirection.y > swipeThreshold) //위로 스와이프
                    {
                        ScrollDOWN();
                    }
                    else if (swipeDirection.y < -swipeThreshold) //아래로 스와이프
                    {
                        ScrollUP();
                    }
                }
            }
        }
    }
    //클릭, 터치로 노래 선택 가능
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        if (clickedObject == SelectDown)
        {
            ScrollDOWN(); //아래로 스크롤
        }
        else if (clickedObject == SelectUp)
        {
            ScrollUP(); //위로 스크롤}
        }
    }


    //노래 미리듣기
    public void PlaySong()
    {
        playSong = SoundManager.instance.PlayBgmSound(GameManager.instance.songType);
        playSong.Play();
    }

    //노래 미리듣기 멈춤
    public void StopSong()
    {
        if (playSong.isPlaying)
            playSong.Stop();
    }
    

    public void ScrollDOWN()
    {
        if (!isMove)
        {
            isMove = true;

            //마지막 곡이 목록의 아래쪽 끝에 도달하면
            if (playSongListBox[GameManager.instance.lastSong].transform.position.y == SongPos[1].position.y)
            {
                //리스트의 첫 곡을 제일 밑으로 보냄
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    playSongListBox[GameManager.instance.firstSong].transform.position -= new Vector3(0, 180f, 0);
                }
                //첫 곡, 마지막 곡 인덱스 초기화
                GameManager.instance.lastSong = GameManager.instance.firstSong;
                GameManager.instance.firstSong = ++GameManager.instance.firstSong > playSongListBox.Length - 1 ? 0 : GameManager.instance.firstSong;
            }

            //선택된 곡 인덱스 초기화
            GameManager.instance.songType = ++GameManager.instance.songType > playSongListBox.Length - 1 ? 0 : GameManager.instance.songType;

            //흔들림 효과, 이동 시 부드러운 움직임 효과
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateMovement(playSongListBox[i].transform, new Vector3(0, 180f, 0)));
                StartCoroutine(AnimateShake(uiImageArray[i]));
            }

            PlaySong(); //노래 미리듣기
        }
    }
    public void ScrollUP()
    {
        if (!isMove)
        {
            isMove = true;

            //첫 곡이 목록의 위쪽 끝에 도달하면
            if (playSongListBox[GameManager.instance.firstSong].transform.position.y == SongPos[0].position.y)
            {
                //리스트의 마지막 곡을 제일 위로 보냄
                for (int i = 0; i < playSongListBox.Length; i++)
                {
                    playSongListBox[GameManager.instance.lastSong].transform.position += new Vector3(0, 180f, 0);
                }
                //첫 곡, 마지막 곡 인덱스 초기화
                GameManager.instance.firstSong = GameManager.instance.lastSong;
                GameManager.instance.lastSong = --GameManager.instance.lastSong < 0 ? playSongListBox.Length - 1 : GameManager.instance.lastSong;
            }

            //선택된 곡 인덱스 초기화
            GameManager.instance.songType = --GameManager.instance.songType < 0 ? playSongListBox.Length - 1 : GameManager.instance.songType;

            //흔들림 효과, 이동 시 부드러운 움직임 효과
            for (int i = 0; i < playSongListBox.Length; i++)
            {
                StartCoroutine(AnimateMovement(playSongListBox[i].transform, new Vector3(0, -180f, 0)));
                StartCoroutine(AnimateShake(uiImageArray[i]));
            }

            PlaySong(); //노래 미리듣기
        }
    }

    //이동 시 부드러운 움직임 효과
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

    //흔들림 효과
    public IEnumerator AnimateShake(RectTransform target)
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

        target.localEulerAngles = new Vector3(0, 0, 0); //애니메이션 후 원래 각도로 복귀
    }
    public float GetAngleAtKeyframe(float t)
    {
        for (int i = 1; i < keyframes.Length; i++) //키프레임 배열을 순회
        {
            if (t < keyframes[i]) //현재 비율 t가 키프레임 i보다 작으면
            {
                //이전 키프레임과 현재 키프레임 사이의 진행 비율 계산
                float progress = (t - keyframes[i - 1]) / (keyframes[i] - keyframes[i - 1]);
                //이전 키프레임과 현재 키프레임 사이의 회전 각도 보간
                return Mathf.Lerp(rotationAngles[i - 1], rotationAngles[i], progress);
            }
        }
        return rotationAngles[rotationAngles.Length - 1]; //마지막 키프레임의 회전 각도 반환
    }

}