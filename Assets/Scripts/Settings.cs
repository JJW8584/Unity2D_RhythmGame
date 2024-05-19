using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KeyAction { UP0, UP1, DOWN0, DOWN1, KEYCOUNT }
public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class Settings : MonoBehaviour
{
    //Å° ÀÔ·Â
    public Text[] keyTXT;

    KeyCode[] defaultKey = new KeyCode[] { KeyCode.F, KeyCode.D, KeyCode.J, KeyCode.K };
    private void Awake()
    {
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
        for(int i=0; i<keyTXT.Length; i++)
        {
            keyTXT[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }
    private void Update()
    {
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

    int key = -1;
    public void ChangeKey(int num)
    {
        
        key = num;
    }

    public void KeyReset()
    {
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys[(KeyAction)i] = defaultKey[i];
        }
    }
}
