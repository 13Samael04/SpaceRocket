using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundeffector : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip FlySound, BoomSound, MusicSound, GemSound, BatterySound, FinishSound, HitSound;

    void Start ()
    {
        audioSource.PlayOneShot(MusicSound);
    }

    public void PlayFlySound()
    {
        audioSource.PlayOneShot(FlySound);
    }
    public void StopFlySound()
    {
        audioSource.Stop();
    }

    public void PlayBoomSound()
    {
        audioSource.PlayOneShot(BoomSound);
    }

    public void PlayMusicSound()
    {
        audioSource.PlayOneShot(MusicSound);
    }

    public void PlayGemSound()
    {
        audioSource.PlayOneShot(GemSound);
    }

    public void PlayBatterySound()
    {
        audioSource.PlayOneShot(BatterySound);
    }

    public void PlayFinishSound()
    {
        audioSource.PlayOneShot(FinishSound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(HitSound);
    }
}
