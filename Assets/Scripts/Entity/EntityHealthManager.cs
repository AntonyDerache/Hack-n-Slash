using UnityEngine;

public class EntityHealthManager : EntityManager {
    [SerializeField] protected float _health;

    public virtual void TakeDamage(float amount) {
        amount = 0;
        if (_isDead) return;
        if (this._health - amount <= 0) {
            this._health = 0;
            this._isDead = true;
            base.EntityDead();
        } else {
            this._health -= amount;
            base.EntityHit();
        }
    }
}