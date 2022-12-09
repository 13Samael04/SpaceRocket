using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;



public class Rocket2 : MonoBehaviour
{

    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    
    [SerializeField] ParticleSystem flyParticles;
    [SerializeField] ParticleSystem boomParticles;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem SferaParticles;
    [SerializeField] ParticleSystem BatteryParticles;
    [SerializeField] ParticleSystem HitParticles;
    [SerializeField] ParticleSystem ImortalParticles;
    [Range(0,50000)][SerializeField] int energyTotal = 2000;
    [SerializeField] int energyApply = 50;
    [SerializeField] Text energyText;
    [SerializeField] Text PlusEnergyText;
  
    [SerializeField] Text lvlText;
    [SerializeField] int AddEnergy;
    [SerializeField] GameObject  BatteryText;

    [SerializeField] private AnalyticsProb analytics;  // Аналитика


    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {Playing, Dead, NextLevel}
    enum Sky {Flying, Stoping}
    enum Side {L, AL, R, AR, N}
    Side side = Side.N;
    Sky sky = Sky.Stoping;
    State state = State.Playing;
    bool f;
    int Sphere;


    // ПЕРЕМЕННЫЕ ДЛЯ РЕКЛАМЫ

    public SimpleADS Ad;
    public int DeathCount = 0; // количество смертей
    
    public int DeathCountAds = 0; // количество смертей для полной рекламы
    public int DeathCountAdsinPause = 0; // количество смертей для полной рекламы
    [SerializeField] GameObject AddLife;

    public Soundeffector soundeffector;  // настройки звука
     int allEnergy;


    // система звезд
    [SerializeField] int Stars1;
    [SerializeField] int Stars2;


    [SerializeField] GameObject PauseScreen;
    [SerializeField] Sprite star, blackstar;
    [SerializeField] GameObject NextLvl;
    [SerializeField] GameObject Continue;


    public int allGem, activeGem;
    public int allBattery, activeBattery;

    // скины 

    public Transform PlayerPos;
    [SerializeField] int rocketIndex;
    [SerializeField] int thisRocketIndex;
    string characterName;

    [SerializeField] Main main;

    bool HitActiv = false;
    
    public CheckPlatform checkPlatform;
    [SerializeField] GameObject Body;
    [SerializeField] Image IconRocket;
    [SerializeField] Text AmountLife;
    [SerializeField] Sprite RocketView;
    
    


    private void Awake() 
    {
        thisRocketIndex = PlayerPrefs.GetInt("Player");
        if (rocketIndex != thisRocketIndex )
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        gameObject.transform.position = PlayerPos.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        print("Сейчас жизней " + PlayerPrefs.GetInt("AddLife")); 
        print("сейчас" + PlayerPrefs.GetString("MegaHard"));
        if (thisRocketIndex == 1)
            {
                energyTotal = energyTotal +  1000;
                print("топляк" + energyTotal);
            }
        allEnergy = energyTotal + AddEnergy * GameObject.FindGameObjectsWithTag("Battery").Length;
        Debug.Log(allEnergy);
        soundeffector.PlayMusicSound();
        Time.timeScale  =1f;
        side = Side.N;
        state = State.Playing;
        sky = Sky.Stoping;
        energyText.text = energyTotal.ToString();

        DeathCount = PlayerPrefs.GetInt("DC",0);    // счетчик рекламы
        DeathCountAds = PlayerPrefs.GetInt("DCA",0);    // счетчик рекламы
        DeathCountAdsinPause = PlayerPrefs.GetInt("AddPause",0); // счетчик рекламы
        
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        allGem = GameObject.FindGameObjectsWithTag("gem").Length;
        allBattery = GameObject.FindGameObjectsWithTag("Battery").Length;


        TextLvl();
        Invoke("DestroyPanel", 3f);

        
        IconRocket.GetComponent<Image>().sprite = RocketView;

    }

    void DestroyPanel()  //убирает наименование уровня
    {
        lvlText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        AmountLife.text = PlayerPrefs.GetInt("AddLife").ToString();


        if (state == State.Playing && sky == Sky.Flying) 
        {
            Launch();
            
        }
        if (state == State.Playing && state != State.Dead)
        {
            Rotation();
        }
        if (side == Side.L && state == State.Playing)
        {
            Left();
        }
        if (side == Side.AL && state == State.Playing)
        {
            AntiLeft();
        }
        if (side == Side.R && state == State.Playing)
        {
            Right();
        }
        if (side == Side.AR && state == State.Playing)
        {
            AntiRight();
        }
        
        
        
        
        
        
    }

