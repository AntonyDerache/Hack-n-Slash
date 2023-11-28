using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour {
    [SerializeField] private int _quantity = 10;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform _spawnPoints;

    private IObjectPool<GameObject> _objectsPool;
    private List<GameObject> _activeBullet = new List<GameObject>();
    private GameObject _activePooledParentObject;

    private void Awake() {
        this._objectsPool = new ObjectPool<GameObject>(CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyPoolObject, false, _quantity, _quantity);
        this._activePooledParentObject = GameObject.Find("ActivePooledObject");
    }

    private GameObject CreateObject() {
        GameObject obj = Instantiate(_objectPrefab, _spawnPoints.position, Quaternion.identity);
        obj.transform.parent = transform;
        return obj;
    }

    public bool Release(GameObject obj)
    {
        if (_activeBullet.Contains(obj)) {
            obj.transform.position = _spawnPoints.position;
            obj.transform.parent = transform;
            this._objectsPool.Release(obj);
            this._activeBullet.Remove(obj);
            return true;
        }
        return false;
    }

    public GameObject Get()
    {
        GameObject obj = this._objectsPool.Get();
        obj.transform.parent = this._activePooledParentObject.transform;
        this._activeBullet.Add(obj);
        return obj;
    }

    public void InitObjects()
    {
        for (int i = 0; i < _quantity; i++) {
            this._activeBullet.Add(this._objectsPool.Get());
        }
        for (int i = 0; i < _quantity; i++) {
            this._objectsPool.Release(_activeBullet[i]);
        }
        this._activeBullet.Clear();
    }

    private void OnTakeObjectFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }
}