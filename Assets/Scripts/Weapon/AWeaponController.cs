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
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoints;
    [SerializeField] private Transform _centerPoint;

    private float _rotationAngle;
    private bool _cursorOver = false;
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
        // CheckIfMouseHover();
        HandleRotation();
    }

    // private void CheckIfMouseHover()
    // {
    //     Vector3 ray = Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());
    //     RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
    //     if (hit.collider != null) {
    //         if (hit.collider.transform.name == name) {
    //             if (_cursorOver == false) {
    //                 _canFire = false;
    //                 GameEventsManager.IsCursorOverWeapon.Invoke(true);
    //             }
    //             _cursorOver = true;
    //         }
    //     } else {
    //         if (_cursorOver) {
    //             _canFire = true;
    //             GameEventsManager.IsCursorOverWeapon.Invoke(false);
    //         }
    //         _cursorOver = false;
    //     }
    // }

    private void HandleRotation()
    {
        Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        _rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_rotationAngle, Vector3.forward);
        if (_rotationAngle > 40 && _rotationAngle < 140) {
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
        StartCoroutine(ShowMuzzle());
        if (id == WeaponsEnum.Shotgun) {
            GameObject bullet = Instantiate(_bullet, _firePoints.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(_bullet, _firePoints.position, Quaternion.identity);
            GameObject bullet3 = Instantiate(_bullet, _firePoints.position, Quaternion.identity);

            bullet.GetComponent<BulletController>()?.Active(_rotationAngle + 10, _bulletSpeed);
            bullet2.GetComponent<BulletController>()?.Active(_rotationAngle, _bulletSpeed);
            bullet3.GetComponent<BulletController>()?.Active(_rotationAngle - 10, _bulletSpeed);
        } else {
            GameObject bullet = Instantiate(_bullet, _firePoints.position, Quaternion.identity);
            bullet.GetComponent<BulletController>()?.Active(_rotationAngle, _bulletSpeed);
        }
    }

    private IEnumerator ShowMuzzle()
    {
        _muzzle.SetActive(true);
        yield return new WaitForSeconds(.05f);
        _muzzle.SetActive(false);

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
