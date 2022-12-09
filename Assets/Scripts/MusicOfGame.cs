using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOfGame : MonoBehaviour
{
    void Awake() 
	{
        
		int numMisic = FindObjectsOfType<MusicOfGame>().Length;
		if (numMisic > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
		
	}
}
