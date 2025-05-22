using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerText : MonoBehaviour
{
    public float delay = 2f; // задержка перед загрузкой следующей сцены

    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject UIPanelHide;
    [SerializeField] private bool Fade = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("triggered text");

            StartCoroutine(AnimateSceneEnd());
        }
    }

    IEnumerator AnimateSceneEnd()
    {
        if (UIPanelHide != null)
        {
            UIPanelHide.SetActive(false);
        }

        UIPanel.SetActive(true);

        if (Fade)
        {
            Image panelImage = UIPanel.GetComponentInChildren<Image>();
            if (panelImage == null)
            {
                panelImage = UIPanel.AddComponent<Image>();
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
        }
    }
}
