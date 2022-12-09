using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ReklamaREWARDED : MonoBehaviour, IUnityAdsListener
{

    string gameId = "3992893";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, testMode);
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.Show(myPlacementId);
        }
        else 
        {
            Debug.Log ("Loser");
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("COOOOOL");
        }
        if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Normal");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log ("LoSeR");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId) {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
