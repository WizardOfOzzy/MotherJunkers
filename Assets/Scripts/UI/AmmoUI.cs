using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Transform Panel;
    public GameObject CounterPrefab;

    private int _maxAmmoSize;

    public void SetMaxAmmoSize(int size)
    {
        _maxAmmoSize = size;

        for (int i = Panel.childCount - 1; i >= 0; i--)
        {
            Destroy(Panel.GetChild(i).gameObject);
        }

        if (size == -1)
        {
            CreateCounter();
            return;
        }
       
        for (int i = 0; i < _maxAmmoSize; i++)
        {
            CreateCounter();
        }
    }

    private void CreateCounter()
    {
        GameObject go = Instantiate(CounterPrefab);
        go.transform.SetParent(Panel);
        go.transform.localScale = Vector3.one;
    }

    public void SetAmmoCount(int count)
    {
        for (int i = 0; i < Panel.childCount; i++)
        {
            Image image = Panel.GetChild(i).GetComponent<Image>();
            Color color = image.color;
            color.a = i < count ? 1.0f : 0.0f;
            image.color = color;
        }
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < Panel.childCount; i++)
        {
            Image image = Panel.GetChild(i).GetComponent<Image>();
            image.color = color;
        }
    }
}
