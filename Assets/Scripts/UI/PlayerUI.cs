using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public EController PlayerIndex;

    public HealthUI HealthUI;
    public AmmoUI AmmoUI;
    public StockUI StockUI;

    public void Awake()
    {
        Publisher.Subscribe<WeaponChangedEvent>(OnWeaponChangedEvent);
        Publisher.Subscribe<WeaponFiredEvent>(OnWeaponFiredEvent);
        Publisher.Subscribe<DamageTakenEvent>(OnDamageTakenEvent);

        AmmoUI.SetColor(PlayerManager.GetPlayerColor(PlayerIndex));
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
        if (e.PlayerIndex == PlayerIndex)
        {
            AmmoUI.SetMaxAmmoSize(e.Weapon.HasInfiniteAmmo ? -1 : (int)e.Weapon.Max_Ammo);
            AmmoUI.SetColor(PlayerManager.GetPlayerColor(PlayerIndex));
        }
    }

    private void OnEnable()
    {
        bool isEnable = false;
        PlayerManager _playerManager = FindObjectOfType<PlayerManager>();
        if (_playerManager)
        {
            for (int i = 0; i < PlayerManager.Players.Count; i++)
            {
                if ((int)PlayerManager.Players[i].controller == (int) PlayerIndex)
                {
                    isEnable = true;
                    break;
                }
            }
        }

        gameObject.SetActive(isEnable);
    }
}
