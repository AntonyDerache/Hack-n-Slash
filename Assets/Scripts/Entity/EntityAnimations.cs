using UnityEngine;

public class EntityAnimations : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    protected int _isRunningHash;
    protected int _deathHash;
    protected int _hitHash;
    protected int _attackHash;

    virtual protected void Awake() {
        if (_anim) {
            _isRunningHash = Animator.StringToHash("Run");
            _deathHash = Animator.StringToHash("Death");
            _hitHash = Animator.StringToHash("Hit");
            _attackHash = Animator.StringToHash("Attack");
        }
    }

    public void Run(bool state)
    {
        if (IsHashValid(_isRunningHash)){
            _anim.SetBool(_isRunningHash, state);
        }
    }

    public void Attack()
    {
        if (IsHashValid(_attackHash)) {
           _anim.SetTrigger(_attackHash);
        }
    }

    public void Hit()
    {
        if (IsHashValid(_hitHash)) {
            _anim.SetTrigger(_hitHash);
        }
    }

    public void Death()
    {
        if (IsHashValid(_deathHash)) {
            _anim.SetTrigger(_deathHash);
        }
    }

    protected bool IsHashValid(int hash)
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
