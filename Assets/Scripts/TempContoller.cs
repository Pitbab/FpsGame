using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TempContoller : MonoBehaviour
{

    [SerializeField] private GameObject muzzleFlash, cartridgeExit, cartridge, hitPoint;
    [SerializeField] private AudioClip single, auto, magOut, magIn;
    public ParticleSystem bulletImpact;
    [SerializeField] private TMP_Text state;
    public LayerMask ignore;
    [SerializeField] private Vector3 aimingPos;


    public Animator _animator { get; private set; }
    private bool _isShooting;
    private bool _isFullReloading;
    private bool _isIdle;

    public float rateOfFire = 0.05f;

    public Camera cam;
    private MouseLook _mouseLook;
    private WeaponSway _weaponSway;
    public AudioSource _audioSource { get; private set; }
    
    public StateMachine stateMachine { get; private set; }
    public IdleGunState idleGunState { get; private set; }
    public SimpleReloadState simpleReloadState {get; private set; }
    public SingleFIreState singleFireState { get; private set; }
    public AutoFireState autoFireState { get; private set; }
    public RunningGunState runningGunState { get; private set; }
    public IdleAimingGunState idleAimingGunState { get; private set; }

    [SerializeField] private PlayerUI PlayerUI;
    [SerializeField] private MouseLook MouseLook;
    [SerializeField] private BasicPlayerController MovementController;

    public PlayerUI playerUI => PlayerUI;
    public MouseLook mouseLook => MouseLook;
    public BasicPlayerController mouvementController => MovementController;

    private void Awake()
    {
        stateMachine = new StateMachine();

        idleGunState = new IdleGunState(stateMachine, "Idle", this);
        simpleReloadState = new SimpleReloadState(stateMachine, "SimpleReload", this);
        singleFireState = new SingleFIreState(stateMachine, "Shoot", this);
        autoFireState = new AutoFireState(stateMachine, "AutoShoot", this);
        runningGunState = new RunningGunState(stateMachine, "Running", this);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _mouseLook = cam.transform.GetComponent<MouseLook>();
        _weaponSway = transform.parent.GetComponent<WeaponSway>();
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

    public void SetFlash(bool state)
    {
        muzzleFlash.SetActive(state);
    }

    public void SetFlashAngle()
    {
        muzzleFlash.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 45));
    }

    public void PlaySingleShot()
    {
        _audioSource.clip = single;
        _audioSource.Play();
    }

    public void PlayLoop()
    {
        _audioSource.loop = true;
        _audioSource.clip = auto;
        _audioSource.Play();
    }

    public void SpawnEffect(Vector3 pos, Vector3 rot)
    {
        Instantiate(bulletImpact, pos, Quaternion.LookRotation(rot));
        Instantiate(cartridge, cartridgeExit.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.right * 5f + transform.forward * Random.Range(-10, 10), ForceMode.Impulse);
        Destroy(Instantiate(hitPoint, pos, Quaternion.identity), 3f);
    }

    public void Reload()
    {
        StartCoroutine(ReloadSound());
    }

    private IEnumerator ReloadSound()
    {
        _audioSource.PlayOneShot(magOut);
        yield return new WaitForSeconds(magOut.length + 0.1f);
        _audioSource.PlayOneShot(magIn);

    }

    public void Recoil()
    {
        _mouseLook.GenerateRecoil();
    }

    public void ResetRecoil()
    {
        _mouseLook.ResetRecoil();
    }

    public void SetAimPos()
    {
        transform.localPosition = aimingPos;
    }

    public void SetSway(bool state)
    {
        _weaponSway.isPosSway = state;
    }




}
