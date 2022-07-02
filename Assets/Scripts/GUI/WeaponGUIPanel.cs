using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WeaponGUIPanel : MonoBehaviour
{
    [SerializeField] private Image _weaponImage;
    [SerializeField] private GameObject _weaponBulletsContainer;
    [SerializeField] private GameObject _bulletPrefabUI;
    private int _startCountItems = 10;
    private List<BulletItemGUI> _bulletItems;
    public static WeaponGUIPanel Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Initialization();
    }

    private void Initialization()
    {
        _bulletItems = new List<BulletItemGUI>();
        for (int i = 0; i < _startCountItems; i++)
        {
            CreateDefaultObject(false);
        }
    }

    public void RenderData(WeaponData data, int countBullets)
    {
        _weaponImage.sprite = data.WeaponImage;
        while (_bulletItems.Count < data.CountBullets)
        {
            CreateDefaultObject(false);
        }

        _bulletItems.ForEach(x => x.gameObject.SetActive(false));

        for (int i = 0; i < data.CountBullets; i++)
        {
            _bulletItems[i].gameObject.SetActive(true);
            
            if (i < countBullets)
            {
                if (countBullets <= 3)
                    _bulletItems[i].SetData(data.LastBullet);
                else
                    _bulletItems[i].SetData(data.FullBullet);
            }
            else
            {
                _bulletItems[i].SetData(data.EmptyBullet);
            }
        }

    }

    public void CreateDefaultObject(bool isActive = false)
    {
        GameObject defaultObject = Instantiate(_bulletPrefabUI, _weaponBulletsContainer.transform);
        defaultObject.SetActive(isActive);

        if (defaultObject.TryGetComponent(out BulletItemGUI bullet))
        {
            _bulletItems.Add(bullet);
        }
        else
        {
            Debug.LogError("Item has not BulletItemGUI component");
        }
        

    }

}
