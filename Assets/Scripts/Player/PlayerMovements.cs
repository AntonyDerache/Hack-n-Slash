using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimations))]
public class PlayerMovements : MonoBehaviour
{
    public float speed = 20;
    [SerializeField] private float _smoothInputSpeed = .1f;

    private Rigidbody2D _rb;
    private PlayerAnimations _playerAnimations;

    private Vector2 _smoothInputVector;
    private Vector2 _smoothInputVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimations = GetComponent<PlayerAnimations>();
    }

    public void SetSmoothInput(Vector2 moveVector)
    {
        _smoothInputVector = Vector2.SmoothDamp(_smoothInputVector, moveVector, ref _smoothInputVelocity, _smoothInputSpeed);
    }

    public void Move(Vector2 moveVector)
    {
        if (moveVector.x != 0 || moveVector.y != 0) {
            _rb.velocity = moveVector * Time.fixedDeltaTime * speed;
            _playerAnimations.Run(true);
        } else {
            _playerAnimations.Run(false);
            _rb.velocity = Vector2.zero;
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }
}
