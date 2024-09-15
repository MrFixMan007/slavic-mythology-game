using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class FairyTaleCutscene : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public Image[] images;            // ������ ����������� ��� ��������
    public float[] displayTimes;      // ������ ������� ����������� ������� ����������� (� ��������)
    public float transitionDuration = 1.0f; // ������������ �������� ����� �������������
    private IGameLoadProducer _gameLoadProducer;

    private int currentImageIndex = 0;
    
    [Inject]
    public void Construct(IGameLoadProducer gameLoadProducer)
    {
        _gameLoadProducer = gameLoadProducer;
    }

    void Start()
    {
        // ��������, ��� ���������� ����������� � ������� ����������� ���������
        if (images.Length == 0 || displayTimes.Length == 0)
        {
            Debug.LogError("������ ����������� ��� ������� ����������� ����");
            return;
        }
        if (images.Length != displayTimes.Length)
        {
            Debug.LogError("������� ����������� � ������� ����������� ������ ����� ���������� �����..");
            return;
        }

        // ������ ��� ����������� ����� ������� ��������
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
            // ����� �������� �����������
            images[currentImageIndex].gameObject.SetActive(true);
            yield return StartCoroutine(FadeIn(images[currentImageIndex]));

            // ���� �����, ��������� ��� �������� �����������
            yield return new WaitForSeconds(displayTimes[currentImageIndex]);

            // ������� � ���������� ����������� (��������� ��������)
            yield return StartCoroutine(FadeOut(images[currentImageIndex]));

            currentImageIndex++;
        }

        // ���������� ��������, ����� ��������� ��������� ����� ��� ��������� ������ ���
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
        image.gameObject.SetActive(false); // ��������� �����������
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
