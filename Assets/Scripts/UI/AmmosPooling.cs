using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AmmosPooling : MonoBehaviour
{
    [SerializeField] private GameObject _ammosIconPrefab;

    private int _numberOfBullets = 36;
    private int _bulletsInCurrentWeapon = 36;
    private IObjectPool<GameObject> _ammosPool;
    private List<GameObject> _activeBullet = new List<GameObject>();


    private void Awake() {
        this._ammosPool = new ObjectPool<GameObject>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, null, false, _numberOfBullets, 40);
        InitBullets(this._bulletsInCurrentWeapon);
    }

    private void Start() {
        GameEventsManager.playerFired?.AddListener(hasFired);
        GameEventsManager.playerReload?.AddListener(ReloadBullets);
        GameEventsManager.playerSwitchedWeapon?.AddListener(SwitchWeapon);
    }

    private void SwitchWeapon(float loadedBullets, int maxAmmos)
    {
        int activeBullet = this._activeBullet.Count;

        if (loadedBullets - activeBullet > 0 ) {
            for (int i = 0; i < loadedBullets - activeBullet; i++) {
                this._activeBullet.Add(_ammosPool.Get());
            }
        } else {
            for (int i = 0; i < activeBullet - loadedBullets; i++) {
                _ammosPool.Release(this._activeBullet[0]);
                this._activeBullet.RemoveAt(0);
            }
        }
        this._bulletsInCurrentWeapon = maxAmmos;
    }

    private void hasFired()
    {
        if (this._activeBullet.Count > 0){
            _ammosPool.Release(this._activeBullet[0]);
            this._activeBullet.Remove(this._activeBullet[0]);
        }
    }

    private void InitBullets(int amount)
    {
        for (int i = 0; i < amount; i++) {
            this._activeBullet.Add(_ammosPool.Get());
        }
    }

    private void ReloadBullets()
    {
        foreach (var item in this._activeBullet) {
            _ammosPool.Release(item);
        }
        this._activeBullet.Clear();
        InitBullets(this._bulletsInCurrentWeapon);
    }

    private GameObject CreateBullet() => Instantiate(_ammosIconPrefab, transform);

    private void OnTakeBulletFromPool(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    private void OnReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
