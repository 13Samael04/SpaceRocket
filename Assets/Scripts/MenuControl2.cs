using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl2 : MonoBehaviour
{
 
    public Button[] Lvls;
    [SerializeField] Text Spheres;  // желтая сфкра в главном меню
    [SerializeField] Text SpheresRed; // красная сфера после прохлждения мега хард режима
    [SerializeField] GameObject RedBackGround;
    [SerializeField] Text KolvoStrasNormal,  KolvoStrasHard;
    [SerializeField] Button Block_Normal, Block_Hard, AddRedSfera;
    [SerializeField] Sprite star, blackstar;
    [SerializeField] GameObject Shop1, Shop2;
    

    public Slider SoundSlider, EffectsSlider;  // слайдер звука Музыки и Эфектов
    public Text soundText, effectsText;   //  к слайдеру значение звука Музыки и Эфектов
    public Main main;
    public Massive1 massive1;

 
    // Start is called before the first frame update
    private void Awake() 
    {
        SoundSlider.value = PlayerPrefs.GetInt("Sound");
        EffectsSlider.value = PlayerPrefs.GetInt("EffectsSound");
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("Kol-vo_Stars"))
        {
            PlayerPrefs.SetInt("Kol-vo_Stars", 0);
            PlayerPrefs.SetInt("Tranzit-Stars", 0);
        }
        
        Time.timeScale  =1f;
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 5);
        }
        if (!PlayerPrefs.HasKey("MegaHard"))
        {
            PlayerPrefs.SetString("MegaHard", "MegaHardDiActive");
        }
        

        
    }  

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
       
    }

    // Update is called once per frame
    void Update()
    {
        massive1.SummaZvezd();
        KolvoStrasNormal.text = PlayerPrefs.GetInt("Kol-vo_Stars").ToString();
        KolvoStrasHard.text = PlayerPrefs.GetInt("Kol-vo_Stars").ToString();
        print (PlayerPrefs.GetInt("Kol-vo_Stars") + " Всего звезд");
 
        
        if (PlayerPrefs.GetString("MegaHard") == "MegaHardDiActive") // Открывает кнопки уровней в нормальном режиме
        {
            for (int i = 0; i < Lvls.Length; i++)
            { 

                if (i <= PlayerPrefs.GetInt("Lvl"))
                {
                    Lvls[i].interactable = true;
                    if (i >= 50 && PlayerPrefs.GetInt("Kol-vo_Stars") >= 120)
                    {
                        Block_Normal.interactable = true;
                        

                        if (i == 100)
                        {
                        
                            Block_Hard.interactable = true;
                        }
                    }
                    
                }
                
                else 
                {
                    Lvls[i].interactable = false;
                }
            }
            for (int i = 1; i < 50; i++)
            {
                if (PlayerPrefs.HasKey("stars" + i))
                {
                    
                    if (PlayerPrefs.GetInt("stars" + i) ==1)
                    {
                        Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = star;
                        Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                    }
                    else if (PlayerPrefs.GetInt("stars" +i) ==2 )
                    {
                        Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = star;
                        Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = star;
                        Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                    }
                    else 
                    {
                        Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = star;
                        Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = star;
                        Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = star;
                    }
                }
                else 
                {
                    Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = blackstar;
                    Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                    Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                }
                
            }
        }
        else if ( PlayerPrefs.GetString("MegaHard") == "MegaHardActive" ) // открывает уровне в ХАРД режиме
        {
            for (int i = 0; i < Lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("LvlHard") && i <= PlayerPrefs.GetInt("Lvl"))
                {
                    Lvls[i].interactable = true;
                    if (i >= 50)
                    {
                        Block_Normal.interactable = true;
                        

                        if (i == 100)
                        {
                        
                            Block_Hard.interactable = true;
                        }
                    }
                    
                }
                
                else 
                {
                    Lvls[i].interactable = false;
                }
            }
            for (int i = 1; i < 50; i++)
            {
                if (PlayerPrefs.HasKey("stars" + i))
                {
                    
                    if (PlayerPrefs.GetInt("stars" + i) ==1)
                    {
                        Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                    }
                    else if (PlayerPrefs.GetInt("stars" +i) ==2 )
                    {
                        Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                    }
                    else 
                    {
                        Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                        Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                    }
                }
                else 
                {
                    Lvls[i-1].transform.GetChild(0).GetComponent<Image>().sprite = blackstar;
                    Lvls[i-1].transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                    Lvls[i-1].transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                }
                
            }
        }

        
        
        
        
        
        
        
        if (PlayerPrefs.HasKey("Spheres"))
        {
            Spheres.text = PlayerPrefs.GetInt("Spheres").ToString();
        }
        else
        {
            Spheres.text = "0";
        }

        if (PlayerPrefs.HasKey("SpheresRed"))
        {
            SpheresRed.text = PlayerPrefs.GetInt("SpheresRed").ToString();
        }
        else
        {
            SpheresRed.text = "0";
        }


        PlayerPrefs.SetInt("Sound", (int)SoundSlider.value);
        soundText.text = SoundSlider.value.ToString();

        PlayerPrefs.SetInt("EffectsSound", (int)EffectsSlider.value);
        effectsText.text = EffectsSlider.value.ToString();

        if (PlayerPrefs.GetInt("DCA") >= 5)
        {
            Shop1.SetActive(false);
            Shop2.SetActive(true);
            AddRedSfera.interactable = true;
            RedBackGround.SetActive(true);
        }
        else 
        {
            AddRedSfera.interactable = false;
            Shop1.SetActive(true);
            Shop2.SetActive(false);
            RedBackGround.SetActive(false);
        }
    }

    public void Lvl_del()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetPlayer(int index)
    {
        if (PlayerPrefs.GetInt("CostBigRocket") == 1)
        {
            PlayerPrefs.SetInt("Player", index);
        }   
        
    }

    
}
