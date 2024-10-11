using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneEnd : MonoBehaviour
{
    public float delay = 2f; // �������� ����� ��������� ��������� �����
    public string TextSceneEnd;
    public string mainMenuSceneName = "MainMenuDan"; // set the scene name here

    [SerializeField] private TMP_Text SceneEndText; // ������ �� UI ����� �������
    [SerializeField] private GameObject SceneEndPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("triggeeeerrrr");

            // ���������� ������� �������
            Time.timeScale = 0f;

            StartCoroutine(AnimateSceneEnd());
        }
    }

    IEnumerator AnimateSceneEnd()
    {
        SceneEndPanel.SetActive(true);

        Image panelImage = SceneEndPanel.GetComponent<Image>();
        if (panelImage == null)
        {
            panelImage = SceneEndPanel.AddComponent<Image>();
        }

        float duration = 2f; // ����� ����������
        float timer = 0f;

        Color initialColor = panelImage.color;
        initialColor.a = 0f;
        panelImage.color = initialColor;

        while (timer < duration)
        {
            float alpha = timer / duration;
            panelImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            timer += Time.unscaledDeltaTime; // ���������� Time.unscaledDeltaTime ��� ���������� ������ �������� ��� ������������� ������� ��������
            yield return null;
        }

        // Wait until the fade-in is complete
        panelImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        // Show the text
        SceneEndText.gameObject.SetActive(true);
        SceneEndText.text = TextSceneEnd;

        // Wait for the delay
        yield return new WaitForSecondsRealtime(delay); // ���������� WaitForSecondsRealtime ��� ���������� ������ �������� ��� ������������� ������� ��������

        // Load the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);

        // ����������� ������� �������
        Time.timeScale = 1f;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}