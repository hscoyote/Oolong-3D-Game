using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;
    private void Awake()
    {
        if (instance == null)
        {
        instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioclip, Transform spawnTransform, float volume)
    {
        // spawns audio game object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        // assigns the auido sound clip
        audioSource.clip = audioclip;

        // assigns volume 
        audioSource.volume = volume; 

        // plays sound 
        audioSource.Play();

        // gets the length of the sound clip 
        float clipLength = audioSource.clip.length;
        
        // destroys the audio game object 
        Destroy(audioSource.gameObject, clipLength);

    }
}