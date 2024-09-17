using UnityEngine;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // скорость игрока

    private Vector2 movementInput; // хранит ввод движения
    private Rigidbody2D rb; 

    [Inject]
    public void Construct(Rigidbody2D rigidbody)
    {
        rb = rigidbody; 
    }

    private void Update()
    {
        // Получаем ввод от игрока
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // Перемещение игрока
        Vector2 movementVelocity = movementInput.normalized * speed;
        rb.MovePosition(rb.position + movementVelocity * Time.fixedDeltaTime);
    }
}