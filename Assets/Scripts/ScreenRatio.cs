using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1080, 1920, true);

        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
    }
}
