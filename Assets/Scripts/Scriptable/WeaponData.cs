using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "Create/Data/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private float _duringShoot;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _speedRotation;
    [SerializeField] private int _countBulletsPool;

    [Header("Bullet Settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField, Range(1, 18)] private int _countBullets = 1;

    [Header("UI Settings")]
    [SerializeField] private Sprite _weaponImage;
    [SerializeField] private Sprite _fullBullet;
    [SerializeField] private Sprite _emptyBullet;
    [SerializeField] private Sprite _lastBullet;

    public GameObject WeaponPrefab => _weaponPrefab;
    public GameObject BulletPrefab => _bulletPrefab;
    public float DuringToShoot => _duringShoot;
    public float ReloadTime => _reloadTime;
    public float SpeedRotation => _speedRotation;
    public int CountBulletsInPool => _countBulletsPool;
    public int CountBullets => _countBullets;
    public Sprite WeaponImage => _weaponImage;
    public Sprite FullBullet => _fullBullet;
    public Sprite EmptyBullet => _emptyBullet;
    public Sprite LastBullet => _lastBullet;

    #region MonoBehaviour 
    private void OnValidate()
    {
        if (_weaponPrefab.GetComponent<BaseWeapon>() == null)
        {
            Debug.LogWarning("Field supports only objects with IWeapon inheritance");
            _weaponPrefab = null;
        }
        if (_bulletPrefab.GetComponent<Bullet>() == null)
        {
            Debug.LogWarning("Field supports only objects with Bullet Component");
            _bulletPrefab = null;
        }
        if (_duringShoot < 0f) _duringShoot = 0f;
        if (_reloadTime < 0f) _reloadTime = 0f;
        if (_countBulletsPool < 0) _countBulletsPool = 0;
        
    }
    #endregion

}
