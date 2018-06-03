using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Weapon {
    protected override void FireWeapon()
    {
        base.FireWeapon();
        GameObject.Instantiate(ProjectilePrefab, spawnPoint.position, spawnPoint.rotation);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryFireWeapon();
        }
    }
}
