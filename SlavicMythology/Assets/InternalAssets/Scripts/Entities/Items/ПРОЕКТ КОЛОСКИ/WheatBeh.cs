using UnityEngine;

public class WheatBehavior : MonoBehaviour
{
    public float bendStrength = 30.0f; // ������������ ���� �������� � ��������
    public float maxBendDistance = 2.0f; // ������������ ����������, ��� ������� ���������� ��������
    public float returnSpeed = 2.0f; // �������� �������� � ��������������� ����

    private Transform player; // ������ �� Transform ������
    private Quaternion initialRotation; // ������ ��������� ������� �������

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("����� � ����� 'Player' �� ������. ���������, ��� ����� ������� ��������������� �����.");
        }

        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < maxBendDistance)
        {
            // ������������ ������� ��������, ������� �������� � � ����������� ����������
            float bendAmount = (1 - distance / maxBendDistance) * bendStrength;

            // ���������� ����������� �� ������ � �������, ����� ������ ����������� ��������
            Vector3 directionFromPlayer = (player.position - transform.position).normalized;

            // ��������� �������� ������� �� �������� �������� ������ �����, ������� ����� ���������� ��������
            transform.rotation = Quaternion.Euler(new Vector3(directionFromPlayer.z, 0, -directionFromPlayer.x) * bendAmount) * initialRotation;
        }
        else
        {
            // ���������� ������� � �������������� ���������
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
        }
    }
}