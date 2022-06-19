using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Gun data")]
public class GunData : ScriptableObject
{
    [Header("Gun Sfx")] 
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private GameObject cartridge;

    [Header("Gun Ref")]
    [SerializeField] private Vector3 aimingPos;

    [Header("Sounds")] 
    [SerializeField] private AudioClip single;
    [SerializeField] private AudioClip auto;
    [SerializeField] private AudioClip magOut;
    [SerializeField] private AudioClip magIn;

    [Header("Gun Stats")] 
    [SerializeField] private float rateOfFire;
    [SerializeField] private float aimingFov;

    //damage, penetration, etc

    public GameObject Cartridge => cartridge;
    public Vector3 AimingPos => aimingPos;
    public AudioClip Single => single;
    public AudioClip Auto => auto;
    public AudioClip MagOut => magOut;
    public AudioClip MagIn => magIn;
    public float RateOfFire => rateOfFire;
    public float AimingFov => aimingFov;
    
    public void CreateBulletHole(Vector3 pos, Vector3 rot, GameObject target)
    {
        Instantiate(bulletImpact, pos, Quaternion.LookRotation(rot), target.transform);
    }
    

}