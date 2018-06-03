using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Color PlayerColor;
    public int PlayerIndex;

    public HealthUI HealthUI;
    public AmmoUI AmmoUI;
    public StockUI StockUI;

    public void Start()
    {
        Publisher.Subscribe<WeaponChangedEvent>(OnWeaponChangedEvent);
        Publisher.Subscribe<WeaponFiredEvent>(OnWeaponFiredEvent);

        //AmmoUI.SetMaxAmmoSize(Random.Range(-1, 20));
        //AmmoUI.SetAmmoCount(2);
        AmmoUI.SetColor(PlayerColor);
    }

    private void OnWeaponFiredEvent(WeaponFiredEvent e)
    {
        if (e.PlayerIndex == PlayerIndex)
        {
            AmmoUI.SetAmmoCount((int)e.Weapon.Current_Ammo);
        }
    }

    private void OnWeaponChangedEvent(WeaponChangedEvent e)
    {
        if (e.PlayerIndex == PlayerIndex)
        {
            AmmoUI.SetMaxAmmoSize(e.Weapon.HasInfiniteAmmo ? -1 : (int)e.Weapon.Max_Ammo);
        }
    }
}
