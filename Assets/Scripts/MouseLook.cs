using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    
    [SerializeField] private float mouseSens = 180f;
    
    private float verticalRecoil;
    private float horizontalRecoil;
    [SerializeField] private float recoilTime;
    private float recoilRotX;
    private float recoilRotY;
    private float time = 0f;
    
    private bool needReplace = false;


    public Vector2[] recoilPattern;
    private int index;
    
    private float _rotationX = 0f;
    private float mouseX;
    private float mouseY;
    public Transform body;

    private bool inMenu = false;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (time > 0)
        {
            recoilRotX = verticalRecoil/ 1000 * Time.deltaTime / recoilTime;
            recoilRotY = horizontalRecoil / 100 * Time.deltaTime / recoilTime;
            time -= Time.deltaTime;
            needReplace = true;
        }
        else
        {
            //recoilRotX = 0;
            //recoilRotY = 0;
            if (needReplace)
            {
                StartCoroutine(RemoveKnockBack());

            }
        }

        if (!inMenu)
        {
            UpdateRotation();
        }

    }
    
    private void UpdateRotation()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        mouseX -= recoilRotY;
        
        _rotationX -= mouseY;
        _rotationX -= recoilRotX;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
            
        transform.parent.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);

    }

    private IEnumerator RemoveKnockBack()
    {
        needReplace = false;
        float timer = recoilTime * 4;

        while (timer > 0)
        {
            recoilRotX = -verticalRecoil/ 1000 * Time.deltaTime / recoilTime / 4;
            recoilRotY = -horizontalRecoil / 100 * Time.deltaTime / recoilTime / 4;
            timer -= Time.deltaTime;
            yield return null;
        }

        recoilRotX = 0;
        recoilRotY = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position, transform.forward * 1000f);
    }

    public void GenerateRecoil()
    {
        time = recoilTime;
        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        if (index != recoilPattern.Length - 1)
        {
            index += 1;
        }

    }

    private int NextIndex(int index)
    {
        return (index + 1);
    }

    public void ResetRecoil()
    {
        index = 0;
    }

    public void SetSensitivity(float sens)
    {
        mouseSens = sens;
    }

    public void SetStateMode(bool state)
    {
        inMenu = state;
    }

    public void SetCameraFov(float fov)
    {
        cam.fieldOfView = fov;
    }
}
