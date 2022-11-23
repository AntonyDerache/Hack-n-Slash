using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private int _isRunningHash;
    private int _idleHash;
    private int _attackHash;
    private int _rollHash;
    private int _dieHash;
    private int _hitHash;

    private void Awake() {
        if (_anim) {
            _idleHash = Animator.StringToHash("Idle");
            _isRunningHash = Animator.StringToHash("Run");
            _attackHash = Animator.StringToHash("Attack");
            _rollHash = Animator.StringToHash("Roll");
            _dieHash = Animator.StringToHash("Death");
            _hitHash = Animator.StringToHash("Hit");
        }
    }

    public void Run(bool state)
    {
        _anim.SetBool(_isRunningHash, state);
    }

    public void Attack(bool state) {

    }

    public void Hit()
    {
        _anim.SetTrigger(_hitHash);
    }

    public void Death()
    {
        _anim.SetTrigger(_dieHash);
    }

    public void Roll()
    {
        _anim.SetTrigger(_rollHash);
    }
}
