using UnityEngine;

public class VehicleHealth : MonoBehaviour
{
    public float Health = 0.0f;

    public void TakeDamage(float pDamage)
    {
        Health += pDamage;

        Publisher.Raise(new DamageTakenEvent(GetComponent<Vehicle>()._controller, Health));
    }
}
