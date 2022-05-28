using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{

     private TempContoller gunController;
     [SerializeField] private float verticalRecoil;
     [SerializeField] private float recoilTime;
     private float time;

    void Start()
    {
        gunController = GetComponent<TempContoller>();
    }


    void Update()
    {

    }

    public void GenerateRecoil()
    {
        time = recoilTime;
    }
}
