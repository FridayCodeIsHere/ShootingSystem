using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    ManagerPool.ObjectInfo.ObjectType Type { get; }
}
