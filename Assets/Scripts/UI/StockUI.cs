using UnityEngine;
using UnityEngine.UI;

public class StockUI : MonoBehaviour
{
    public Transform Panel;

    public void SetStockAmmount(int ammount)
    {
        for (int i = 0; i < Panel.childCount; i++)
        {
            Image image = Panel.GetChild(i).GetComponent<Image>();
            Color color = image.color;
            color.a = i <= (ammount - 1) ? 1.0f : 0.0f;

            image.color = color;
        }
    }
}
