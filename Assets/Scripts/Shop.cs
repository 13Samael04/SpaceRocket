using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    int life;
    public Text CostBigRocket;
    public Image CostSphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("CostBigRocket") == 1)   // проверка на покупку Большой ракеты
        {
            CostBigRocket.enabled = false;
            CostSphere.enabled = false;
        }
    }

    public void SellLife()
    {
        if (PlayerPrefs.GetInt("SpheresRed") >= 10)
        {
            AddLife();
            PlayerPrefs.SetInt("SpheresRed", PlayerPrefs.GetInt("SpheresRed") - 10);
            print("Сейчас жизней " + PlayerPrefs.GetInt("AddLife")); 
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

    public void SellBigRocket()
    {
        if (PlayerPrefs.GetInt("Spheres") >= 50 && PlayerPrefs.GetInt("CostBigRocket") == 0)
        {
            PlayerPrefs.SetInt("Spheres", PlayerPrefs.GetInt("Spheres") - 50);
            PlayerPrefs.SetInt("CostBigRocket", 1); // Отключает цену у кнопки Big Rocket
            print("Купил BigRocket " + PlayerPrefs.GetInt("Player")); 
        }
    }


    void HitHp() // для теста в скрипт ракета
    {
        if (PlayerPrefs.GetInt("Addlife") > 0)
        {
            PlayerPrefs.SetInt("AddLife", PlayerPrefs.GetInt("AddLife") - 1);
            print("Сейчас жизней " + PlayerPrefs.GetInt("AddLife")); 
        }
        else if (PlayerPrefs.GetInt("Addlife") > 0)
        {

        }
    }
}
