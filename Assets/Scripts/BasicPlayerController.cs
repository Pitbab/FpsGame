using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float horAxis;
    private float verAxis;
    private float gravityForce = -9.8f;
    private float speed = 100f;
    private Vector3 moveVec;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        
        moveVec = transform.right * horAxis + transform.forward * verAxis + transform.up * gravityForce;

        if (moveVec.magnitude > 1)
        {
            moveVec /= moveVec.magnitude;
        }
        
        controller.Move(speed * moveVec  * Time.deltaTime);
    }
    
    
}
