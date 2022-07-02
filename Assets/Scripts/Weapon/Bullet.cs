using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField, Range(0, 0.5f)] private float _randomizeDamage;


    #region MonoBehaviour 
    private void OnValidate()
    {
        if (_lifeTime < 0f) _lifeTime = 0f;
        if (_speed < 0f) _speed = 0f;
        if (_damage < 0) _damage = 0;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDemageable damageCollider))
        {
            float randomDamage = _randomizeDamage * _damage;
            int damage = (int)Random.Range(_damage - randomDamage, _damage + randomDamage);
            damageCollider.ReduceHealth(damage);
            Destroy(this.gameObject);
        }
    }

    

}
