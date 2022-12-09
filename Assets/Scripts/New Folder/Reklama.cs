using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Reklama : MonoBehaviour
{


    string gameId = "3992893";
    bool testMode = false;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else 
        {
            Debug.Log("Херня");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
