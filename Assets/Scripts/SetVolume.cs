using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//코드작성: 권지수
public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider; //전체 볼륨 슬라이더
    [SerializeField] private Slider m_MusicBGMSlider; //음악 볼륨 슬라이더
    [SerializeField] private Slider m_MusicSFXSlider; //효과음 볼륨 슬라이더

    private void Awake()
    {
        //슬라이더 값 변경 시 호출될 함수 설정
        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        //슬라이더 초기값 설정
        m_MusicMasterSlider.value = PlayerPrefs.GetFloat("Master", 0.75f);
        m_MusicBGMSlider.value = PlayerPrefs.GetFloat("BGM", 0.75f);
        m_MusicSFXSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
    }

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master", volume); //현재 볼륨 값 저장
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGM", volume); //현재 볼륨 값 저장
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume); //현재 볼륨 값 저장
    }
}
