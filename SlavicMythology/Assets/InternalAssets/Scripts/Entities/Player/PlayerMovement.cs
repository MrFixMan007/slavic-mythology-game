using UnityEngine;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // �������� ������

    private Vector2 movementInput; // ������ ���� ��������
    private Rigidbody2D rb; 

    [Inject]
    public void Construct(Rigidbody2D rigidbody)
    {
        rb = rigidbody; 
    }

    private void Update()
    {
        // �������� ���� �� ������
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // ����������� ������
        Vector2 movementVelocity = movementInput.normalized * speed;
        rb.MovePosition(rb.position + movementVelocity * Time.fixedDeltaTime);
    }
}