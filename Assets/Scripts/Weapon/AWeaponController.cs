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
    [SerializeField] private float _bulletDamage;

    [SerializeField] private ObjectPooling _bulletsPool;
    private List<GameObject> _activeBullet = new List<GameObject>();

    private float _rotationAngle;
    // private bool _cursorOver = false;
    private bool _canFire = true;
    private Coroutine _resetFireRateCoroutine = null;
    private Coroutine _reloadCoroutine = null;
    private SpriteRenderer _sprite;
    private List<SpriteRenderer> _handsSprites = new List<SpriteRenderer>();
    [HideInInspector] public float _loadedAmmos = 0;

    private void Awake()
    {
        this._loadedAmmos = _ammos;
        this._sprite = transform.Find("Sprite")?.GetComponent<SpriteRenderer>();
        for (int i = 0; i < _sprite.gameObject.transform.childCount; i++) {
            this._handsSprites.Add(_sprite.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    private void Start()
    {
        this._bulletsPool.InitObjects();
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
        this._rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(this._rotationAngle, Vector3.forward);
        if (this._rotationAngle > 40 && this._rotationAngle < 140) {
            _sprite.sortingOrder = 0;
            for (int i = 0; i < this._handsSprites.Count; i++) {
                this._handsSprites[i].sortingOrder = 1;
            }
        } else {
            _sprite.sortingOrder = 3;
            for (int i = 0; i < this._handsSprites.Count; i++) {
                this._handsSprites[i].sortingOrder = 4;
            }
        }
    }

    private bool IsAllowToFire() {
        if (!this._canFire)
            return false;
        if (this._loadedAmmos <= 0) {
            this._reloadCoroutine = StartCoroutine(ReloadWeapon());
            return false;
        }
        return true;
    }

    private void InitShotgunBullet()
    {

        GameObject bullet = this._bulletsPool.Get();
        GameObject bullet2 = this._bulletsPool.Get();
        GameObject bullet3 = this._bulletsPool.Get();

        bullet.GetComponent<BulletController>().Active(this._rotationAngle + 10, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
        bullet2.GetComponent<BulletController>().Active(this._rotationAngle, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
        bullet3.GetComponent<BulletController>().Active(this._rotationAngle - 10, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
    }

    private void InitWeaponBullet()
    {
        GameObject bullet = this._bulletsPool.Get();
        bullet.GetComponent<BulletController>().Active(this._rotationAngle, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
    }

    virtual public void Fire(Vector3 mousePos)
    {
        if (!IsAllowToFire()) {
            return;
        }
        this._canFire = false;
        GameEventsManager.playerFired?.Invoke();
        this._resetFireRateCoroutine = StartCoroutine(ResetFireRate());
        this._loadedAmmos -= 1;
        StartCoroutine(ShowMuzzle());
        if (id == WeaponsEnum.Shotgun) {
            InitShotgunBullet();
        } else {
            InitWeaponBullet();
        }
    }

    private IEnumerator ShowMuzzle()
    {
        this._muzzle.SetActive(true);
        yield return new WaitForSeconds(.05f);
        this._muzzle.SetActive(false);

    }

    private IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(this._fireRate);
        SetCanFire(true);
    }

    public IEnumerator ReloadWeapon()
    {
        SetCanFire(false);
        yield return new WaitForSeconds(this._reloadTime);
        GameEventsManager.playerReload?.Invoke();
        this._loadedAmmos = _ammos;
        SetCanFire(true);
    }

    virtual public void ShowWeapon(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetCanFire(bool state)
    {
        this._canFire = state;
    }
}
