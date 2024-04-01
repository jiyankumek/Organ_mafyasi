using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class health2 : MonoBehaviour
{
    public Image healthBar;
    public TMP_Text healthText;
    public Image gameOverImage; // Oyun sonu ekran�n�z� buraya s�r�kleyin
    private int health = 100;

    void Start()
    {
        UpdateHealthBar();
        gameOverImage.enabled = false; // Oyun ba�lad���nda oyun sonu ekran�n� gizle
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DecreaseHealth(10);
        }
    }

    void DecreaseHealth(int amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0);
        UpdateHealthBar();

        if (health == 0)
        {
            GameOver();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)health / 100;
        healthText.text = "Can: " + health.ToString();
    }

    void GameOver()
    {
        // Oyun sonu ekran�n� g�ster
        gameOverImage.enabled = true;
    }
}
