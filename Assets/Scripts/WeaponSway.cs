using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [SerializeField] private float swayRotMultiplier, swayPosMultiplier;
    [SerializeField] private float rotSmoothingAim, rotSmoothingHip, posSmooting;
    private float currentRotSmoothing;
    private float mouseX;
    private float mouseY;
    private Quaternion rotationX;
    private Quaternion rotationY;
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    private Vector3 startingPos;
    [SerializeField] private BasicPlayerController rigController;
    [SerializeField] private MouseLook mouseLook;
    public bool isPosSway = true;

    private void Start()
    {
        startingPos = new Vector3(0, 0, (-(mouseLook.baseFov / 60f) + 1f) * 0.3f);
        Debug.Log(startingPos);
        transform.localPosition = startingPos;
    }

    void Update()
    {
        
        // sway rotation
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        
        rotationX = Quaternion.AngleAxis(-mouseY * swayRotMultiplier, Vector3.right);
        rotationY = Quaternion.AngleAxis(mouseX * swayRotMultiplier, Vector3.up);

        targetRotation = rotationX * rotationY;

        // sway position
        if (isPosSway)
        {
            currentRotSmoothing = rotSmoothingHip;
            targetPosition = startingPos + rigController.controller.velocity.normalized/6 + rigController.moveVec.normalized * swayPosMultiplier;
        }
        else
        {
            currentRotSmoothing = rotSmoothingAim;
            targetPosition = startingPos;
        }
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, currentRotSmoothing * Time.deltaTime);

        
        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, posSmooting * Time.deltaTime);
    }

}
