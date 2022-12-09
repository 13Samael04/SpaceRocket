using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massive : MonoBehaviour
{
    public GameObject[] ReferenceWaypoint = new GameObject[30];
    void Start()
    {
        for(int c = 1; c < ReferenceWaypoint.Length+1; c++)
        {
            ReferenceWaypoint[c-1] = GameObject.Find("ReferenceWaypoint ("+c+")");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
