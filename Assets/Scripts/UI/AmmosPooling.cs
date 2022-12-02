using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AmmosPooling : MonoBehaviour
{
    private int _numberOfBullets = 10;
    private IObjectPool<GameObject> _ammosPool;
    private List<GameObject> _activeBullet = new List<GameObject>();
    [SerializeField] private GameObject _ammosIconPrefab;

    private void Awake() {
        _ammosPool = new ObjectPool<GameObject>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, null, false, _numberOfBullets, 40);
        InitBullets();
    }

    private void Start() {
        GameEventsManager.playerFired?.AddListener(hasFired);
        GameEventsManager.playerReload?.AddListener(ResetBullets);
    }

    private void hasFired()
    {
        if (_activeBullet.Count > 0){
            _ammosPool.Release(_activeBullet[0]);
            _activeBullet.Remove(_activeBullet[0]);
        }
    }

    private void InitBullets()
    {
        for (int i = 0; i < _numberOfBullets; i++) {
            _activeBullet.Add(_ammosPool.Get());
        }
    }

    private void ResetBullets()
    {
        foreach (var item in _activeBullet) {
            _ammosPool.Release(item);
        }
        _activeBullet.Clear();
        InitBullets();
    }

    private GameObject CreateBullet() => Instantiate(_ammosIconPrefab, transform);

    private void OnTakeBulletFromPool(GameObject bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
