using UnityEngine;

public class Weapon : Item 
{
    [SerializeField]
    protected AudioSource _audioSource;

    public string Name;

    private const float MIN_AMMO = 0f;

    [SerializeField]
    protected float CurrentAmmo;

    [SerializeField]
    private float MaxAmmo;

    [SerializeField]
    protected float AmmoPerShot;

    [SerializeField]
    protected float ShotsPerSecond;

    public bool HasInfiniteAmmo;

    [SerializeField]
    protected GameObject ProjectilePrefab;

    [SerializeField]
    protected Transform spawnPoint;
    // Returns true if the weapon successfully fires
    public EController _playerIndex = 0;
    public float Max_Ammo
    {
        get { return MaxAmmo; }
    }

    public float Current_Ammo
    {
        get { return CurrentAmmo; }
    }

    protected virtual void Start()
    {
        CurrentAmmo = MaxAmmo;

        Vehicle vehicle = GetComponentInParent<Vehicle>();
        if (vehicle)
        {
            _playerIndex = vehicle._controller;
        }
    }

    public bool TryFireWeapon()
    {
        bool didFire = CanFire(); 

        // Actually fire the weapon
        if (didFire)
        {
            FireWeapon();
        }

        return didFire;
    }

    protected virtual void FireWeapon()
    {
        Publisher.Raise(new WeaponFiredEvent(_playerIndex, this));
    }

    protected virtual bool CanFire()
    {
        bool result = false;
        // Check to see if we have enough ammo to fire
        if (HasInfiniteAmmo)
        {
            result = true;
        }
        else if (CurrentAmmo > MIN_AMMO)
        {
            // Always allow 
            CurrentAmmo = Mathf.Max(MIN_AMMO, CurrentAmmo - AmmoPerShot);
            result = true;
        }
        return result;
    }

    public virtual void OnFirePressed()
    {

    }
    public virtual void OnFireHeld()
    {

    }
    public virtual void OnFireReleased()
    {

    }
}
