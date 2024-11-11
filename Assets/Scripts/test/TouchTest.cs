using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour
{
    /*public int MaxHP = 250;
    public int HP;
    public Image HPmeter; //ü�¹� UI*/
    private float DoubleNoteTime = 0.3f; //���� ��Ʈ ����

    private float elapsedTime_0 = 0.0f; //���� ��Ʈ Ŭ�� �ð�
    public bool isClicked_0 = false; //���� ��Ʈ Ŭ�� ����
    private float elapsedTime_1 = 0.0f; //������ ��Ʈ Ŭ�� �ð�
    public bool isClicked_1 = false; //������ ��Ʈ Ŭ�� ����

    public bool isNotBoth = true; //������Ʈ ����
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

        /*//Ʃ�丮���� �ƴ� ��쿡�� ü�� �ʱ�ȭ
        if (!GameManager.instance.isTutorial)
        {
            HP = MaxHP;
            HPmeter.fillAmount = (float)HP / (float)MaxHP;
        }*/
    }

    void Update()
    {
        // �Ͻ� ������ �ƴ� ���
        if (!GameManager.instance.isPause)
        {
            PCTouchInput();  // ��ġ �Է� ó��
            //Health();      // ü�� ����
        }
    }

    void PCTouchInput()
    {
        //��
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.UP1]))
        {
            isClicked_0 = true;
            if (isNotBoth)
                //SoundManager.instance.PlaySound("JUMP");
            theTimingManager.CheckTiming0(); //�� ��Ʈ ���� üũ
            Motion(0); //�ִϸ��̼� ����
        }
        if (isClicked_0 == true)
        {
            elapsedTime_0 += Time.deltaTime;

            //���� ��Ʈ
            if (DoubleNoteTime > elapsedTime_0 && (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1])))
            {
                theTimingManager.CheckTiming_Both(); //���� ��Ʈ ���� üũ
                Motion(2); //�ִϸ��̼� ����
            }
        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP0]) || Input.GetKeyUp(KeySetting.keys[KeyAction.UP1]))
        {
            isClicked_0 = false;
            elapsedTime_0 = 0.0f;
        }


        //�Ʒ�
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1]))
        {
            isClicked_1 = true;
            if (isNotBoth)
                //SoundManager.instance.PlaySound("JUMP");
            theTimingManager.CheckTiming1(); //�Ʒ� ��Ʈ ���� üũ
            Motion(1); //�ִϸ��̼� ����
        }
        if (isClicked_1 == true)
        {
            elapsedTime_1 += Time.deltaTime;

            //���� ��Ʈ
            if (DoubleNoteTime > elapsedTime_1 && (Input.GetKeyDown(KeySetting.keys[KeyAction.UP0]) || Input.GetKeyDown(KeySetting.keys[KeyAction.UP1])))
            {
                theTimingManager.CheckTiming_Both(); //���� ��Ʈ ���� üũ
                Motion(2); //�ִϸ��̼� ����
            }
        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN0]) || Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN1]))
        {
            isClicked_1 = false;
            elapsedTime_1 = 0.0f;
        }
    }

    void MobileTouchInput()
    {
        // ��ġ�� �ִ� ��쿡�� ó��
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return; //UI ��ġ�� �������� ���

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 touchPosition = touch.position;

                // ���� ��ġ
                if (touchPosition.x < Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        isClicked_0 = true;
                        elapsedTime_0 = 0.0f; // ��ġ�� ���۵Ǿ����Ƿ� �ð��� �ʱ�ȭ
                        if (isNotBoth)
                            SoundManager.instance.PlaySound("JUMP");
                        theTimingManager.CheckTiming0(); // ���� ��Ʈ ���� üũ
                        Motion(0); // �ִϸ��̼� ����
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        isClicked_0 = false;
                        elapsedTime_0 = 0.0f; // ��ġ�� �������Ƿ� �ð��� �ʱ�ȭ
                    }
                }

                // ������ ��ġ (������ if������ ó��)
                if (touchPosition.x >= Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        isClicked_1 = true;
                        elapsedTime_1 = 0.0f; // ��ġ�� ���۵Ǿ����Ƿ� �ð��� �ʱ�ȭ
                        if (isNotBoth)
                            SoundManager.instance.PlaySound("JUMP");
                        theTimingManager.CheckTiming1(); // ������ ��Ʈ ���� üũ
                        Motion(1); // �ִϸ��̼� ����
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        isClicked_1 = false;
                        elapsedTime_1 = 0.0f; // ��ġ�� �������Ƿ� �ð��� �ʱ�ȭ
                    }
                }
            }

            // �� ��ġ�� ���ÿ� �߻��� �� ���� ��Ʈ ����
            if (isClicked_0 && isClicked_1)
            {
                elapsedTime_0 += Time.deltaTime; // ��ġ �� ��� �ð� ������Ʈ
                elapsedTime_1 += Time.deltaTime;

                if (DoubleNoteTime > elapsedTime_0 && DoubleNoteTime > elapsedTime_1)
                {
                    theTimingManager.CheckTiming_Both(); // ���� ��Ʈ ���� üũ
                    Motion(2); // �ִϸ��̼� ����
                }
            }
        }
    }

    /*//ü���� 0�� �Ǹ�
    void Health()
    {
        if (HP <= 0)
        {
            PlaySong.SongStop(); //�뷡 ����
            LoadingSceneManager.LoadScene("EndingScene"); //���� ������ ��ȯ
        }

        UpdateHealthBar(); //ü�¹� ������Ʈ
    }*/

    //���� ���
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

    //�ִϸ��̼� ����
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

    /*//ü�¹� ������Ʈ
    void UpdateHealthBar()
    {
        HPmeter.fillAmount = Mathf.Lerp(HPmeter.fillAmount, (float)HP / (float)MaxHP, Time.deltaTime * 20);
    }

    //������
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
        }
    }

    //�浹
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
    }*/
}