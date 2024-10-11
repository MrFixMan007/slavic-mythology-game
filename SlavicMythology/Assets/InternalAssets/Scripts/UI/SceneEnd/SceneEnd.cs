using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneEnd : MonoBehaviour
{
    public float delay = 2f; // задержка перед загрузкой следующей сцены
    public string TextSceneEnd;
    public string mainMenuSceneName = "MainMenuDan"; // set the scene name here

    [SerializeField] private TMP_Text SceneEndText; // ссылка на UI текст элемент
    [SerializeField] private GameObject SceneEndPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("triggeeeerrrr");

            // Остановить игровой процесс
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

        float duration = 2f; // время затемнения
        float timer = 0f;

        Color initialColor = panelImage.color;
        initialColor.a = 0f;
        panelImage.color = initialColor;

        while (timer < duration)
        {
            float alpha = timer / duration;
            panelImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            timer += Time.unscaledDeltaTime; // Используем Time.unscaledDeltaTime для корректной работы анимации при остановленном игровом процессе
            yield return null;
        }

        // Wait until the fade-in is complete
        panelImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        // Show the text
        SceneEndText.gameObject.SetActive(true);
        SceneEndText.text = TextSceneEnd;

        // Wait for the delay
        yield return new WaitForSecondsRealtime(delay); // Используем WaitForSecondsRealtime для корректной работы задержки при остановленном игровом процессе

        // Load the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);

        // Возобновить игровой процесс
        Time.timeScale = 1f;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}