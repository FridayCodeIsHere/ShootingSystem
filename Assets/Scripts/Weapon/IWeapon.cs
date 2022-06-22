using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public float ReloadTime { get; }
    public bool IsReload { get; }
    public void Shoot();

}
