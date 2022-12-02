using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AmmosPooling : MonoBehaviour
{
    private int _numberOfBullets = 36;
    private int _bulletsInCurrentWeapon = 36;
    private IObjectPool<GameObject> _ammosPool;
    private List<GameObject> _activeBullet = new List<GameObject>();
    [SerializeField] private GameObject _ammosIconPrefab;


    private void Awake() {
        _ammosPool = new ObjectPool<GameObject>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, null, false, _numberOfBullets, 40);
        InitBullets(_bulletsInCurrentWeapon);
    }

    private void Start() {
        GameEventsManager.playerFired?.AddListener(hasFired);
        GameEventsManager.playerReload?.AddListener(ReloadBullets);
        GameEventsManager.playerSwitchedWeapon.AddListener(SwitchWeapon);
    }

    private void SwitchWeapon(float loadedBullets, int maxAmmos)
    {
        int activeBullet = _activeBullet.Count;

        if (loadedBullets - activeBullet > 0 ) {
            for (int i = 0; i < loadedBullets - activeBullet; i++) {
                _activeBullet.Add(_ammosPool.Get());
            }
        } else {
            for (int i = 0; i < activeBullet - loadedBullets; i++) {
                _ammosPool.Release(_activeBullet[0]);
                _activeBullet.RemoveAt(0);
            }
        }
        _bulletsInCurrentWeapon = maxAmmos;
    }

    private void hasFired()
    {
        if (_activeBullet.Count > 0){
            _ammosPool.Release(_activeBullet[0]);
            _activeBullet.Remove(_activeBullet[0]);
        }
    }

    private void InitBullets(int amount)
    {
        for (int i = 0; i < amount; i++) {
            _activeBullet.Add(_ammosPool.Get());
        }
    }

    private void ReloadBullets()
    {
        foreach (var item in _activeBullet) {
            _ammosPool.Release(item);
        }
        _activeBullet.Clear();
        InitBullets(_bulletsInCurrentWeapon);
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
