using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedSthere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
      //  PlayerPrefs.SetInt("CurentRedSpheres" + currentLevelIndex, 0 );
        if (PlayerPrefs.GetString("MegaHard") != "MegaHardActive")
        {
            gameObject.SetActive(false);
        }

        
        if (PlayerPrefs.HasKey("CurentRedSpheres" + currentLevelIndex))
        {
            if (PlayerPrefs.GetInt("CurentRedSpheres" + currentLevelIndex) == 1)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CurentRedSpheres" + currentLevelIndex, 0 );
        }
        

        print (PlayerPrefs.GetInt("CurentRedSpheres" + currentLevelIndex));
    }

    // Update is called once per frame
    void Update()
    {
                
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        print (PlayerPrefs.GetInt("CurentRedSpheres" + currentLevelIndex) + "индекс красной сферы");
    }

   


}
