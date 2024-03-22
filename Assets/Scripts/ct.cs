using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ct : MonoBehaviour
{
    public canbar healthBar; // HealthBar scriptine eri�mek i�in referans
    private float health = 100f; // Ba�lang��ta oyuncunun can�

    void Start()
    {
        // HealthBar'� ba�latmak i�in maksimum can� ayarla
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        // Ekrana t�kland���nda hasar al
        if (Input.GetMouseButtonDown(0)) // Sol t�klama i�in
        {
            TakeDamage(10f);
        }
        else if (Input.GetMouseButtonDown(1)) // Sa� t�klama i�in
        {
            TakeDamage(20f);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        // Sa�l�k de�erini kontrol et, minimum 0 olmal�
        health = Mathf.Max(0f, health);

        // Sa�l�k �ubu�unu g�ncelle
        healthBar.SetHealth(health);

        Debug.Log("Health: " + health); // Sa�l�k de�erini konsola yazd�r

        // Burada sa�l�k de�erine g�re di�er i�lemleri yapabilirsiniz
        // �rne�in, karakterin animasyonunu oynatabilir, �l�m i�lemlerini yapabilirsiniz
    }
}
