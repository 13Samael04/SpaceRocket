using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlatform : MonoBehaviour
{
    public bool PlatformCheck = false;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision) 
    {
        switch(collision.gameObject.name)
        {
            case "Rocket":
            print("садится жопой");
            PlatformCheck = true;

            break;
            case "AtomRocket":
            print("садится жопой");
            PlatformCheck = true;

            break;
        }
    }

    public void Chehck()
    {
        if (PlatformCheck == true)
        {
            
        }
    }
}
