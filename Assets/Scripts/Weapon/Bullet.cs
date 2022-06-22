using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private ManagerPool.ObjectInfo.ObjectType _type;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    public ManagerPool.ObjectInfo.ObjectType Type => _type;

    #region MonoBehaviour 
    private void OnValidate()
    {
        if (_lifeTime < 0f) _lifeTime = 0f;
        if (_speed < 0f) _speed = 0f;
    }
    #endregion

    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        StartCoroutine(LifeCoroutine());
    }

    private IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        ManagerPool.Instance.DestroyObject(this.gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }

}
