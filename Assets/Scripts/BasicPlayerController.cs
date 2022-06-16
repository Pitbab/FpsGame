using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlayerController : MonoBehaviour
{
    public LayerMask whatIsGround;
    [SerializeField] private Transform checkGroundPlace;
    
    public CharacterController controller;
    private float gravityForce = -30f;
    private float terminalVelocity = -50f;
    public Vector3 moveVec { get; private set;}
    private Vector3 velocity;

    private bool inMenu = false;
    
    public float walkingSpeed = 6f;
    public float fallingSpeed = 5f;
    private int score;
    
    #region State Machine Varaibles

    [SerializeField] private TMP_Text playerStateDebug;
    private float jumpSpeed = 5f;
    public StateMachine stateMachine { get; private set; }
    public PlayerStandingState playerStandingState { get; private set; }
    public PlayerFallingState playerFallingState { get; private set; }
    public PlayerJumpingState playerJumpingState { get; private set; }

    #endregion


    private void Awake()
    {
        stateMachine = new StateMachine();
        playerStandingState = new PlayerStandingState(this, stateMachine);
        playerFallingState = new PlayerFallingState(this, stateMachine);
        playerJumpingState = new PlayerJumpingState(this, stateMachine);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stateMachine.Initialize(playerStandingState);
    }

    private void Update()
    {
        
        stateMachine.currentState.Update();
        playerStateDebug.text = stateMachine.currentState.ToString();
        if(inMenu) return;
        
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
        
        controller.Move(moveVec * speed * Time.deltaTime);
    }
    
    public void Jump()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        velocity = dir * controller.velocity.magnitude / 2; 
        velocity.y = Mathf.Sqrt(jumpSpeed * -1f * gravityForce);
    }
    
    public void GroundedVelocity()
    {
        velocity.y = -10f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void FallingVelocity()
    {
        velocity.y += gravityForce * Time.deltaTime;

        if (velocity.y < terminalVelocity)
        {
            velocity.y = terminalVelocity;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    public bool CheckGround()
    {
        return Physics.CheckSphere(checkGroundPlace.position, 0.3f, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(checkGroundPlace.position, 0.3f);
    }

    public void ReceiveScore(int amount)
    {
        score += amount;
    }
    

}
