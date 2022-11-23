using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 20;
    [SerializeField] private float _smoothInputSpeed = .1f;

    private CharacterController _cc;
    private PlayerAnimations _playerAnimations;

    private Vector2 _smoothInputVector;
    private Vector2 _smoothInputVelocity;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _playerAnimations = GetComponent<PlayerAnimations>();
    }

    public void SetSmoothInput(Vector2 moveVector)
    {
        _smoothInputVector = Vector2.SmoothDamp(_smoothInputVector, moveVector, ref _smoothInputVelocity, _smoothInputSpeed);
    }

    public void Move(Vector2 moveVector)
    {
        if (moveVector.x != 0 || moveVector.y != 0) {
            _cc.Move(_smoothInputVector * Time.fixedDeltaTime * speed);
            _playerAnimations.Run(true);
        } else {
            _playerAnimations.Run(false);
            _cc.Move(Vector2.zero);
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        _cc.Move(velocity);
    }
}
