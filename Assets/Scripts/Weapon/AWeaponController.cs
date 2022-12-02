using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AWeaponController : MonoBehaviour
{
    public WeaponsEnum id;
    public int _ammos;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Sprite _muzzle;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private List<Transform> _firePoints = new List<Transform>();
    [SerializeField] private Transform _centerPoint;

    private bool _canFire = true;
    private Coroutine _resetFireRateCoroutine = null;
    private Coroutine _reloadCoroutine = null;
    private SpriteRenderer _sprite;
    private List<SpriteRenderer> _handsSprites = new List<SpriteRenderer>();
    [HideInInspector] public float _loadedAmmos = 0;

    private void Awake()
    {
        _loadedAmmos = _ammos;
        _sprite = transform.Find("Sprite")?.GetComponent<SpriteRenderer>();
        for (int i = 0; i < _sprite.gameObject.transform.childCount; i++) {
            _handsSprites.Add(_sprite.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    private void Update()
    {
        Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (angle > 40 && angle < 140) {
            _sprite.sortingOrder = 0;
            for (int i = 0; i < _handsSprites.Count; i++) {
                _handsSprites[i].sortingOrder = 1;
            }
        } else {
            _sprite.sortingOrder = 3;
            for (int i = 0; i < _handsSprites.Count; i++) {
                _handsSprites[i].sortingOrder = 4;
            }
        }
    }

    virtual public void Fire(Vector3 mousePos)
    {
        if (!_canFire)
            return;
        if (_loadedAmmos <= 0) {
            _reloadCoroutine = StartCoroutine(ReloadWeapon());
            return;
        }
        _canFire = false;
        GameEventsManager.playerFired?.Invoke();
        _resetFireRateCoroutine = StartCoroutine(ResetFireRate());
        _loadedAmmos -= 1;
        for (int i = 0; i < _firePoints.Count; i++) {
            GameObject bullet = Instantiate(_bullet, _firePoints[i].position, Quaternion.identity);
            if (bullet) {
                // TO FIX SPEED OF BULLET
                if (_centerPoint != null) {
                    bullet.GetComponent<BulletController>()?.Active((_firePoints[i].position - _centerPoint.position).normalized, _bulletSpeed);
                } else {
                    bullet.GetComponent<BulletController>()?.Active((Camera.main.ScreenToWorldPoint(mousePos) - _firePoints[i].position).normalized, _bulletSpeed);
                }
            }
        }
    }

    private IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }

    public IEnumerator ReloadWeapon()
    {
        _canFire = false;
        yield return new WaitForSeconds(_reloadTime);
        GameEventsManager.playerReload?.Invoke();
        _loadedAmmos = _ammos;
        _canFire = true;
    }

    virtual public void ShowWeapon(bool state)
    {
        gameObject.SetActive(state);
    }
}
