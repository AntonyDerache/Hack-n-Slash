using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : UnityEvent<int> { }


public class EnemyEventsManager : MonoBehaviour {
    public static EnemyDeath enemyDeath = new EnemyDeath();
}