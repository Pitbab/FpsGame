using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        InvokeRepeating("Rotate", 0f, 0.1f);
    }
    
    void Update()
    {

    }

    public void Rotate()
    {
        transform.rotation = Quaternion.LookRotation(transform.parent.forward);
        transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(-20, 20));
    }
}
