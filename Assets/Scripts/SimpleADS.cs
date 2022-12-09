using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;



public class SimpleADS : MonoBehaviour
{

    string gameId = "4112595";
    bool testMode = false;

    int life;

    private void Start()
    {
        
        Advertisement.Initialize(gameId, testMode);
            
        
        
        
    }
    

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            PlayerPrefs.SetInt("DC",0); // обнуляем рекламу в Rocket2
        }
        else 
        {
            Debug.Log("херня");
            PlayerPrefs.SetInt("DC",0); // обнуляем рекламу в Rocket2
        }

        
    }

    public void LongAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            PlayerPrefs.SetInt("DCA",0); // обнуляем рекламу в Rocket2
            AddLife();
            
        }
        else 
        {
            Debug.Log("херня");
           
        }
    }


    void AddLife()
    {
        PlayerPrefs.SetInt("AddLife", PlusLife());
    }

    int PlusLife()
    {
        life++;
        return life;
    }




    public void LongAdsForPause()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            PlayerPrefs.SetInt("AddPause",0); // обнуляем рекламу в Rocket2
            AddLife();
            Invoke("Restart", 1f);
            
        }
        else 
        {
            Debug.Log("херня");
           
        }
    }
    void Restart()  // перезагружает уровень
    {
        int currentLvl = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLvl);
    }
}
