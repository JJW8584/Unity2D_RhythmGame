using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//�ڵ��ۼ�: ������
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicSFXSlider;

    public AudioSource bgm_player;
    public AudioSource sfx_player;
    public AudioSource playSong_player;

    public AudioClip bgm_audio_clips;
    public AudioClip[] PlaySong_audio_clips;
    public AudioClip[] audio_clips;

    //GameManager.instance.songType
    private void Awake()
    {
        //�����Ŵ��� �̱��� ����
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);

        //bgm_player = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        //sfx_player = GameObject.Find("Sfx Player").GetComponent<AudioSource>();
    }

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void PlaySound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "JUMP": index = 0; break;
            case "ATTACK": index = 1; break;
            case "HIT": index = 2; break;
            case "Click": index = 3; break;
        }

        sfx_player.clip = audio_clips[index];
        sfx_player.Play();
    }

    public AudioSource PlayBgmSound(int i)
    {
        playSong_player.clip = PlaySong_audio_clips[i];
        return playSong_player;
    }

    public AudioSource BgmSound()
    {
        bgm_player.clip = bgm_audio_clips;
        return bgm_player;
    }
}