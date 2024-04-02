using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gun : MonoBehaviour
{
    public Light sun; // Güneþ ýþýðý
    public TMP_Text clockText; // Saat göstergesi metni
    public float dayDuration = 60f; // Bir günün süresi (saniye cinsinden)

    private float timer = 0f; // Geçen süreyi hesaplamak için sayaç
    private int currentHour = 0; // Mevcut saat
    private int currentMinute = 0; // Mevcut dakika

    void Start()
    {
        // Saat 00:00'da baþlat
        timer = 0f;
        UpdateClock();
    }

    void Update()
    {
        // Zamaný güncelle
        timer += Time.deltaTime;

        // Saati güncelle
        UpdateClock();

        // Güneþin konumunu ve ýþýðýný güncelle
        float angle;
        float intensity;

        if (currentHour < 9)
        {
            // Saat 5-9 arasý yavaþça aydýnlanýr
            angle = Mathf.Lerp(270f, 360f, (currentHour - 5f) / 4f);
            intensity = Mathf.Lerp(0.1f, 1f, (currentHour - 5f) / 4f);
        }
        else if (currentHour < 19)
        {
            // Saat 9-19 arasý aydýnlýk
            angle = 0f;
            intensity = 1f;
        }
        else if (currentHour < 23.5)
        {
            // Saat 19-23.30 arasý hafif karanlýk
            angle = Mathf.Lerp(0f, 180f, (currentHour - 19f) / 4.5f);
            intensity = Mathf.Lerp(1f, 0.5f, (currentHour - 19f) / 4.5f);
        }
        else
        {
            // Saat 23.30-5 arasý tam karanlýk
            angle = 180f;
            intensity = 0.1f;
        }

        sun.transform.localRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        RenderSettings.ambientIntensity = intensity; // Atmosfer ýþýðýný ayarla
    }

    void UpdateClock()
    {
        // Saat 23:59'dan sonra 00:00'a döner
        currentHour = Mathf.FloorToInt(timer) % 24; // Saat hesaplamasý
        currentMinute = Mathf.FloorToInt((timer - Mathf.Floor(timer)) * 60); // Dakika hesaplamasý

        clockText.text = string.Format("{0:00}:{1:00}", currentHour, currentMinute);
    }



 

}

