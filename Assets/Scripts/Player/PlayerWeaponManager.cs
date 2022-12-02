using System.Collections.Generic;
using UnityEngine;

public enum WeaponsEnum
{
    Shotgun,
    Gun,
    Rifle,
}

public class PlayerWeaponManager : MonoBehaviour
{
    public WeaponsEnum currentWeapon = WeaponsEnum.Shotgun;
    private List<AWeaponController> _weapons = new List<AWeaponController>();
    private AWeaponController _currentWeaponController = null;

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            AWeaponController ctrl = transform.GetChild(i).GetComponent<AWeaponController>();
            if (ctrl != null) {
                if (ctrl.id == currentWeapon) {
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

    public void RotateWeaponScale(float angle)
    {
        if (_currentWeaponController == null) {
            return;
        }
        if (angle > 120 || angle < -70) {
            _currentWeaponController.gameObject.transform.localScale = new Vector3(-1, -1, 1);
        } else {
            _currentWeaponController.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SwitchWeapon(int index)
    {
        if (_weapons[index] == null)
            return;
        _currentWeaponController.ShowWeapon(false);
        _currentWeaponController = _weapons[index];
        _currentWeaponController.ShowWeapon(true);
        currentWeapon = _currentWeaponController.id;
        GameEventsManager.playerSwitchedWeapon?.Invoke(_currentWeaponController._loadedAmmos, _currentWeaponController._ammos);
    }

    public void ReloadWeapon()
    {
        StartCoroutine(_currentWeaponController.ReloadWeapon());
    }
}
