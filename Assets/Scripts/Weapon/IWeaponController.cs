using UnityEngine;

public interface IWeaponController
{
    WeaponsEnum id { get; }
    float fireRate { get; }
    float reloadTime { get; }
    int ammos { get; }
    Sprite muzzle { get; }
    Sprite bullet { get; }

    public void Fire();
    public void ShowWeapon(bool state);
}
