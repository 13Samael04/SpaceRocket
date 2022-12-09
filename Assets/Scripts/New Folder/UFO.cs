using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class UFO : MonoBehaviour
{

    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    [SerializeField] AudioClip flySound;
    [SerializeField] AudioClip boomSound;

    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem flyParticles;
    [SerializeField] ParticleSystem boomParticles;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] int energyTotal = 2000;
    [SerializeField] int energyApply = 50;
    [SerializeField] Text energyText;
    [SerializeField] Text lvlText;
    


    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {Playing, Dead, NextLevel}
    enum Sky {Flying, Stoping}
    enum Side {L, AL, R, AR, N}
    Side side = Side.N;
    Sky sky = Sky.Stoping;
    State state = State.Playing;
    bool f;
     

    // Start is called before the first frame update
    void Start()
    {
        side = Side.N;
        state = State.Playing;
        sky = Sky.Stoping;
        energyText.text = energyTotal.ToString();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        TextLvl();
        Invoke("DestroyPanel", 3f);
        

    }

    void DestroyPanel()
    {
        lvlText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        cercle();
        if (state == State.Playing && sky == Sky.Flying) 
        {
            Launch();
            
        }
        if (state == State.Playing)
        {
            Rotation();
        }
        if (side == Side.L)
        {
            Left();
        }
        if (side == Side.AL)
        {
            AntiLeft();
        }
        if (side == Side.R)
        {
            Right();
        }
        if (side == Side.AR)
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
            Finish();
            break;

            case "Battery":
            PlusEnergy(1000, collision.gameObject);
            print ("PlusEnergy");
            break;

            default:
            Lose();
            break;
        }
    }

    void PlusEnergy(int energyToAdd, GameObject batteryObj)
    {
        batteryObj.GetComponent<CapsuleCollider>().enabled = false;
        energyTotal += energyToAdd;
        energyText.text = energyTotal.ToString();
        Destroy(batteryObj);
    }
    void Finish()
    {
        state = State.NextLevel;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticles.Play();
        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);
        }
        
        
            
        Invoke("LoadNextLevel", 2f);
    }

    void Lose()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(boomSound);
        boomParticles.Play();
        Invoke("LoadFirstLevel", 2f);
    }

    void LoadNextLevel() // Finish
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if(nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);

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
                audioSource.PlayOneShot(flySound);
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
        sky = Sky.Stoping;
        audioSource.Pause();
        flyParticles.Stop();
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

    void cercle()
    {
        float rotationSpeed = rotSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        //gameObject.GetComponentInChildren<Transform>().Rotate(Vector3.up * rotationSpeed);
        transform.GetChild(0).GetComponent<Transform>().Rotate(Vector3.up * rotationSpeed);
    }

    

}
