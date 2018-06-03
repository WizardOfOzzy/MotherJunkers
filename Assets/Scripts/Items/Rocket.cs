using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Weapon 
{
    [SerializeField]
    private float TimeBetweenShots = 0.3f;
    private float _shotTimer;

	protected override void Start()
	{
        base.Start();
        _shotTimer = -1f;
	}

	protected override void FireWeapon()
    {
        if (_shotTimer < 0f)
        {
            base.FireWeapon();
            _audioSource.Play();
            GameObject.Instantiate(ProjectilePrefab, spawnPoint.position, spawnPoint.rotation);
            _shotTimer = TimeBetweenShots;
        }
    }

    void Update()
    {
        if (_shotTimer > 0f)
        {
            _shotTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryFireWeapon();
        }
    }
}
