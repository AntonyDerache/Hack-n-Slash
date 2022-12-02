using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BulletController : MonoBehaviour
{
    private Rigidbody2D _rb;

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
}
