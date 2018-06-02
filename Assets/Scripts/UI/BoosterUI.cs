using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoosterUI : MonoBehaviour
{
    public RectTransform panel;
    private Image _image;

    private void Start()
    {
        _image = panel.GetComponent<Image>();
        SetBooster(0.5f);
    }

    private void Update()
    {
        panel.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void SetBooster(float percentage /*0 - 1*/)
    {
        _image.DOFillAmount(percentage, 0.1f);
    }
}
