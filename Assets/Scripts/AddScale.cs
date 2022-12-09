using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScale : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;
    [SerializeField] float Speed;
    [SerializeField] [Range(0,1)] float moveProgress;
    Vector3 startPositionScale;
    Transform startPositionRotation;
    [SerializeField] float rotationSpeed;
    void Start()
    {
        startPositionScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        moveProgress = Mathf.PingPong(Time.time * Speed, 1);
        Vector3 offset = movePosition * moveProgress;
        transform.localScale = startPositionScale + offset;
        transform.GetComponent<Transform>().Rotate(Vector3.forward * rotationSpeed);
    }
}
