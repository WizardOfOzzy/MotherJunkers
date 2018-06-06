using UnityEngine;

public class VehicleHealth : MonoBehaviour
{
    public float Health;
    public int Stock = 3;

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


}
