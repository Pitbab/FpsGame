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
    [SerializeField] private CharacterController rigController;
    public bool isPosSway = true;

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
            targetPosition = rigController.velocity.normalized * swayPosMultiplier;
        }
        else
        {
            currentRotSmoothing = rotSmoothingAim;
            targetPosition = Vector3.zero;
        }
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, currentRotSmoothing * Time.deltaTime);

        
        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, posSmooting * Time.deltaTime);
    }

}
