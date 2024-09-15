using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    private Texture2D fadeTexture;
    private float alpha = 0f;
    private bool isFading = false;
    private float fadeDuration = 1f;

    private void Start()
    {
        CreateFadeTexture();
    }

    private void CreateFadeTexture()
    {
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, Color.black);
        fadeTexture.Apply();
    }

    public IEnumerator FadeIn(float duration)
    {
        fadeDuration = duration;
        yield return StartCoroutine(Fade(1f));
    }

    public IEnumerator FadeOut(float duration)
    {
        fadeDuration = duration;
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFading = true;
        float startAlpha = alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        alpha = targetAlpha;
        isFading = false;
    }

    private void OnGUI()
    {
        if (isFading || alpha > 0f)
        {
            GUI.color = new Color(0, 0, 0, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }
}