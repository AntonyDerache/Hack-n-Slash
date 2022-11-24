using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerAnimations _playerAnimations;
    private PlayerMovements _playerMovements;

    [SerializeField] private PlayerWeaponManager _weaponController;
    private SpriteRenderer _sprite;

    private PlayerInput _playersInput;
    private InputAction _moveAction;
    private InputAction _rollAction;
    private InputAction _fireAction;

    private Vector2 _moveVector;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
        _playerMovements = GetComponent<PlayerMovements>();
        _playersInput = GetComponent<PlayerInput>();
        _sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        InitInputActions();
    }

    private void InitInputActions()
    {
        _moveAction = _playersInput.actions["Move"];
        _rollAction = _playersInput.actions["Roll"];
        _fireAction = _playersInput.actions["Fire"];
    }

    private void Update()
    {
        _moveVector = _moveAction.ReadValue<Vector2>();
        if (_rollAction.triggered) {
            _playerAnimations.Roll();
            // Roll();
        }
        if (_weaponController && _fireAction.triggered) {
            // _playerAnimations.Attack();
            _weaponController.Fire((Vector3)Mouse.current.position.ReadValue());
        }
        CheckRotation();
        SetMovements();
    }

    private void CheckRotation()
    {
        Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (_weaponController) _weaponController.RotateWeapon(angle);
        if (angle > 120 || angle < -70) {
            _sprite.flipX = true;
        } else {
            _sprite.flipX = false;
        }
    }

    private void SetMovements()
    {
        _playerMovements.SetSmoothInput(_moveVector);
        _playerMovements.Move(_moveVector);
    }
}
