using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    public SpriteRenderer _sprite;

    private void Awake() {
        _sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Debug.Log(angle);
        // if (angle > 90 || angle < -90) {
        //     _sprite.flipY = true;
        // } else {
        //     _sprite.flipY = false;
        // }
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void RotateWeapon(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (angle > 120 || angle < -70){
            _sprite.flipY = true;
        } else {
            _sprite.flipY = false;
        }
    }

    public void Fire()
    {
        
    }
}
