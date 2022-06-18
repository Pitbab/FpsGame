using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlayerController : MonoBehaviour
{
    [SerializeField] private Transform checkGroundPlace, vaultingPlace;
    public PlayerData playerData;
    
    public CharacterController controller;
    public Vector3 moveVec { get; private set;}
    private Vector3 velocity;
    private bool inMenu = false;

    #region State Machine Varaibles

    [SerializeField] private TMP_Text playerStateDebug;
    public PlayerState lastState;
    public StateMachine stateMachine { get; private set; }
    public PlayerStandingState playerStandingState { get; private set; }
    public PlayerFallingState playerFallingState { get; private set; }
    public PlayerJumpingState playerJumpingState { get; private set; }
    public PlayerRunningState playerRunningState { get; private set; }
    public PlayerVaultingState playerVaultingState { get; private set; }

    #endregion


    private void Awake()
    {
        stateMachine = new StateMachine();
        playerStandingState = new PlayerStandingState(this, stateMachine, playerData);
        playerFallingState = new PlayerFallingState(this, stateMachine, playerData);
        playerJumpingState = new PlayerJumpingState(this, stateMachine, playerData);
        playerRunningState = new PlayerRunningState(this, stateMachine, playerData);
        playerVaultingState = new PlayerVaultingState(this, stateMachine, playerData);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stateMachine.Initialize(playerStandingState);
    }

    private void Update()
    {
        if(inMenu) return;
        stateMachine.currentState.Update();
        playerStateDebug.text = stateMachine.currentState.ToString();
        
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdated();
    }

    public void SwitchMenuState(bool state)
    {
        inMenu = state;
    }

    public void Move(float horAxis, float verAxis, float speed)
    {
        moveVec = transform.right * horAxis + transform.forward * verAxis;

        if (moveVec.magnitude > 1)
        {
            moveVec /= moveVec.magnitude;
        }
        
        controller.Move(moveVec * (speed * Time.deltaTime));
    }
    
    public void Jump()
    {
        velocity = moveVec * playerData.JumpSpeed/2;
        velocity.y = Mathf.Sqrt(playerData.JumpSpeed * -1f * playerData.GravityForce);
    }

    public void Vault()
    {
        velocity = Vector3.zero;
        velocity.y = Mathf.Sqrt(playerData.JumpSpeed * -1f * playerData.GravityForce);
    }
    
    public void GroundedVelocity()
    {
        velocity = Vector3.zero;
        velocity.y = -10f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void FallingVelocity()
    {
        velocity.y += playerData.GravityForce * Time.deltaTime;

        if (velocity.y < playerData.TerminalVelocity)
        {
            velocity.y = playerData.TerminalVelocity;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    public bool CheckGround()
    {
        return Physics.CheckSphere(checkGroundPlace.position, playerData.GroundCheckRange, playerData.WhatIsGround);
    }

    public bool CheckVault()
    {
        return Physics.Raycast(vaultingPlace.position, transform.forward, playerData.VaultDetectionRange, playerData.WhatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(checkGroundPlace.position, playerData.GroundCheckRange);
        Gizmos.DrawRay(vaultingPlace.position, transform.forward * playerData.VaultDetectionRange); 
    }


}