    void OnCollisionEnter(Collision collision) 
    {

        if (state != State.Playing)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
            print ("Ok");
            break;

            case "Finish":
            if(checkPlatform.PlatformCheck == true)
            {
                Finish();
            }
            else if (PlayerPrefs.GetString("MegaHard") == "MegaHardActive")
            {
                Finish();
            }
            else
            {
                Lose();
            }
            break;

            
            default:
            if (HitActiv == false)
            {
                if (PlayerPrefs.GetInt("AddLife") > 0)
                {
                    HitActiv = true;
                    Instantiate(HitParticles, transform.position, Quaternion.identity);
                    soundeffector.PlayHitSound();
                    ImortalParticles.Play();
                    PlayerPrefs.SetInt("AddLife", PlayerPrefs.GetInt("AddLife") - 1);
                    Invoke("HitDiactiv", 3f);
                    print("Сейчас жизней " + PlayerPrefs.GetInt("AddLife")); 
                }
                else if (PlayerPrefs.GetInt("AddLife") == 0)
                {
                
                    Lose();
                }
            }
            
            
            break;
        }
    }
    void HitDiactiv()
    {
        HitActiv = false;
        ImortalParticles.Stop();
    }

    void OnTriggerEnter(Collider collision) 
    {
        
        switch(collision.gameObject.tag)
        {
            case "Battery":
            PlusEnergy(AddEnergy, collision.gameObject);
            PlusEnergyText.enabled = true;
            PlusEnergyText.text = "+" + AddEnergy.ToString();
            Invoke("EnergyPlusDeactive",2f);
            BatteryParticles.Play();


            BatteryText.GetComponent<TextMesh>().text = "+" + AddEnergy.ToString();
            activeBattery++;
            if (activeBattery == allBattery)
            {
                AddEnergy = 0;
            }
            
            Instantiate (BatteryText,transform.position, Quaternion.identity);

            
            
            break;

            case "gem":
            
            PlusGem(collision.gameObject);
            main.GetSpheres();
            SferaParticles.Play();
            activeGem++;
            break;


            case "gemRed":
            
            PlusGem(collision.gameObject);
            main.GetSpheresRed();
            SferaParticles.Play();
            main.PlusRedGem();
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;  /// убирает красную сферу из уровня
            PlayerPrefs.SetInt("CurentRedSpheres" + currentLevelIndex, 1 );
            break;
        }
    }
    

    void PlusEnergy(int energyToAdd, GameObject batteryObj)   // играет звук из скрипта Soundbat
    {
        batteryObj.GetComponent<CapsuleCollider>().enabled = false;
        batteryObj.GetComponent<soundbat>().PlayBatterySound();
        batteryObj.GetComponent<soundbat>().InvisBattery();
        energyTotal += energyToAdd;
        energyText.text = energyTotal.ToString();
    }

    void PlusGem(GameObject gemObj)  // играет звук из скрипта Soundbat
    {
        gemObj.GetComponent<SphereCollider>().enabled = false;
        gemObj.GetComponent<soundbat>().PlayGemSound();
        gemObj.GetComponent<soundbat>().InvisGem();
    }

    

    

    void EnergyPlusDeactive() // выключает текст прибавления энергии
    {
        PlusEnergyText.enabled = false;
        
    }

    public void Finish()
    {
        state = State.NextLevel;
        audioSource.Stop();
        soundeffector.PlayFinishSound();
        finishParticles.Play();
        main.PlusGem();
        //main.PlusRedGem();
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (PlayerPrefs.GetString("MegaHard") == "MegaHardDiActive")
        {
            if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (PlayerPrefs.GetString("MegaHard") != "MegaHardDictive")
        {
            
            print("красная сфера =" + PlayerPrefs.GetInt("SpheresRed"));
            if (!PlayerPrefs.HasKey("LvlHard") || PlayerPrefs.GetInt("LvlHard") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("LvlHard", SceneManager.GetActiveScene().buildIndex);
            }
            
        }

        
        

        
        int FinishEnergy = energyTotal + AddEnergy;
        Debug.Log(FinishEnergy);
        if ((FinishEnergy) < Stars1 && !PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 1);
        }
        if ((FinishEnergy) >= Stars1 && ((FinishEnergy) <= Stars2) && (!PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex) || PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex) < 2))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 1);
            if (activeGem >= allGem)
            {
                PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 2);
            }
        }
        else if ((FinishEnergy) > Stars2 && (!PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex) || PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex) < 3))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 2);
            if (activeGem >= allGem)
            {
                PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 3);
            }
        }


        if ((FinishEnergy) < Stars1)
        {
            PauseScreen.SetActive(true);
            PauseScreen.transform.GetChild(0).GetComponent<Image>().sprite = star;
            PauseScreen.transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
            PauseScreen.transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
            PlayerPrefs.SetInt("Kol_Stars", 1);
            print(PlayerPrefs.GetInt("Kol_Stars"));
        }
        if ((FinishEnergy) >= Stars1 && ((FinishEnergy) <= Stars2))
        {
            PauseScreen.SetActive(true);
            PauseScreen.transform.GetChild(0).GetComponent<Image>().sprite = star;
            PauseScreen.transform.GetChild(1).GetComponent<Image>().sprite = star;
            PauseScreen.transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
            PlayerPrefs.SetInt("Kol_Stars", 2);
            if (activeGem < allGem)
            
            {
                PauseScreen.transform.GetChild(1).GetComponent<Image>().sprite = blackstar;
                PlayerPrefs.SetInt("Kol_Stars", 1);
            }
            print(PlayerPrefs.GetInt("Kol_Stars"));
        }
        else if ((FinishEnergy) > Stars2 ) 
        {
            PauseScreen.SetActive(true);
            PauseScreen.transform.GetChild(0).GetComponent<Image>().sprite = star;
            PauseScreen.transform.GetChild(1).GetComponent<Image>().sprite = star;
            PauseScreen.transform.GetChild(2).GetComponent<Image>().sprite = star;
            PlayerPrefs.SetInt("Kol_Stars", 3);
            if (activeGem < allGem)
            
            {
                PauseScreen.transform.GetChild(2).GetComponent<Image>().sprite = blackstar;
                PlayerPrefs.SetInt("Kol_Stars", 2);
            }
            print(PlayerPrefs.GetInt("Kol_Stars"));
        }
        NextLvl.SetActive(true);
        Continue.SetActive(false);
        print(PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex));
        GaAnalytics.instance.OnLevelComplete();
        
        
        /// Аналитика для звезд
        int levelId = SceneManager.GetActiveScene().buildIndex;
        string characterName = PlayerPrefs.GetInt("Kol_Stars").ToString();
        int sferaKol = activeGem;
        this.analytics.OnLevelStars(levelId, characterName);
        this.analytics.OnLevelSfera(levelId, sferaKol);
        



        
        
        
    }

    void Lose()
    {
        DeathCount++;
        DeathCountAds++;
        DeathCountAdsinPause++;
        PlayerPrefs.SetInt("DC",DeathCount);    //Добовляется 1 к счетчику смерти
        PlayerPrefs.SetInt("DCA",DeathCountAds);    //Добовляется 1 к счетчику смерти для полной рекламы
        PlayerPrefs.SetInt("AddPause",DeathCountAdsinPause);    //Добовляется 1 к счетчику смерти для полной рекламы
        Body.SetActive(false);

        state = State.Dead;
        audioSource.Stop();
        soundeffector.PlayBoomSound();
        boomParticles.Play();
        state = State.Dead;
        Disconect();
        if (DeathCount == 4)
        {
            Ad.ShowAd();
        }
        if (PlayerPrefs.GetInt("AddPause") >= 2)
        {
            PauseScreen.SetActive(true);
            Continue.SetActive(false);
            AddLife.SetActive(true);
            
        }
        if (PlayerPrefs.GetInt("AddPause") < 2)
        {
            Invoke("LoadFirstLevel", 2f);
        }
        int levelId = SceneManager.GetActiveScene().buildIndex;
        if (rocketIndex == thisRocketIndex )
        {
            characterName = "Rocket";
        }
        else
        {
            characterName = "Big Rocket";
        }
        
        this.analytics.OnPlayerDeath(levelId, characterName);
        
    }

    void Disconect()// корабль рассыпается на части
    {
        transform.GetChild(3).gameObject.AddComponent<Rigidbody>();
        transform.GetChild(2).gameObject.AddComponent<Rigidbody>();
        transform.GetChild(1).gameObject.AddComponent<Rigidbody>();
        transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
        transform.DetachChildren();
    }

   

    void LoadFirstLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);  //Lose
    }

    public void Launch()
    {
         if (state != State.Dead && energyTotal > 0) 
        {
            energyTotal -= Mathf.RoundToInt(energyApply * Time.deltaTime);
            energyText.text = energyTotal.ToString();
            rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
            if (audioSource.isPlaying == false) 
            {


                soundeffector.PlayFlySound();  // настройка звука


                flyParticles.Play();
            }
            
            
        }
        
        else 
            {
                audioSource.Pause();
                flyParticles.Stop();
            }
        

    }

    void Rotation()
    {
        float rotationSpeed = rotSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false;


    }
    public void MegaFly() 
    {
        
        sky = Sky.Flying;
        
         
        
    }
    public void AntiFly()
    {
        if(PlayerPrefs.GetString("MegaHard") != "MegaHardActive")
        {
            sky = Sky.Stoping;
            audioSource.Pause();
            flyParticles.Stop();
        }
        

    }
    public void Left()
    {
        side = Side.L;
        float rotationSpeed = rotSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationSpeed);
    }
    public void AntiLeft()
    {
         side = Side.AL;
        float rotationSpeed = rotSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * 0);
    }
    public void Right()
    {
        side = Side.R;
        float rotationSpeed = rotSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationSpeed);
    }
    public void AntiRight()
    {
         side = Side.AR;
        float rotationSpeed = rotSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        transform.Rotate(-Vector3.forward * 0);
    }

    



    public void TextLvl()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        lvlText.text = "Level "+currentLevelIndex.ToString();
        
    }

    public int GetSpheres()
    {
        return Sphere;
    }

    


    

}
