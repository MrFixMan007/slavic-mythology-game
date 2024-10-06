using System.Collections;
using UnityEngine;

public class ThrowEffect : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // направление отбрасывания
    public float speed = 10f; // скорость отбрасывания
    public float delay = 1f; // задержка перед отбрасыванием
    public AudioClip breakSound; // звук разбивания

    private AudioSource audioSource; 

    private void OnEnable()
    {
        // Инициализируем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Настройка положения звука
        audioSource.spatialBlend = 1f; // включаем 3D-звук
        audioSource.spatialize = true; // включаем пространственное звучание

        // Откидываем префаб в сторону через заданную задержку
        StartCoroutine(DelayedThrow());
    }

    IEnumerator DelayedThrow()
    {
        yield return new WaitForSeconds(delay); // ожидаем заданную задержку

        // Откидываем префаб в сторону
        StartCoroutine(ThrowCoroutine());
        
        yield return new WaitForSeconds(1f); // ожидаем секунду
        StartCoroutine(BreakApart());
    }

    IEnumerator ThrowCoroutine()
    {
        // Воспроизводим звук разбивания
        audioSource.PlayOneShot(breakSound);
        while (true)
        {
            
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
    }
    
    IEnumerator BreakApart()
    {
        // Разбиваем префаб на части
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                GameObject piece = child.gameObject;
                // Откидываем часть в разные стороны
                piece.transform.Translate(Random.insideUnitCircle * 10f);
            }
        }
      
        
        Object.Destroy(gameObject);
        yield return null; 
    }
}