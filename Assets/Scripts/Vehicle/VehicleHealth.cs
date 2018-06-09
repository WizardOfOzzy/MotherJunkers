using System.Collections;
using UnityEngine;

public class VehicleHealth : MonoBehaviour
{
    public float Health;
    public int Stock = 3;
    public bool Invulnerable;

    private void Start()
    {
        Health = 0.0f;
        Stock = 3;
    }

    public void TakeDamage(float pDamage)
    {
        Health += pDamage;
        Publisher.Raise(new DamageTakenEvent(GetComponent<Vehicle>()._controller, Health));
    }

    public void UpdateStock()
    {
        Stock -= 1;

        PlayerUI[] playerUis = FindObjectsOfType<PlayerUI>();
        foreach (PlayerUI playerUi in playerUis)
        {
            if (playerUi.PlayerIndex == GetComponent<Vehicle>()._controller)
            {
                playerUi.StockUI.SetStockAmmount(Stock);
                break;
            }
        }
    }

    public void SetHealth(int health)
    {
        Health = health;
        PlayerUI[] playerUis = FindObjectsOfType<PlayerUI>();
        foreach (PlayerUI playerUi in playerUis)
        {
            if (playerUi.PlayerIndex == GetComponent<Vehicle>()._controller)
            {
                playerUi.HealthUI.SetHealth(health);
                break;
            }
        }
    }

    public void SetRespawnInvulnerability()
    {
        StartCoroutine(RespawnInvulnerability());
    }

    private IEnumerator RespawnInvulnerability()
    {
        Collider[] colliders = GetComponents<Collider>();

        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
        Vehicle vehicle = GetComponent<Vehicle>();
        Invulnerable = true;
        vehicle.FlashPlayer();
        yield return new WaitForSeconds(1.0f);
        Invulnerable = false;

        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }
    }
}
