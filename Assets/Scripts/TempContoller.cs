using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TempContoller : MonoBehaviour
{

    [SerializeField] private GameObject muzzleFlash, cartridgeExit;
    [SerializeField] private TMP_Text state;
    public LayerMask ignore;
    [SerializeField] private GunData gunData;


    public Animator _animator { get; private set; }
    private bool _isShooting;
    private bool _isFullReloading;
    private bool _isIdle;

    public float rateOfFire = 0.05f;

    public float normalFov { get; private set; }

    public Camera cam;
    private MouseLook _mouseLook;
    private WeaponSway _weaponSway;
    public AudioSource _audioSource { get; private set; }

    #region StateMachine variables

    public StateMachine stateMachine { get; private set; }
    public IdleGunState idleGunState { get; private set; }
    public SimpleReloadState simpleReloadState {get; private set; }
    public SingleFIreState singleFireState { get; private set; }
    public AutoFireState autoFireState { get; private set; }
    public RunningGunState runningGunState { get; private set; }
    public IdleAimingGunState idleAimingGunState { get; private set; }

    #endregion
    
    [SerializeField] private PlayerUI PlayerUI;
    [SerializeField] private MouseLook MouseLook;
    [SerializeField] private BasicPlayerController MovementController;

    public PlayerUI playerUI => PlayerUI;
    public MouseLook mouseLook => MouseLook;
    public BasicPlayerController mouvementController => MovementController;


    #region Unity Events Functions

    private void Awake()
    {
        stateMachine = new StateMachine();

        idleGunState = new IdleGunState(stateMachine, "Idle", this, gunData);
        simpleReloadState = new SimpleReloadState(stateMachine, "SimpleReload", this, gunData);
        singleFireState = new SingleFIreState(stateMachine, "Shoot", this, gunData);
        autoFireState = new AutoFireState(stateMachine, "AutoShoot", this, gunData);
        runningGunState = new RunningGunState(stateMachine, "Running", this, gunData);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _mouseLook = cam.transform.GetComponent<MouseLook>();
        _weaponSway = transform.parent.GetComponent<WeaponSway>();

        normalFov = cam.fieldOfView;
        stateMachine.Initialize(idleGunState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        state.text = stateMachine.currentState.ToString();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdated();
    }

    #endregion

    #region Setter

    public void SetFlash(bool flashState)
    {
        muzzleFlash.SetActive(flashState);
    }

    public void SetFlashAngle()
    {
        muzzleFlash.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 45));
    }
    
    public void SetAimPos()
    {
        transform.localPosition = gunData.AimingPos;
    }

    public void SetSway(bool swayState)
    {
        _weaponSway.isPosSway = swayState;
    }

    public void SetFov(float fov)
    {
        cam.fieldOfView = fov;
    }
    

    #endregion

    #region Linker

    public void Recoil()
    {
        _mouseLook.GenerateRecoil();
    }

    public void ResetRecoil()
    {
        _mouseLook.ResetRecoil();
    }

    #endregion

    #region Sound

    public void PlaySingleShot()
    {
        _audioSource.clip = gunData.Single;
        _audioSource.Play();
        
        //only testing, no need for serviceLocator on component inside this gameObject hierarchy
        //ServiceLocator.Current.Get<ISoundService>().PlaySound(single, transform.position, 1f);
    }

    public void PlayLoop()
    {
        _audioSource.loop = true; 
        _audioSource.clip = gunData.Auto ;
        _audioSource.Play();
    }

    #endregion

    #region Others
    
    public void SpawnEffect(Vector3 pos, Vector3 rot)
    {
        gunData.CreateBulletHole(pos, rot);
        Instantiate(gunData.Cartridge, cartridgeExit.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.right * 5f + transform.forward * Random.Range(-10, 10), ForceMode.Impulse);
    }

    public void Reload()
    {
        StartCoroutine(ReloadSound());
    }

    private IEnumerator ReloadSound()
    {
        _audioSource.PlayOneShot(gunData.MagOut);
        yield return new WaitForSeconds(gunData.MagOut.length + 0.1f);
        _audioSource.PlayOneShot(gunData.MagIn);

    }

    #endregion
    
}
