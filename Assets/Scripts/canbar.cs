using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class canbar : MonoBehaviour
{
    public Slider slider; // Can çubuðu için Slider bileþeni
    public TMP_Text healthText; // Can deðerini göstermek için Text bileþeni

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health; // Maksimum caný ayarla
        slider.value = health; // Baþlangýçta mevcut caný maksimum can olarak ayarla
        UpdateHealthText(health); // Can deðerini güncelle
    }

    public void SetHealth(float health)
    {
        slider.value = health; // Caný güncelle
        UpdateHealthText(health); // Can deðerini güncelle
    }

    void UpdateHealthText(float health)
    {
        healthText.text = Mathf.RoundToInt(health).ToString(); // Can deðerini güncelle
    }
}
