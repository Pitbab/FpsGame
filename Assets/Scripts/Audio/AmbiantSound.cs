using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AmbiantSound : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clips[Random.Range(0, clips.Count - 1)]);
    }

    private void Update()
    {
        if(source.isPlaying) return;
        
        source.PlayOneShot(clips[Random.Range(0, clips.Count - 1)]);
    }
}
