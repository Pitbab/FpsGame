using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [SerializeField] private float swayRotMultiplier, swayPosMultiplier;
    [SerializeField] private float rotSmoothing, posSmooting;
    private float mouseX;
    private float mouseY;
    private Quaternion rotationX;
    private Quaternion rotationY;
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    [SerializeField] private CharacterController rigController;
    
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        
        rotationX = Quaternion.AngleAxis(-mouseY * swayRotMultiplier, Vector3.right);
        rotationY = Quaternion.AngleAxis(mouseX * swayRotMultiplier, Vector3.up);

        targetRotation = rotationX * rotationY;
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotSmoothing * Time.deltaTime);

        targetPosition = rigController.velocity.normalized * swayPosMultiplier;
        
        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, posSmooting * Time.deltaTime);
    }
}
