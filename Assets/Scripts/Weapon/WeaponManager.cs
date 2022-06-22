using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private BaseWeapon[] _weapons;
    private BaseWeapon _previousWeapon;
    public static int WeaponSelected { get; private set; } = 0;


    private void OnMouseUp()
    {
        Debug.Log("MouseUp");
        _previousWeapon = _weapons[WeaponSelected];
        WeaponSelected++;
        WeaponSelected = (int)Mathf.Repeat(WeaponSelected, _weapons.Length);
        ChangeWeapon();
    }

    private void Update()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel > 0.1)
        {
            Debug.Log("MouseUp");
            _previousWeapon = _weapons[WeaponSelected];
            WeaponSelected++;
            WeaponSelected = (int)Mathf.Repeat(WeaponSelected, _weapons.Length);
            ChangeWeapon();
        }
        if (mouseWheel < -0.1)
        {
            Debug.Log("MouseDown");
            _previousWeapon = _weapons[WeaponSelected];
            WeaponSelected--;
            WeaponSelected = (int)Mathf.Repeat(WeaponSelected, _weapons.Length);
            ChangeWeapon();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
        _previousWeapon = _weapons[WeaponSelected];
        WeaponSelected--;
        WeaponSelected = (int)Mathf.Repeat(WeaponSelected, _weapons.Length);
        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        _previousWeapon?.gameObject.SetActive(false);
        _weapons[WeaponSelected].gameObject.SetActive(true);
    }

}
