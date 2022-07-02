using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseWeapon
{
    private static readonly int _shoot = Animator.StringToHash("ShootPistol");
    
    public override void Shoot()
    {
        if (CanShoot && CountBullets > 0)
        {
            base.Shoot();
            BaseAnimator.SetTrigger(_shoot);
            GameObject bullet = ManagerPool.Instance.GetObject(BaseWeaponData);
            if (bullet.TryGetComponent(out Bullet bulletComponent))
            {
                bulletComponent.OnCreate(ShootPosition.position, transform.rotation);
            }
        }
        

        
    }
}
