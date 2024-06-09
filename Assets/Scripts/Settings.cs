using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//코드작성: 권지수

public enum KeyAction { UP0, UP1, DOWN0, DOWN1, KEYCOUNT }
public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class Settings : MonoBehaviour
{
    //키 입력
    public Text[] keyTXT;

    //기본 키 설정
    KeyCode[] defaultKey = new KeyCode[] { KeyCode.F, KeyCode.D, KeyCode.J, KeyCode.K };

    private void Awake()
    {
        //키 설정 초기화
        if (KeySetting.keys.Count == 0)
        {
            for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
            {
                KeySetting.keys.Add((KeyAction)i, defaultKey[i]);
            }
        }
    }

    private void Start()
    {
        //키 설정을 텍스트로 표시
        for(int i=0; i<keyTXT.Length; i++)
        {
            keyTXT[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }

    private void Update()
    {
        //현재 키 설정을 텍스트로 표시
        for (int i = 0; i < keyTXT.Length; i++)
        {
            keyTXT[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
        
    }

    Event keyEvent;
    private void OnGUI()
    {
        keyEvent = Event.current;
        if (keyEvent.isKey && key > -1)
        {
            //같은 키가 이미 사용중이면 해당 키와 변경
            for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
            {
                if (i != key && KeySetting.keys[(KeyAction)i] == keyEvent.keyCode)
                {
                    KeySetting.keys[(KeyAction)i] = KeySetting.keys[(KeyAction)key];
                }
            }
            KeySetting.keys[(KeyAction)key]=keyEvent.keyCode;
            key = -1;
        }
    }

    //키를 변경할 때 호출
    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
    }

    //키 설정을 기본값으로 초기화
    public void KeyReset()
    {
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys[(KeyAction)i] = defaultKey[i];
        }
    }
}
