using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Color PlayerColor;
    public EController PlayerIndex;

    public HealthUI HealthUI;
    public AmmoUI AmmoUI;
    public StockUI StockUI;

    public void Awake()
    {
        Publisher.Subscribe<WeaponChangedEvent>(OnWeaponChangedEvent);
        Publisher.Subscribe<WeaponFiredEvent>(OnWeaponFiredEvent);
        Publisher.Subscribe<DamageTakenEvent>(OnDamageTakenEvent);

        //AmmoUI.SetMaxAmmoSize(Random.Range(-1, 20));
        //AmmoUI.SetAmmoCount(2);

        PlayerColor = GameStateManager.GetPlayerColor(PlayerIndex);
        AmmoUI.SetColor(PlayerColor);
    }

    private void OnDamageTakenEvent(DamageTakenEvent e)
    {
        if (e.PlayerIndex == PlayerIndex)
        {
            HealthUI.SetHealth((int)e.CurrentHealth);
        }
    }

    private void OnWeaponFiredEvent(WeaponFiredEvent e)
    {
        if (e.PlayerIndex == PlayerIndex)
        {
            AmmoUI.SetAmmoCount(e.Weapon.HasInfiniteAmmo ? -1 : (int)e.Weapon.Current_Ammo);
        }
    }

    private void OnWeaponChangedEvent(WeaponChangedEvent e)
    {
        Debug.Log(e.PlayerIndex + " " + e.Weapon.Name);
        if (e.PlayerIndex == PlayerIndex)
        {
            AmmoUI.SetMaxAmmoSize(e.Weapon.HasInfiniteAmmo ? -1 : (int)e.Weapon.Max_Ammo);
        }
    }
}
