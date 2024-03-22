using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class canbar : MonoBehaviour
{
    public Slider slider; // Can �ubu�u i�in Slider bile�eni
    public TMP_Text healthText; // Can de�erini g�stermek i�in Text bile�eni

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health; // Maksimum can� ayarla
        slider.value = health; // Ba�lang��ta mevcut can� maksimum can olarak ayarla
        UpdateHealthText(health); // Can de�erini g�ncelle
    }

    public void SetHealth(float health)
    {
        slider.value = health; // Can� g�ncelle
        UpdateHealthText(health); // Can de�erini g�ncelle
    }

    void UpdateHealthText(float health)
    {
        healthText.text = Mathf.RoundToInt(health).ToString(); // Can de�erini g�ncelle
    }
}
