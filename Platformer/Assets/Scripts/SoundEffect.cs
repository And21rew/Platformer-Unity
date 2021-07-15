using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip coinSound, winSound, loseSound;

    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }
}
