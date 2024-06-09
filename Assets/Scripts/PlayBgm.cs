using UnityEngine;

//�ڵ��ۼ�: ������
public class PlayBgm : MonoBehaviour
{
    public AudioSource BgmSong;

    void Awake()
    {
        BgmSong = SoundManager.instance.BgmSound();
        PlayBgmMusic();
    }

    //���� ���
    public void PlayBgmMusic()
    {
        BgmSong.time = 0;   //���� ��� ��ġ �ʱ�ȭ
        BgmSong.Play(); //���� ���
    }

    //���� ����
    public void StopBgmMusic()
    {
        BgmSong.Stop();
    }
}
