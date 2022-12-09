using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//скрипт висит на камере

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    public Button restart, choseLvl;

    int Sphere = -1;
    int SphereRed = -1;
    
    public void Restart()  // перезагружает уровень
    {
        int currentLvl = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLvl);
    }

    public void ChoseLvl()  // выводит на главное меню
    {
        SceneManager.LoadScene(0);
        
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
       
    }

    public void PauseOff()
    {
        Time.timeScale  =1f;
      

    }

    public void LoadNextLevel() // Finish
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if(nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);

    }

    public void PlusGem() // Добавляет желтую сферу после финиша 
    {
        if (PlayerPrefs.HasKey("Spheres"))
        {
            PlayerPrefs.SetInt("Spheres", PlayerPrefs.GetInt("Spheres") + GetSpheres());
        }
        else 
        {
            PlayerPrefs.SetInt("Spheres", GetSpheres());
        }
        print("Всего сфер = " + PlayerPrefs.GetInt("Spheres") );
    }

    public int GetSpheres()
    {
        Sphere++;
        return Sphere;
    }

     public void PlusRedGem()
    {
        if (PlayerPrefs.HasKey("SpheresRed"))
        {
            PlayerPrefs.SetInt("SpheresRed", PlayerPrefs.GetInt("SpheresRed") + GetSpheresRed());
            
        }
        else 
        {
            PlayerPrefs.SetInt("SpheresRed", GetSpheresRed());
            
        }
        print(PlayerPrefs.GetInt("SpheresRed"));
    }

    public int GetSpheresRed()
    {
        SphereRed++;
        return SphereRed;
    }
}
