using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dayd : MonoBehaviour
{
    public float dayDuration = 86400f; // G�n�n saniye cinsinden s�resi (24 saat x 60 dakika x 60 saniye)
    public Light sun; // G�ne� �����
    public Color dayColor; // G�nd�z rengi
    public Color nightColor; // Gece rengi
    public TMP_Text clockText; // Saat g�stergesi metni

    private int currentSecond; // Mevcut saniye
    private int currentSalise; // Mevcut salise (100 salise 1 saniyeye denk gelir)
    private float timeOfDay; // G�n�n saat cinsinden ge�en zaman�

    void Update()
    {
        UpdateClock();
        UpdateTime();
        UpdateLighting();
    }

    void UpdateClock()
    {
        currentSecond = Mathf.FloorToInt(timeOfDay) % 60; // Saniye hesaplamas�
        currentSalise = Mathf.FloorToInt((timeOfDay - Mathf.Floor(timeOfDay)) * 100); // Salise hesaplamas�

        clockText.text = string.Format("{0:00}:{1:00}", currentSecond, currentSalise);
    }

    void UpdateTime()
    {
        timeOfDay += Time.deltaTime * (24f / dayDuration);
        if (timeOfDay > 24f * 3600f) // Bir g�n� tamamlad�k�a saatleri s�f�rla
        {
            timeOfDay -= 24f * 3600f;
        }
    }

    void UpdateLighting()
    {
        float t = timeOfDay / (24f * 3600f);
        sun.transform.localRotation = Quaternion.Euler(t * 360f - 90, 170, 0); // G�ne�in d�nmesi

        // Arka plan rengini g�ncelle (g�nd�zden geceye ge�erken ge�i� yap)
        RenderSettings.ambientLight = Color.Lerp(dayColor, nightColor, t * t);

        // Hava yava� yava� karars�n, �rne�in 19:00'dan sonra
        if (timeOfDay >= 1f * 3600f)
        {
            sun.intensity = Mathf.Lerp(1f, 0.1f, (timeOfDay - 1f * 3600f) / (5f * 3600f)); // 19:00'dan sonra yava� yava� karanl�kla�ma
        }
        else
        {
            sun.intensity = 1f; // Di�er zamanlarda g�ne�in parlakl��� normal
        }
        if (currentSecond == 23 && currentSalise == 59)
        {
            timeOfDay = 0f;
            sun.intensity = 1f;
        }
    }
}
