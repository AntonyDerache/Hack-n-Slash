using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : MonoBehaviour
{
    public static UnityEvent playerFired = new UnityEvent();
    public static UnityEvent playerReload = new UnityEvent();
}