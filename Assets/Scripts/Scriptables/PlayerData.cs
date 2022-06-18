using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("States Speed")]
    [SerializeField, Range(0, 20)] private int walkSpeed;
    [SerializeField, Range(0, 20)] private int runSpeed;
    [SerializeField, Range(0, 20)] private int crouchSpeed;
    [SerializeField, Range(0, 20)] private int slideSpeed;

    [Header("ground detection")] 
    [SerializeField, Range(0, 5)] private float groundCheckRange;
    [SerializeField] private LayerMask whatIsGround;

    [Header("gravity")] 
    [SerializeField, Range(-100, 100)] private int gravityForce;
    [SerializeField, Range(-100, 0)] private int terminalVelocity;
    [SerializeField, Range(0, 20)] private int jumpSpeed;
    
    public int WalkSpeed => walkSpeed;
    public int RunSpeed => runSpeed;
    public int CrouchSpeed => crouchSpeed;
    public int SlideSpeed => slideSpeed;

    public float GroundCheckRange => groundCheckRange;
    public LayerMask WhatIsGround => whatIsGround;

    public int GravityForce => gravityForce;
    public int TerminalVelocity => terminalVelocity;
    public int JumpSpeed => jumpSpeed;


}
