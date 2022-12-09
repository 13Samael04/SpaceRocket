using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRocket : MonoBehaviour
{
    public Transform PlayerPos;
    public GameObject RocketActive1, RocketActive2;
    public GameObject  RocketDeactive1, RocketDeactive2;
    // Start is called before the first frame update

    void Start()
    {
        if (PlayerPrefs.GetInt("Player") ==0)   // Обычная ракета
        {
            RocketActive1.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Player") ==1)   // Большая ракета
        {
            RocketActive2.SetActive(true);
        }
        
        
    }
    public void RocketActivator()
    {
        if (PlayerPrefs.GetInt("Player") ==0)   // Обычная ракета
        {
            RocketActive1.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Player") ==1)    // Большая ракета
        {
            RocketActive2.SetActive(true);
        }
        
        
    }

    public void RocketDeactivated()
    {
        if (PlayerPrefs.GetInt("Player") ==1)
        {
            RocketActive1.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Player") ==0)
        {
            RocketActive2.SetActive(false);
        }
    }
}
