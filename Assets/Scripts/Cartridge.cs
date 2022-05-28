using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cartridge : MonoBehaviour
{

    private AudioSource source;
    public List<AudioClip> ShellSound;
    private bool playedSound;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Destroy(gameObject, 2);
        playedSound = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (playedSound) return;
        source.PlayOneShot(ShellSound[Random.Range(0, ShellSound.Count)]);
        playedSound = true;

    }
}
