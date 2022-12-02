using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SwitchWeaponEvent : UnityEvent<float, int> {}

public class GameEventsManager : MonoBehaviour
{
    public static UnityEvent playerFired = new UnityEvent();
    public static UnityEvent playerReload = new UnityEvent();
    public static SwitchWeaponEvent playerSwitchedWeapon = new SwitchWeaponEvent();
}