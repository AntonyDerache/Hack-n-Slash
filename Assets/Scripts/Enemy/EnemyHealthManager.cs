using UnityEngine;

public class EnemyHealthManager : EntityHealthManager {
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (this._health == 0) {
            Destroy(gameObject, 1f);
        }
    }
}