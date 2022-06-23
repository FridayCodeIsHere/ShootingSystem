using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDemageable
{
    public void ReduceHealth(int value);
    public void Dead();
}
