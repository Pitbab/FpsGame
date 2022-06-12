using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private Image crossHair;
    [SerializeField] private GameObject pauseMenuPanel;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void SwitchCrossHairMode(bool state)
    {
        crossHair.enabled = state;
    }

    public void SwitchMenuState(bool state)
    {
        Cursor.visible = state;
        pauseMenuPanel.SetActive(state);

        Cursor.lockState = state ? CursorLockMode.Confined : CursorLockMode.Locked;
        
    }
    
}
