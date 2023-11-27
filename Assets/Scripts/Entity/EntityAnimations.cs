using UnityEngine;

public class EntityAnimations : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    protected int _isRunningHash;
    protected int _dieHash;
    protected int _hitHash;
    protected int _attackHash;

    virtual protected void Awake() {
        if (_anim) {
            _isRunningHash = Animator.StringToHash("Run");
            _dieHash = Animator.StringToHash("Death");
            _hitHash = Animator.StringToHash("Hit");
            _attackHash = Animator.StringToHash("Attack");
        }
    }

    public void Run(bool state)
    {
        if (IsParameterExists(_isRunningHash)){
            _anim.SetBool(_isRunningHash, state);
        }
    }

    public void Attack()
    {
        if (IsParameterExists(_attackHash)) {
           _anim.SetTrigger(_attackHash);
        }
    }

    public void Hit()
    {
        if (IsParameterExists(_hitHash)) {
            _anim.SetTrigger(_hitHash);
        }
    }

    public void Death()
    {
        if (IsParameterExists(_hitHash)) {
            _anim.SetTrigger(_hitHash);
        }
    }

    protected bool IsParameterExists(int hash)
    {
        bool exists = false;
        foreach (var param in _anim.parameters) {
            if (hash == param.nameHash) {
                exists = true;
                break;
            }
        }
        return exists;
    }
}
