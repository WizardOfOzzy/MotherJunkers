using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Color PlayerColor;

    public HealthUI HealthUI;
    public AmmoUI AmmoUI;
    public StockUI StockUI;

    public void Start()
    {
        AmmoUI.SetMaxAmmoSize(Random.Range(-1, 20));
        //AmmoUI.SetAmmoCount(2);
        AmmoUI.SetColor(PlayerColor);
    }
}
