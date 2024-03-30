using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dayd : MonoBehaviour
{
    public float dayDuration = 86400f; // Günün saniye cinsinden süresi (24 saat x 60 dakika x 60 saniye)
    public Light sun; // Güneþ ýþýðý
    public Color dayColor; // Gündüz rengi
    public Color nightColor; // Gece rengi
    public TMP_Text clockText; // Saat göstergesi metni

    private int currentSecond; // Mevcut saniye
    private int currentSalise; // Mevcut salise (100 salise 1 saniyeye denk gelir)
    private float timeOfDay; // Günün saat cinsinden geçen zamaný

    void Update()
    {
        UpdateClock();
        UpdateTime();
        UpdateLighting();
    }

    void UpdateClock()
    {
        currentSecond = Mathf.FloorToInt(timeOfDay) % 60; // Saniye hesaplamasý
        currentSalise = Mathf.FloorToInt((timeOfDay - Mathf.Floor(timeOfDay)) * 100); // Salise hesaplamasý

        clockText.text = string.Format("{0:00}:{1:00}", currentSecond, currentSalise);
    }

    void UpdateTime()
    {
        timeOfDay += Time.deltaTime * (24f / dayDuration);
        if (timeOfDay > 24f * 3600f) // Bir günü tamamladýkça saatleri sýfýrla
        {
            timeOfDay -= 24f * 3600f;
        }
    }

    void UpdateLighting()
    {
        float t = timeOfDay / (24f * 3600f);
        sun.transform.localRotation = Quaternion.Euler(t * 360f - 90, 170, 0); // Güneþin dönmesi

        // Arka plan rengini güncelle (gündüzden geceye geçerken geçiþ yap)
        RenderSettings.ambientLight = Color.Lerp(dayColor, nightColor, t * t);

        // Hava yavaþ yavaþ kararsýn, örneðin 19:00'dan sonra
        if (timeOfDay >= 1f * 3600f)
        {
            sun.intensity = Mathf.Lerp(1f, 0.1f, (timeOfDay - 1f * 3600f) / (5f * 3600f)); // 19:00'dan sonra yavaþ yavaþ karanlýklaþma
        }
        else
        {
            sun.intensity = 1f; // Diðer zamanlarda güneþin parlaklýðý normal
        }
        if (currentSecond == 23 && currentSalise == 59)
        {
            timeOfDay = 0f;
            sun.intensity = 1f;
        }
    }
}
