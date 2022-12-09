using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundbat : MonoBehaviour
{


    public AudioSource audioSource;
    public AudioClip BatterySound, GemSound;
    

    public void PlayBatterySound()
    {
        audioSource.PlayOneShot(BatterySound);
    }

    public void InvisBattery()
    {
        transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

        Invoke("Destroybattery",2f);
    }

    void Destroybattery()
    {
        Destroy(gameObject, 1f);
    }


    public void PlayGemSound()
    {
        audioSource.PlayOneShot(GemSound);
    }

    public void InvisGem()
    {
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("Destroybattery",2f);
    }

}
