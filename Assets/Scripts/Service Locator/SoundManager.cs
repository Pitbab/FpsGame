using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : ISoundService
{
    private AudioSource audioSource;
    private GameObject obj;
    private const string name = "AudioService";
    private SoundManager instance;

    public void PlaySound(AudioClip clip, Vector3 position, float volume)
    {
        obj.transform.position = position;
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);
    }

    public void InitService()
    {
        obj = new GameObject();
        obj.name = name;
        audioSource = obj.AddComponent<AudioSource>();
        Object.DontDestroyOnLoad(obj);
    }
}
