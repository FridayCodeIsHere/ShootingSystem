using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : BaseWeapon
{
    private static readonly int _shoot = Animator.StringToHash("ShootShotgun");
    public override void Shoot()
    {
        if (CanShoot && CountBullets > 0)
        {
            base.Shoot();
            Bullet firstBullet = ManagerPool.Instance.GetObject(BaseWeaponData).GetComponent<Bullet>();
            Bullet secondBullet = ManagerPool.Instance.GetObject(BaseWeaponData).GetComponent<Bullet>();

            if (!firstBullet || !secondBullet)
            {
                Debug.LogWarning("Objects has not Bullet component!");
                return;
            }

            firstBullet.OnCreate(ShootPosition.position, transform.rotation);
            secondBullet.OnCreate(ShootPosition.position, transform.rotation);
        }
        
    }
}
