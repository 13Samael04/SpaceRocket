using UnityEngine;

// Класс для объекта опорной путевой точки (далее - вэйпоинт)
public class Waypoint : MonoBehaviour
{
    // Ссылка на скрипт в объекте врага. Нужна, чтобы удалять вэйпоинт из списка, и чтобы к нему уже не возвращаться
    EnemyFolowe enemyFolow;
    private void Start()
    {
        // Ссылка автоматически присваевается, т.к., такой скрипт на сцене только один
        enemyFolow = FindObjectOfType<EnemyFolowe>();
    }
    // Когда какой-то объект выйдет из тригера...
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем: этот объект враг, или нет?
        if (other.gameObject == enemyFolow.gameObject)
        {
            // Удаляем себя из массива вэйпоинтов
            enemyFolow.ReferenceWaypoints.Remove(gameObject.GetComponent<Waypoint>());
            // Уничтожаем себя со сцены, чтобы не тратить ресурсы устройства. Вэйпоинт нужен только один раз, поэтому от него можно избавиться
            Destroy(gameObject);
        }
    }
}
