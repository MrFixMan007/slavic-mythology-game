using UnityEngine;

public class WheatBehavior : MonoBehaviour
{
    public float bendStrength = 30.0f; // Максимальный угол сгибания в градусах
    public float maxBendDistance = 2.0f; // Максимальное расстояние, при котором происходит сгибание
    public float returnSpeed = 2.0f; // Скорость возврата к первоначальному углу

    private Transform player; // Ссылка на Transform игрока
    private Quaternion initialRotation; // Хранит начальный поворот колоска

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Игрок с тегом 'Player' не найден. Убедитесь, что игрок помечен соответствующим тегом.");
        }

        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < maxBendDistance)
        {
            // Рассчитываем степень сгибания, линейно уменьшая её с увеличением расстояния
            float bendAmount = (1 - distance / maxBendDistance) * bendStrength;

            // Определяем направление от игрока к колоску, чтобы задать направление сгибания
            Vector3 directionFromPlayer = (player.position - transform.position).normalized;

            // Выполняем вращение колоска на заданную величину вокруг точки, которая будет основанием сгибания
            transform.rotation = Quaternion.Euler(new Vector3(directionFromPlayer.z, 0, -directionFromPlayer.x) * bendAmount) * initialRotation;
        }
        else
        {
            // Возвращаем колосок в первоначальное положение
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
        }
    }
}