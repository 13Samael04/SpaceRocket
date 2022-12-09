using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class AnalyticsProb : MonoBehaviour
{
   public void OnPlayerDeath(int levelId, string characterName)
   {
       Analytics.CustomEvent(customEventName: "Player Dead", eventData: new Dictionary< string, object>()
       {
           {"Level", levelId},
           {"Charecter", characterName}
           

       });
   }

   public void OnLevelStarted(int levelId, string characterName)
   {
       Analytics.CustomEvent (customEventName: "Level Started", eventData: new Dictionary<string, object>()
       {
           {"Level", levelId},
           {"Charecter", characterName}
       });
   }

   public void OnLevelStars(int levelId, string characterName)
   {
       Analytics.CustomEvent (customEventName: "Starssssss", eventData: new Dictionary<string, object>()
       {
           {"Level", levelId},
           {"KOL-Stars", characterName}
       });
   }

   public void OnLevelSfera(int levelId, int sferaKol)
   {
       Analytics.CustomEvent (customEventName: "Sferaaaaa", eventData: new Dictionary<string, object>()
       {
           {"Level", levelId},
           {"KOL-Sfer", sferaKol}
       });
   }
}
