using UnityEngine;

public class Shotgun : AWeaponController
{
    protected override void InitWeaponBullets() {
        GameObject bullet = this._bulletsPool.Get();
        GameObject bullet2 = this._bulletsPool.Get();
        GameObject bullet3 = this._bulletsPool.Get();

        bullet.GetComponent<BulletController>().Active(this._rotationAngle + 10, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
        bullet2.GetComponent<BulletController>().Active(this._rotationAngle, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
        bullet3.GetComponent<BulletController>().Active(this._rotationAngle - 10, this._bulletSpeed, this._bulletDamage, this._bulletsPool);
    }
}
