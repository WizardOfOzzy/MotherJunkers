using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text Health;
    public Text PlayerName;

    private int _health;
    private const int MaxHealth = 200;

    private const float UpdateSpeed = 0.01f;
    private const float ShakeDuration = 0.1f;
    private readonly Vector3 ShakeStrength = new Vector3(10, 10, 0);
    private const int ShakeVibration = 50;

    private readonly Gradient _gradient = new Gradient { mode = GradientMode.Blend };

    private Vector3 healthPosition;

    private void Start()
    {
        GradientColorKey[] colorKeys = new GradientColorKey[4];
        colorKeys[0] = new GradientColorKey(Color.white, 0.0f);
        colorKeys[1] = new GradientColorKey(Color.yellow, 0.33f);
        colorKeys[2] = new GradientColorKey(new Color(1, 127 / 255.0f, 80 / 255.0f), 0.66f);
        colorKeys[3] = new GradientColorKey(Color.red, 1.0f);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i] = new GradientAlphaKey(1.0f, 0.0f);
        }

        _gradient.SetKeys(colorKeys, alphaKeys);

        healthPosition = Health.transform.localPosition;
    }

    public void SetHealth(int health)
    {
        _health = health;

        SetColor();
        //Shake();
        Health.DOText(_health.ToString(), UpdateSpeed, false, ScrambleMode.Numerals);
    }

    private void SetColor()
    {
        Color c = _gradient.Evaluate(_health / 200.0f);
        Health.DOColor(c, UpdateSpeed);
    }

    private void Shake()
    {
        Health.transform.DOKill();

        Health.transform.DOShakePosition(ShakeDuration, ShakeStrength, ShakeVibration).OnComplete(() =>
            {
                Health.rectTransform.localPosition = healthPosition;
            }).OnKill(() =>
        {
            Health.rectTransform.localPosition = healthPosition;
        });
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            _health += 25;
            _health %= MaxHealth;

            SetHealth(_health);
        }
    }
}
