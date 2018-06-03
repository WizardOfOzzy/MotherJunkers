using UnityEngine;

public class StandardMachineGun : Weapon
{
    private float fireRate;
    private float fireTrack;
    private const float offset = .25f;

    protected override void Start()
    {
        base.Start();
        fireRate = 1 / ShotsPerSecond;
        fireTrack = -1f;
    }

    protected override void FireWeapon()
    {
        //base.FireWeapon();
        Publisher.Raise(new WeaponFiredEvent(_playerIndex, this));
        MachineGunPool.Instance.SpawnProjectile(spawnPoint.position + Random.onUnitSphere * offset, spawnPoint.rotation);
        fireTrack = fireRate;
    }

    protected override bool CanFire()
    {
        bool canFire = base.CanFire();
        canFire = canFire && fireTrack <= 0;
        return canFire;
    }

    private void Update()
    {
        if (fireTrack > 0)
        {
            fireTrack -= Time.deltaTime;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
            _audioSource.time = 0;
        }
    }

    public override void OnFireHeld()
    {
        base.OnFireHeld();
        TryFireWeapon();
    }
}
