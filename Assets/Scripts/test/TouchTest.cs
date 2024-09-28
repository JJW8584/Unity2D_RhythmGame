using UnityEngine;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour
{
    public int MaxHP = 250;
    public int HP;
    public Image HPmeter; //체력바 UI
    private float DoubleNoteTime = 0.3f; //더블 노트 기준

    private float elapsedTime_0 = 0.0f; //왼쪽 노트 클릭 시간
    public bool isClicked_0 = false; //왼쪽 노트 클릭 여부
    private float elapsedTime_1 = 0.0f; //오른쪽 노트 클릭 시간
    public bool isClicked_1 = false; //오른쪽 노트 클릭 여부

    public bool isNotBoth = true; //더블노트 여부
    public bool isNote = false;

    TimingManager theTimingManager;
    PlaySong PlaySong;
    Animator animator;
    AudioSource audioSource;
    public AudioClip JumpSound;
    public AudioClip AttackSound;
    public AudioClip HitSound;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        PlaySong = FindObjectOfType<PlaySong>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        //튜토리얼이 아닌 경우에만 체력 초기화
        if (!GameManager.instance.isTutorial)
        {
            HP = MaxHP;
            HPmeter.fillAmount = (float)HP / (float)MaxHP;
        }
    }

    void Update()
    {
        // 일시 정지가 아닌 경우
        if (!GameManager.instance.isPause)
        {
            HandleTouchInput();  // 터치 입력 처리
            Health();      // 체력 관리
        }
    }

    void PCTouchInput()
    {
        // 터치 입력이 있을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 터치 위치 판정
            Vector2 touchPosition = Input.mousePosition;

            // 왼쪽 터치
            if (touchPosition.x < Screen.width / 2)
            {
                isClicked_0 = true;
                if (isNotBoth)
                    SoundManager.instance.PlaySound("JUMP");
                theTimingManager.CheckTiming0(); // 왼쪽 노트 판정 체크
                Motion(0); // 애니메이션 실행
            }
            if (Input.GetMouseButtonUp(0))
            {
                isClicked_0 = false;
                elapsedTime_0 = 0.0f;
            }
            // 오른쪽 터치
            else if (touchPosition.x >= Screen.width / 2)
            {
                isClicked_1 = true;
                if (isNotBoth)
                    SoundManager.instance.PlaySound("JUMP");
                theTimingManager.CheckTiming1(); // 오른쪽 노트 판정 체크
                Motion(1); // 애니메이션 실행

                if (Input.GetMouseButtonUp(0))
                {
                    isClicked_1 = false;
                    elapsedTime_1 = 0.0f;
                }
            }

            // 더블 노트 판정 (왼쪽과 오른쪽 동시에 터치)
            if (isClicked_0 && isClicked_1)
            {
                if (DoubleNoteTime > elapsedTime_0 && DoubleNoteTime > elapsedTime_1)
                {
                    theTimingManager.CheckTiming_Both(); // 더블 노트 판정 체크
                    Motion(2); // 애니메이션 실행
                }
            }
        }
    }

    void HandleTouchInput()
    {
        // 터치 입력이 있을 때
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // 터치 위치 판정
                Vector2 touchPosition = touch.position;

                // 왼쪽 터치
                if (touchPosition.x < Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        isClicked_0 = true;
                        if (isNotBoth)
                            SoundManager.instance.PlaySound("JUMP");
                        theTimingManager.CheckTiming0(); // 왼쪽 노트 판정 체크
                        Motion(0); // 애니메이션 실행
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        isClicked_0 = false;
                        elapsedTime_0 = 0.0f;
                    }
                }
                // 오른쪽 터치
                else if (touchPosition.x >= Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        isClicked_1 = true;
                        if (isNotBoth)
                            SoundManager.instance.PlaySound("JUMP");
                        theTimingManager.CheckTiming1(); // 오른쪽 노트 판정 체크
                        Motion(1); // 애니메이션 실행
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        isClicked_1 = false;
                        elapsedTime_1 = 0.0f;
                    }
                }
            }

            // 더블 노트 판정 (왼쪽과 오른쪽 동시에 터치)
            if (isClicked_0 && isClicked_1)
            {
                if (DoubleNoteTime > elapsedTime_0 && DoubleNoteTime > elapsedTime_1)
                {
                    theTimingManager.CheckTiming_Both(); // 더블 노트 판정 체크
                    Motion(2); // 애니메이션 실행
                }
            }
        }
    }

    //체력이 0이 되면
    void Health()
    {
        if (HP <= 0)
        {
            PlaySong.SongStop(); //노래 멈춤
            LoadingSceneManager.LoadScene("EndingScene"); //엔딩 씬으로 전환
        }

        UpdateHealthBar(); //체력바 업데이트
    }

    //사운드 재생
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = JumpSound;
                break;
            case "ATTACK":
                audioSource.clip = AttackSound;
                break;
            case "HIT":
                audioSource.clip = HitSound;
                break;
        }
        audioSource.Play();
    }

    //애니메이션 실행
    void Motion(int height)
    {
        if (isNotBoth)
        {
            int attackType = UnityEngine.Random.Range(0, 2);
            if (height == 0)
            {
                switch (attackType)
                {
                    case 0:
                        animator.SetBool("isBodyShot0", true);
                        break;
                    case 1:
                        animator.SetBool("isScratch0", true);
                        break;
                }
            }
            if (height == 1)
            {
                switch (attackType)
                {
                    case 0:
                        animator.SetBool("isBodyShot", true);
                        break;
                    case 1:
                        animator.SetBool("isScratch", true);
                        break;
                }
            }
        }
        else if (!isNotBoth)
        {
            animator.SetBool("isUpperCut", true);
        }
    }

    public void SetJump() { animator.SetBool("isJump", false); }
    public void SetBodyShot() { animator.SetBool("isBodyShot", false); }
    public void SetScratch() { animator.SetBool("isScratch", false); }
    public void SetBodyShot0() { animator.SetBool("isBodyShot0", false); }
    public void SetScratch0() { animator.SetBool("isScratch0", false); }
    public void SetUpperCut() { animator.SetBool("isUpperCut", false); }
    public void SetisNotBoth() { isNotBoth = true; }
    public void SetisNote() { isNote = false; }
    public void SetHit() { animator.SetBool("isHit", false); }

    //체력바 업데이트
    void UpdateHealthBar()
    {
        HPmeter.fillAmount = Mathf.Lerp(HPmeter.fillAmount, (float)HP / (float)MaxHP, Time.deltaTime * 20);
    }

    //데미지
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
        }
    }

    //충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isTutorial)
        {
            if (collision.gameObject.tag == "Note0" || collision.gameObject.tag == "Note1" || collision.gameObject.tag == "DoubleNote" || collision.gameObject.tag == "Wheel")
            {
                TakeDamage(10);
                animator.SetBool("isHit", true);
            }
        }
    }
}
