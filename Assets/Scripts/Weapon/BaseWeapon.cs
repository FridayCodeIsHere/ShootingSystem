using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private ManagerPool.ObjectInfo.ObjectType _bulletType;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _reloadTime;
    private Transform _shootPosition;
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

    private void Start()
    {
        _shootPosition = transform.GetChild(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        Rotation();
    }

    private void Rotation()
    {
        Vector3 differenceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(differenceDir.y, differenceDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(rotationZ + _offset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotation);
    }

    public virtual void Shoot()
    {
        Debug.Log("Shoot!");
        GameObject bullet = ManagerPool.Instance.GetObject(_bulletType);
        if (bullet.TryGetComponent(out Bullet bulletComponent))
        {
            bulletComponent.OnCreate(_shootPosition.position, transform.rotation);
        }
    }
}
