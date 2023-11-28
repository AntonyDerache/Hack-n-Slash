using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject[] _hitEffect;
    private Rigidbody2D _rb;
    private ObjectPooling pool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Active(float angle, float speed, ObjectPooling pool)
    {
        this.pool = pool;
        StartCoroutine(BulletEndOfLife());
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (_rb) {
            _rb.velocity = transform.right * (speed * 350) * Time.fixedDeltaTime;
        }
    }

    public void HitEnemy()
    {
        GameObject bulletSplash = Instantiate(_hitEffect[Random.Range(0, 2)], transform.position, Quaternion.identity);
        Destroy(bulletSplash, .20f);
        GameEventsManager.bulletHit?.Invoke(gameObject);
        DeleteOfPoolBullet();
    }

    private IEnumerator BulletEndOfLife() {
        yield return new WaitForSeconds(2f);
        DeleteOfPoolBullet();
    }

    private void DeleteOfPoolBullet() {
        if (pool) {
            if (pool.Release(gameObject)) {
                return;
            }
        }
        Destroy(gameObject);
    }
}
