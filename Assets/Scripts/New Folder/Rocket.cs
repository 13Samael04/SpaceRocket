using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Rocket : MonoBehaviour
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
    [SerializeField] int energyApply = 5;
    [SerializeField] Text energyText;


    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {Playing, Dead, NextLevel}
    State state = State.Playing;

    // Start is called before the first frame update
    void Start()
    {
        energyText.text = energyTotal.ToString();
        state = State.Playing;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Playing ) 
        {
            Launch();
            Rotation();
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
        SceneManager.LoadScene(0);  //Lose
    }

    void Launch()
    {
        if (Input.GetKey(KeyCode.Space) && energyTotal > 0)
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
}
