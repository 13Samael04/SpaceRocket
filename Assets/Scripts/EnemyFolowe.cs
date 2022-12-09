using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFolowe : MonoBehaviour
{
    // Объект, который будем двигать
    [SerializeField] GameObject MoveObject;
    // Протоганист, к которому будем двигаться
    [SerializeField] Transform Target;
    // Скорость, с которой будем двигаться
    [SerializeField] float Speed;
    // Список опорных путевых точек (далее - вэйпоинтов)
    public List<Waypoint> ReferenceWaypoints;
    Rigidbody rigidbodyS;
    private void Start()
    {
        rigidbodyS = MoveObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        
        // Создаем объект луча, летящего от нас до ракеты
        Ray ray = new Ray(MoveObject.transform.position, Target.position - transform.position);
        // Создаем луч в физическом пространстве, и информацию о столкновениях записываем в новую переменную hit
        Physics.Raycast(ray, out RaycastHit hit);
        
        Vector3 nowTarget;
        // Если луч уперся в ракету или мы уже прошли через все вэйпоинты ...
        if (hit.transform.gameObject == Target.gameObject || ReferenceWaypoints.Count == 0)
        {
            // ... значит она в прямой видимости, и к ней можно двигаться, что мы и сделаем ...
            nowTarget = Target.position;
        }
        else
        {
            // ... иначе двигаемся к ближайшему вэйпоинту
            nowTarget = ReferenceWaypoints[0].transform.position;
        }
        MoveObject.transform.position = Vector3.MoveTowards(MoveObject.transform.position, nowTarget, Time.deltaTime * Speed);
    }
}
