using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // ќпределение источников звука дл€ спокойной и динамичной музыки
    public AudioSource calmMusicSource;
    public AudioSource dynamicMusicSource;

    // ќпределение аудиоклипов дл€ спокойной и динамичной музыки
    public AudioClip calmMusicClip;
    public AudioClip dynamicMusicClip;

    // “екущее состо€ние музыки
    private bool isDynamicMusicPlaying = false;

    // ƒлительность затухани€ дл€ плавных переходов
    public float fadeDuration = 2f;

    // »сходна€ громкость дл€ восстановлени€ после затухани€
    private float initialDynamicMusicVolume;
    private float initialCalmMusicVolume;

    private void Awake()
    {
        // —охран€ем исходную громкость дл€ последующего восстановлени€
        initialDynamicMusicVolume = dynamicMusicSource.volume;
        initialCalmMusicVolume = calmMusicSource.volume;
    }

    // ћетод дл€ смены на спокойную музыку
    public void SwitchToCalmMusic()
    {
        if (isDynamicMusicPlaying)
        {
            StartCoroutine(SwitchAfterFade(dynamicMusicSource, calmMusicSource, calmMusicClip, fadeDuration));
            isDynamicMusicPlaying = false;
        }
        else
        {
            // ≈сли динамична€ музыка не играет, сразу включаем спокойную музыку
            calmMusicSource.volume = initialCalmMusicVolume;
            calmMusicSource.clip = calmMusicClip;
            calmMusicSource.Play();
        }
    }

    // ћетод дл€ смены на динамичную музыку
    public void SwitchToDynamicMusic()
    {
        StartCoroutine(FadeOutMusic(calmMusicSource, fadeDuration));

        // «апуск динамичной музыки с исходной громкостью
        dynamicMusicSource.volume = initialDynamicMusicVolume;
        dynamicMusicSource.clip = dynamicMusicClip;
        dynamicMusicSource.Play();

        isDynamicMusicPlaying = true;
    }

    //  орутина дл€ затухани€ музыки и переключени€ на другую музыку после затухани€
    private IEnumerator SwitchAfterFade(AudioSource fromSource, AudioSource toSource, AudioClip toClip, float duration)
    {
        yield return FadeOutMusic(fromSource, duration);

        // ѕосле завершени€ затухани€ воспроизводим новую музыку
        toSource.volume = initialCalmMusicVolume;
        toSource.clip = toClip;
        toSource.Play();
    }

    //  орутина дл€ затухани€ музыки
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

    // метод дл€ выключени€ всей музыки
    public void StopMusic()
    {
        calmMusicSource.Stop();
        dynamicMusicSource.Stop();
    }
}
