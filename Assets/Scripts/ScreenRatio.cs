using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        //Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
    }
}
