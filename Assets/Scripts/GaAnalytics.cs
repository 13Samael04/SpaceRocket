using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GaAnalytics : MonoBehaviour
{

    public static GaAnalytics instance;

    private void Awake() 
    {
        instance = this;

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.Initialize();
    }

    // Update is called once per frame
    public void OnLevelComplete()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level: " );
        Debug.Log ("Level: " );
    }
}
