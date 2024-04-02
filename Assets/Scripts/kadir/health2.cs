using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class health2 : MonoBehaviour
{
    public Image healthBarImage; // Canbarýnýzý buraya sürükleyin
    public TMP_Text healthText;
    public Image gameOverImage; // Oyun sonu ekranýnýzý buraya sürükleyin
    private int health = 100;

    void Start()
    {
        UpdateHealthBar();
        gameOverImage.enabled = false; // Oyun baþladýðýnda oyun sonu ekranýný gizle
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
        float healthPercentage = (float)health / 100;
        healthBarImage.fillAmount = healthPercentage;
        healthText.text = "Can: " + health.ToString();

        // Can deðeri azaldýkça Image bileþeninin boyutunu azalt
        healthBarImage.rectTransform.sizeDelta = new Vector2(healthPercentage * 100, healthBarImage.rectTransform.sizeDelta.y);
    }

    void GameOver()
    {
        // Oyun sonu ekranýný göster
        gameOverImage.enabled = true;
    }
}
