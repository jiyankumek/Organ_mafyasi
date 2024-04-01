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
    private int health = 100;

    void Start()
    {
        UpdateHealthBar();
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
        // Oyunu tamamen durdur
        EditorApplication.isPlaying = false;
    }
}
