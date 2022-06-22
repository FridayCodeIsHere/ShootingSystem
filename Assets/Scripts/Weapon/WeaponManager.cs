using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private BaseWeapon[] _weapons;
    private BaseWeapon _previousWeapon;
    private UserControl _userControl;
    public static int WeaponSelected { get; private set; } = 0;

    private void OnEnable()
    {
        _userControl.Enable();
    }

    private void OnDisable()
    {
        _userControl.Disable();
    }

    private void Awake()
    {
        _userControl = new UserControl();
    }

    private void Update()
    {
        float mouseScroll = _userControl.PC.MouseWheel.ReadValue<float>();
        if (mouseScroll != 0f)
        {
            _previousWeapon = _weapons[WeaponSelected];

            if (mouseScroll < -1f) WeaponSelected--;
            else if (mouseScroll > 1f) WeaponSelected++;

            WeaponSelected = (int)Mathf.Repeat(WeaponSelected, _weapons.Length);
            ChangeWeapon();
        }
    }

    private void ChangeWeapon(int index = -1)
    {
        if (index < 0) index = WeaponSelected;
        _previousWeapon?.gameObject.SetActive(false);
        _weapons[index].gameObject.SetActive(true);
    }

}
