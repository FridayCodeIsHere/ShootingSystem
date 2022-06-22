using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPool : MonoBehaviour
{
    public static ManagerPool Instance;

    [Serializable] public struct ObjectInfo
    {
        public enum ObjectType
        {
            bullet,
            racket
        }
        public ObjectType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [SerializeField] private List<ObjectInfo> _objectsInfo;

    private Dictionary<ObjectInfo.ObjectType, Pool> _pools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InitPool();
    }

    private void InitPool()
    {
        _pools = new Dictionary<ObjectInfo.ObjectType, Pool>();
        GameObject emptyGO = new GameObject();

        foreach(ObjectInfo obj in _objectsInfo)
        {
            GameObject container = Instantiate(emptyGO, transform, false);
            container.name = obj.Type.ToString();

            _pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                GameObject go = InstantiateObject(obj.Type, container.transform);
                _pools[obj.Type].Objects.Enqueue(go);
            }
        }

        Destroy(emptyGO);
    }

    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        GameObject go = Instantiate(_objectsInfo.Find(x => x.Type == type).Prefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectInfo.ObjectType type)
    {
        GameObject obj;
        if (_pools[type].Objects.Count > 0)
        {
            obj = _pools[type].Objects.Dequeue();
        }
        else
        {
            obj = InstantiateObject(type, _pools[type].Container);
        }
        obj.SetActive(true);
        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        if (obj.TryGetComponent(out IPooledObject poolObject))
        {
            _pools[poolObject.Type].Objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}
