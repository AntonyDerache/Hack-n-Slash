using UnityEngine;

public class Gun : MonoBehaviour, IWeaponController
{
    [SerializeField] private WeaponsEnum _id;
    public WeaponsEnum id { get => _id; }
    [SerializeField] private float _fireRate;
    public float fireRate { get => _fireRate; }
    [SerializeField] private float _reloadTime;
    public float reloadTime { get => _reloadTime; }
    [SerializeField] private int _ammos;
    public int ammos { get => _ammos; }
    [SerializeField] private Sprite _muzzle;
    public Sprite muzzle { get => _muzzle; }
    [SerializeField] private Sprite _ball;
    public Sprite bullet { get => _ball; }

    public void Fire()
    {
        Debug.Log("fire");
    }

    public void ShowWeapon(bool state)
    {
        gameObject.SetActive(state);
    }
}
