using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponData _weaponData;
    public WeaponData BaseWeaponData => _weaponData;

    private float _speedRotation;
    private float _timeToDelay;
    private float _reloadTime;

    private UserControl _userControl;
    private Animator _animator;
    private float _offset = 0f;
    private float _currentDelay;
    protected bool CanShoot = true;

    private static readonly int _showWeapon = Animator.StringToHash("Show");
    private static readonly int _hideWeapon = Animator.StringToHash("Hide");

    public Animator BaseAnimator => _animator;
    public Transform ShootPosition { get; private set; }
    public float ReloadTime => _reloadTime;
    public int CountBullets { get; private set; }
    public bool IsReload { get; private set; }

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_speedRotation < 0f) _speedRotation = 0f;
        if (_reloadTime < 0f) _reloadTime = 0f;
        if (_timeToDelay < 0f) _timeToDelay = 0f;
    }
    #endregion

    private void OnEnable()
    {
        _userControl.Enable();
        ShowWeapon();
    }

    private void OnDisable()
    {
        _userControl.Disable();
    }

    private void Awake()
    {
        _userControl = new UserControl();
        _animator = GetComponent<Animator>();
        CountBullets = _weaponData.CountBullets;
    }

    private void Start()
    {
        _speedRotation = _weaponData.SpeedRotation;
        _timeToDelay = _weaponData.DuringToShoot;
        _reloadTime = _weaponData.ReloadTime;

        ShootPosition = transform.GetChild(0);
        _userControl.PC.LeftClick.performed += context => Shoot();
        _currentDelay = _timeToDelay;
    }

    private void Update()
    {
        if (!CanShoot)
        {
            _currentDelay -= Time.deltaTime;
            if (_currentDelay <= 0f)
            {
                CanShoot = true;
                _currentDelay = _timeToDelay;
            }
        }
        Rotation();
    }

    private void Rotation()
    {

        Vector2 mousePosition = _userControl.PC.MousePosition.ReadValue<Vector2>();

        Vector3 differenceDir = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(differenceDir.y, differenceDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(rotationZ + _offset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotation);

    }

    public virtual void Shoot()
    {
        if (CanShoot == true && CountBullets > 0)
        {
            CountBullets--;
            WeaponGUIPanel.Instance.RenderData(_weaponData, CountBullets);
            Debug.Log($"ShootBase: {CanShoot}");
            CanShoot = false;
            
        }
        
    }

    public void ShowWeapon()
    {
        _animator.SetTrigger(_showWeapon);
    }

    public void HideWeapon()
    {
        _animator.SetTrigger(_hideWeapon);
    }
}
