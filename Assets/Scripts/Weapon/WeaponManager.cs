using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public delegate void WeaponChange();
    public static WeaponChange OnChangeWeapon;

    [SerializeField] private BaseWeapon[] _weapons;
    private BaseWeapon _previousWeapon;
    private UserControl _userControl;
    private bool _canSelect = true;
    public static WeaponManager Instance;
    public BaseWeapon[] Weapons => _weapons;
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
        if (Instance == null)
        {
            Instance = this;
        }
        _userControl = new UserControl();
        
    }

    private void Start()
    {
        WeaponData data = _weapons[WeaponSelected].BaseWeaponData;
        int countBullets = _weapons[WeaponSelected].CountBullets;
        WeaponGUIPanel.Instance.RenderData(data, countBullets);
        ManagerPool.Instance.SetPoolData(_weapons);
    }

    private void Update()
    {
        float mouseScroll = _userControl.PC.MouseWheel.ReadValue<float>();
        if (mouseScroll != 0f && _canSelect)
        {
            _weapons[WeaponSelected].HideWeapon();
            _previousWeapon = _weapons[WeaponSelected];

            if (mouseScroll < -1f) WeaponSelected--;
            else if (mouseScroll > 1f) WeaponSelected++;

            WeaponSelected = (int)Mathf.Repeat(WeaponSelected, _weapons.Length);
            _canSelect = false;

            WeaponData dataWeapon = _weapons[WeaponSelected].BaseWeaponData;
            WeaponGUIPanel.Instance.RenderData(dataWeapon, _weapons[WeaponSelected].CountBullets);

        }
    }

    public void ChangeWeapon(int index = -1)
    {
        _canSelect = true;
        if (index < 0) index = WeaponSelected;
        _previousWeapon?.gameObject.SetActive(false);
        _weapons[index].gameObject.SetActive(true);
    }

}
