using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotation : MonoBehaviour
{
    Vector3 centerPosition;
    [SerializeField] [Range(0,1)] float rotateProgress;
    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rotateProgress = Mathf.PingPong(Time.time * moveSpeed, 1);
        
    }
}
