using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // ����������� ���������� ����� ��� ��������� � ���������� ������
    public AudioSource calmMusicSource;
    public AudioSource dynamicMusicSource;

    // ����������� ����������� ��� ��������� � ���������� ������
    public AudioClip calmMusicClip;
    public AudioClip dynamicMusicClip;

    // ������� ��������� ������
    private bool isDynamicMusicPlaying = false;

    // ������������ ��������� ��� ������� ���������
    public float fadeDuration = 2f;

    // �������� ��������� ��� �������������� ����� ���������
    private float initialDynamicMusicVolume;
    private float initialCalmMusicVolume;

    private void Awake()
    {
        // ��������� �������� ��������� ��� ������������ ��������������
        initialDynamicMusicVolume = dynamicMusicSource.volume;
        initialCalmMusicVolume = calmMusicSource.volume;
    }

    // ����� ��� ����� �� ��������� ������
    public void SwitchToCalmMusic()
    {
        if (isDynamicMusicPlaying)
        {
            StartCoroutine(SwitchAfterFade(dynamicMusicSource, calmMusicSource, calmMusicClip, fadeDuration));
            isDynamicMusicPlaying = false;
        }
        else
        {
            // ���� ���������� ������ �� ������, ����� �������� ��������� ������
            calmMusicSource.volume = initialCalmMusicVolume;
            calmMusicSource.clip = calmMusicClip;
            calmMusicSource.Play();
        }
    }

    // ����� ��� ����� �� ���������� ������
    public void SwitchToDynamicMusic()
    {
        StartCoroutine(FadeOutMusic(calmMusicSource, fadeDuration));

        // ������ ���������� ������ � �������� ����������
        dynamicMusicSource.volume = initialDynamicMusicVolume;
        dynamicMusicSource.clip = dynamicMusicClip;
        dynamicMusicSource.Play();

        isDynamicMusicPlaying = true;
    }

    // �������� ��� ��������� ������ � ������������ �� ������ ������ ����� ���������
    private IEnumerator SwitchAfterFade(AudioSource fromSource, AudioSource toSource, AudioClip toClip, float duration)
    {
        yield return FadeOutMusic(fromSource, duration);

        // ����� ���������� ��������� ������������� ����� ������
        toSource.volume = initialCalmMusicVolume;
        toSource.clip = toClip;
        toSource.Play();
    }

    // �������� ��� ��������� ������
    private IEnumerator FadeOutMusic(AudioSource audioSource, float duration)
    {
        float startTime = Time.time;
        float startVolume = audioSource.volume;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        audioSource.Stop();
    }

    // ����� ��� ���������� ���� ������
    public void StopMusic()
    {
        calmMusicSource.Stop();
        dynamicMusicSource.Stop();
    }
}
