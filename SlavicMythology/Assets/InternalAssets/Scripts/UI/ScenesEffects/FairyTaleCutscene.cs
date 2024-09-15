using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class FairyTaleCutscene : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public Image[] images;            // Массив изображений для катсцены
    public float[] displayTimes;      // Массив времени отображения каждого изображения (в секундах)
    public float transitionDuration = 1.0f; // Длительность перехода между изображениями
    private IGameLoadProducer _gameLoadProducer;

    private int currentImageIndex = 0;
    
    [Inject]
    public void Construct(IGameLoadProducer gameLoadProducer)
    {
        _gameLoadProducer = gameLoadProducer;
    }

    void Start()
    {
        // Проверка, что количество изображений и времени отображения совпадают
        if (images.Length == 0 || displayTimes.Length == 0)
        {
            Debug.LogError("Массив изображений или времени отображения пуст");
            return;
        }
        if (images.Length != displayTimes.Length)
        {
            Debug.LogError("Массивы изображений и времени отображения должны иметь одинаковую длину..");
            return;
        }

        // Скрыть все изображения перед началом катсцены
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        while (currentImageIndex < images.Length)
        {
            // Показ текущего изображения
            images[currentImageIndex].gameObject.SetActive(true);
            yield return StartCoroutine(FadeIn(images[currentImageIndex]));

            // Ждем время, указанное для текущего изображения
            yield return new WaitForSeconds(displayTimes[currentImageIndex]);

            // Переход к следующему изображению (затухание текущего)
            yield return StartCoroutine(FadeOut(images[currentImageIndex]));

            currentImageIndex++;
        }

        // Завершение катсцены, можно загрузить следующую сцену или выполнить другой код
        LoadNextScene();
    }

    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            image.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0), t);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0);
        image.gameObject.SetActive(false); // Отключаем изображение
    }

    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0f;
        Color startColor = new Color(image.color.r, image.color.g, image.color.b, 0);
        image.color = startColor;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            image.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 1), t);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 1);
    }

    void LoadNextScene()
    {
        _gameLoadProducer.StartGame();
    }
}
