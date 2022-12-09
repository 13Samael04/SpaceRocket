using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] Vector3 Rotation;
    [SerializeField] float Speed;
    [SerializeField] GameObject MoveObject;
    // Протоганист, к которому будем двигаться
    [SerializeField] Transform Target;
    [SerializeField] Vector3 movePosition;
    [SerializeField] [Range(0,1)] float moveProgress;
    [SerializeField]  float moveSpeed;
    Vector3 startPosition;


    void Start()
    {
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Rotation * Speed * Time.deltaTime);
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);
        Vector3 offset = movePosition * moveProgress;
        transform.position = startPosition + offset;
        
    }

    void FixedUpdate() 
    {
         // Создаем объект луча, летящего от нас до ракеты
        Ray ray = new Ray(MoveObject.transform.position, Target.position - transform.position);
        // Создаем луч в физическом пространстве, и информацию о столкновениях записываем в новую переменную hit
        Physics.Raycast(ray, out RaycastHit hit);
    }
}
