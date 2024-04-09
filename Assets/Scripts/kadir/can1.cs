using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class can1 : MonoBehaviour
{
    public Image healthBar;
    public TMP_Text healthText;
    public Image gameOverImage; // Oyun sonu image'ý
    private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        gameOverImage.enabled = false; // Oyun baþladýðýnda image'ý gizle
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentHealth > 0)
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            // Can deðeri 0'a düþtüðünde image'ý göster ve can texti ile can barýný gizle
            gameOverImage.enabled = true;
            healthBar.enabled = false;
            healthText.enabled = false;
        }
    }
}
