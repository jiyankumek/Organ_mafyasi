using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ct : MonoBehaviour
{
    public canbar healthBar; // HealthBar scriptine eriþmek için referans
    private float health = 100f; // Baþlangýçta oyuncunun caný

    void Start()
    {
        // HealthBar'ý baþlatmak için maksimum caný ayarla
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        // Ekrana týklandýðýnda hasar al
        if (Input.GetMouseButtonDown(0)) // Sol týklama için
        {
            TakeDamage(10f);
        }
        else if (Input.GetMouseButtonDown(1)) // Sað týklama için
        {
            TakeDamage(20f);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        // Saðlýk deðerini kontrol et, minimum 0 olmalý
        health = Mathf.Max(0f, health);

        // Saðlýk çubuðunu güncelle
        healthBar.SetHealth(health);

        Debug.Log("Health: " + health); // Saðlýk deðerini konsola yazdýr

        // Burada saðlýk deðerine göre diðer iþlemleri yapabilirsiniz
        // Örneðin, karakterin animasyonunu oynatabilir, ölüm iþlemlerini yapabilirsiniz
    }
}
