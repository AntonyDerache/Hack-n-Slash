using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BulletController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction = new Vector2(0, 0) ;
    private float _speed = 0f;

    private void Awake()
    {
        Destroy(this.gameObject, 2f);
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Active(Vector2 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        if (_rb) {
            _rb.velocity = direction * (_speed * 350) * Time.fixedDeltaTime;
        }
    }
}
