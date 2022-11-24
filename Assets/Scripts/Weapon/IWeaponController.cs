using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AWeaponController : MonoBehaviour
{
    public WeaponsEnum id;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected int _ammos;
    [SerializeField] protected Sprite _muzzle;
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected List<Transform> _firePoints = new List<Transform>();
    [SerializeField] protected Transform _centerPoint;

    virtual public void Fire(Vector3 mousePos)
    {
        for (int i = 0; i < _firePoints.Count; i++) {
            GameObject bullet = Instantiate(_bullet, _firePoints[i].position, Quaternion.identity);
            if (bullet) {
                if (_centerPoint != null) {
                    bullet.GetComponent<BulletController>()?.Active((_firePoints[i].position - _centerPoint.position).normalized, _bulletSpeed);
                } else {
                    bullet.GetComponent<BulletController>()?.Active((Camera.main.ScreenToWorldPoint(mousePos) - _firePoints[i].position).normalized, _bulletSpeed);
                }
            }
        }
    }

    virtual public void ShowWeapon(bool state)
    {
        gameObject.SetActive(state);
    }

    private void Update()
    {
        Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (angle > 120 || angle < -70){
            transform.localScale = new Vector2(1f, -1f);
        } else {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
