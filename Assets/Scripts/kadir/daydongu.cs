using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gun : MonoBehaviour
{
    public Light sun; // G�ne� �����
    public TMP_Text clockText; // Saat g�stergesi metni
    public float dayDuration = 60f; // Bir g�n�n s�resi (saniye cinsinden)

    private float timer = 0f; // Ge�en s�reyi hesaplamak i�in saya�
    private int currentHour = 0; // Mevcut saat
    private int currentMinute = 0; // Mevcut dakika

    void Start()
    {
        // Saat 00:00'da ba�lat
        timer = 0f;
        UpdateClock();
    }

    void Update()
    {
        // Zaman� g�ncelle
        timer += Time.deltaTime;

        // Saati g�ncelle
        UpdateClock();

        // G�ne�in konumunu ve �����n� g�ncelle
        float angle;
        float intensity;

        if (currentHour < 9)
        {
            // Saat 5-9 aras� yava��a ayd�nlan�r
            angle = Mathf.Lerp(270f, 360f, (currentHour - 5f) / 4f);
            intensity = Mathf.Lerp(0.1f, 1f, (currentHour - 5f) / 4f);
        }
        else if (currentHour < 19)
        {
            // Saat 9-19 aras� ayd�nl�k
            angle = 0f;
            intensity = 1f;
        }
        else if (currentHour < 23.5)
        {
            // Saat 19-23.30 aras� hafif karanl�k
            angle = Mathf.Lerp(0f, 180f, (currentHour - 19f) / 4.5f);
            intensity = Mathf.Lerp(1f, 0.5f, (currentHour - 19f) / 4.5f);
        }
        else
        {
            // Saat 23.30-5 aras� tam karanl�k
            angle = 180f;
            intensity = 0.1f;
        }

        sun.transform.localRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        RenderSettings.ambientIntensity = intensity; // Atmosfer �����n� ayarla
    }

    void UpdateClock()
    {
        // Saat 23:59'dan sonra 00:00'a d�ner
        currentHour = Mathf.FloorToInt(timer) % 24; // Saat hesaplamas�
        currentMinute = Mathf.FloorToInt((timer - Mathf.Floor(timer)) * 60); // Dakika hesaplamas�

        clockText.text = string.Format("{0:00}:{1:00}", currentHour, currentMinute);
    }



 

}

