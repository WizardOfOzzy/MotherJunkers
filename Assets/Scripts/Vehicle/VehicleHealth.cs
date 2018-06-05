using UnityEngine;

public class VehicleHealth : MonoBehaviour
{
    public float Health = 0.0f;
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
        FindObjectOfType<PlayerUI>().StockUI.SetStockAmmount(Stock);
    }

}
