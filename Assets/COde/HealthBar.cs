using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image bar;

    public void UpdateHealth(int health, int maxHealth)
    {
        if (healthText != null)
            healthText.text = $"{health} / {maxHealth}";
        if (bar != null)
            bar.fillAmount = (float)health / maxHealth;
    }

    public void UpdateBar(int value, int maxValue, string text)
    {
        if (healthText != null)
            healthText.text = text;
        if (bar != null)
            bar.fillAmount = (float)value / maxValue;
    }
}
