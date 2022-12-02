using UnityEngine;

public class PlayerAnimations : EntityAnimations
{
    private int _rollHash;

    protected override void Awake() {
        base.Awake();
        if (_anim) {
            _rollHash = Animator.StringToHash("Roll");
        }
    }

    public void Roll()
    {
        if (IsParameterExists(_rollHash)) {
            _anim.SetTrigger(_rollHash);
        }
    }
}
