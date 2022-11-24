using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponsEnum
{
    Shotgun,
    Gun,
    Rifle,
}

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponsEnum _currentWeapon;
    private List<AWeaponController> _weapons = new List<AWeaponController>();
    private AWeaponController _currentWeaponController = null;

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            AWeaponController ctrl = transform.GetChild(i).GetComponent<AWeaponController>();
            if (ctrl != null) {
                if (ctrl.id == _currentWeapon) {
                    _currentWeaponController = ctrl;
                    ctrl.ShowWeapon(true);
                }
                _weapons.Add(ctrl);
            }
        }
    }

    public void Fire(Vector3 mousePos)
    {
        if (_currentWeaponController != null) {
            _currentWeaponController.Fire(mousePos);
        }
    }
}
