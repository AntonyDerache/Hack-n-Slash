using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerAnimations _playerAnimations;
    private PlayerMovements _playerMovements;

    private PlayerInput _playersInput;
    private InputAction _moveAction;

    private Vector2 _moveVector;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
        _playerMovements = GetComponent<PlayerMovements>();
        _playersInput = GetComponent<PlayerInput>();
        InitInputActions();
    }

    private void InitInputActions()
    {
        _moveAction = _playersInput.actions["Move"];
    }

    private void Update()
    {
        _moveVector = _moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        SetMovements();
    }

    private void SetMovements()
    {
        _playerMovements.SetSmoothInput(_moveVector);
        //  if (_moveVector.x != 0) {
        //     _pa.IsRunning(true);
        // } else {
        //     _pa.IsRunning(false);
        // }
        _playerMovements.Move(_moveVector);
    }
}
