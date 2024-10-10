using System.Collections;
using UnityEngine;

public class ThrowEffect : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // ����������� ������������
    public float speed = 10f; // �������� ������������
    public float delay = 1f; // �������� ����� �������������
    public AudioClip breakSound; // ���� ����������

    private AudioSource audioSource; 

    private void OnEnable()
    {
        // �������������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // ��������� ��������� �����
        audioSource.spatialBlend = 1f; // �������� 3D-����
        audioSource.spatialize = true; // �������� ���������������� ��������

        // ���������� ������ � ������� ����� �������� ��������
        StartCoroutine(DelayedThrow());
    }

    IEnumerator DelayedThrow()
    {
        yield return new WaitForSeconds(delay); // ������� �������� ��������

        // ���������� ������ � �������
        StartCoroutine(ThrowCoroutine());
        
        yield return new WaitForSeconds(1f); // ������� �������
        StartCoroutine(BreakApart());
    }

    IEnumerator ThrowCoroutine()
    {
        // ������������� ���� ����������
        audioSource.PlayOneShot(breakSound);
        while (true)
        {
            
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
    }
    
    IEnumerator BreakApart()
    {
        // ��������� ������ �� �����
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                GameObject piece = child.gameObject;
                // ���������� ����� � ������ �������
                piece.transform.Translate(Random.insideUnitCircle * 10f);
            }
        }
      
        
        Object.Destroy(gameObject);
        yield return null; 
    }
}