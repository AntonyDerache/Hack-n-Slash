using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : EntityManager
{
    private PlayerMovements _playerMovements;

    [SerializeField] private PlayerWeaponManager _playerWeaponController;
    private SpriteRenderer _sprite;

    private PlayerInput _playersInput;
    private InputAction _moveAction;
    private InputAction _rollAction;
    private InputAction _fireAction;
    private InputAction _switchFirstWeapon;
    private InputAction _switchSecondWeapon;
    private InputAction _switchThirdWeapon;
    private InputAction _reloadWeapon;

    private Vector2 _moveVector;

    protected override void Awake()
    {
        base.Awake();
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
        _switchFirstWeapon = _playersInput.actions["SwitchFirstWeapon"];
        _switchSecondWeapon = _playersInput.actions["SwitchSecondWeapon"];
        _switchThirdWeapon = _playersInput.actions["SwitchThirdWeapon"];
        _reloadWeapon = _playersInput.actions["Reload"];
    }

    private void Update()
    {
        _moveVector = _moveAction.ReadValue<Vector2>();
        CheckFiring();
        CheckRotation();
        CheckSwitchWeapon();
        CheckRolling();
        CheckReload();
    }

    private void FixedUpdate()
    {
        SetMovements();
    }

    private void CheckRotation()
    {
        Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _playerWeaponController.RotateWeaponScale(angle);
        if (angle > 120 || angle < -70) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void CheckSwitchWeapon()
    {
        if (_switchFirstWeapon.triggered) {
            _playerWeaponController?.SwitchWeapon(0);
        } else if (_switchSecondWeapon.triggered) {
            _playerWeaponController?.SwitchWeapon(1);
        } else if (_switchThirdWeapon.triggered) {
            _playerWeaponController?.SwitchWeapon(2);
        }
    }

    private void CheckRolling()
    {
        if (_rollAction.triggered) {
            // _playerAnimations.Roll();
            this._animationController.Attack();
            // Roll();
        }
    }

    private void CheckFiring()
    {
        if (_playerWeaponController && _fireAction.IsPressed()) {
            // _playerAnimations.Attack();
            _playerWeaponController.Fire((Vector3)Mouse.current.position.ReadValue());
        }
    }

    private void CheckReload()
    {

        if (_playerWeaponController && _reloadWeapon.triggered) {
            // _playerAnimations.Attack();
            _playerWeaponController.ReloadWeapon();
        }
    }

    private void SetMovements()
    {
        _playerMovements.SetSmoothInput(_moveVector);
        _playerMovements.Move(_moveVector);
    }


    public WeaponsEnum GetCurrentWeapon()
    {
        return _playerWeaponController.currentWeapon;
    }
}
