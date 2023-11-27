using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EntityAnimations _animationController;

    private void Awake() {
        _animationController = gameObject.GetComponent<EntityAnimations>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bullet")) {
            BulletController bulletCtrl = other.gameObject.GetComponent<BulletController>();
            bulletCtrl?.HitEnemy();

            _animationController.Hit();
        }
    }
}