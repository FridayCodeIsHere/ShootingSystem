using UnityEngine;
using UnityEngine.InputSystem;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private ManagerPool.ObjectInfo.ObjectType _bulletType;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _reloadTime;
    private Transform _shootPosition;
    private UserControl _userControl;
    private float _offset = 0f;

    public float ReloadTime => _reloadTime;
    public bool IsReload { get; private set; }

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_speedRotation < 0f) _speedRotation = 0f;
        if (_reloadTime < 0f) _reloadTime = 0f;
    }
    #endregion

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

    private void Start()
    {
        _shootPosition = transform.GetChild(0);
        _userControl.PC.LeftClick.performed += context => Shoot();
    }

    private void Update()
    {

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
        GameObject bullet = ManagerPool.Instance.GetObject(_bulletType);
        if (bullet.TryGetComponent(out Bullet bulletComponent))
        {
            bulletComponent.OnCreate(_shootPosition.position, transform.rotation);
        }
    }
}
