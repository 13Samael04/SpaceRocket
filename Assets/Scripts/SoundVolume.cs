using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{


    
    public AudioSource soundSource, effectsSource;




    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        soundSource.volume = (float)PlayerPrefs.GetInt("Sound") / 20 ;  
        effectsSource.volume = (float)PlayerPrefs.GetInt("EffectsSound") / 20 ;
        
    }
}
