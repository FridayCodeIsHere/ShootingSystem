using System.Collections.Generic;
using UnityEngine;

public class ManagerPool : MonoBehaviour
{
    public static ManagerPool Instance;
    private Dictionary<IWeapon, Pool> _pools;
    private List<WeaponData> _weaponsData;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetPoolData(BaseWeapon[] weapons)
    {
        _weaponsData = new List<WeaponData>();
        for (int i = 0; i < weapons.Length; i++)
        {
            _weaponsData.Add(weapons[i].BaseWeaponData);
        }
        InitPool();
    }

    private void InitPool()
    {
        _pools = new Dictionary<IWeapon, Pool>();
        GameObject emptyGO = new GameObject();

        foreach(WeaponData weapon in _weaponsData)
        {
            GameObject container = Instantiate(emptyGO, transform, false);
            container.name = weapon.WeaponPrefab.ToString();

            IWeapon currentWeapon = weapon.WeaponPrefab.GetComponent<IWeapon>();
            _pools[currentWeapon] = new Pool(container.transform);

            for (int i = 0; i < weapon.CountBulletsInPool; i++)
            {
                GameObject go = InstantiateObject(weapon, container.transform);
                _pools[currentWeapon].Objects.Enqueue(go);
            }
        }

        Destroy(emptyGO);
    }

    private GameObject InstantiateObject(WeaponData weapon, Transform parent)
    {
        GameObject go = Instantiate(weapon.BulletPrefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(WeaponData weaponData)
    {
        GameObject obj;
        IWeapon weapon = weaponData.WeaponPrefab.GetComponent<IWeapon>();
        if (_pools[weapon].Objects.Count > 0)
        {
            obj = _pools[weapon].Objects.Dequeue();
        }
        else
        {
            obj = InstantiateObject(weaponData, _pools[weapon].Container);
        }
        obj.SetActive(true);
        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        if (obj.GetComponent<IPooledObject>() != null)
        {
            string objParentName = obj.transform.parent.name;
            foreach (var pool in _pools)
            {
                if (objParentName == pool.Value.Container.name)
                {
                    pool.Value.Objects.Enqueue(obj);
                    obj.SetActive(false);
                    return;
                }
            }
            
        }
    }
}
