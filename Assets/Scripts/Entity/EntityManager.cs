using UnityEngine;

public class EntityManager : MonoBehaviour {
    private EntityAnimations _animationController;
    protected bool _isDead = false;

    private void Awake()
    {
        this._animationController = gameObject.GetComponent<EntityAnimations>();
    }

    public virtual void EntityHit(float damageTaken)
    {
        if (!this._isDead) {
            this._animationController.Hit();
        }
    }

    protected virtual void EntityDead() {
        this._animationController.Death();
        this._isDead = true;
    }
}