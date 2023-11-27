using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject[] _hitEffect;
    private Rigidbody2D _rb;

    private int _lastRandom = 0;

    private void Awake()
    {
        Destroy(this.gameObject, 2f);
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Active(float angle, float speed)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (_rb) {
            _rb.velocity = transform.right * (speed * 350) * Time.fixedDeltaTime;
        }
    }

    public void HitEnemy()
    {
        GameObject bulletSplash = Instantiate(_hitEffect[Random.Range(0, 2)], transform.position, Quaternion.identity);
        Destroy(bulletSplash, .20f);
        Destroy(gameObject);
    }
}
