using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daychan : MonoBehaviour
{
    public Light sun; // G�ne� �����
    public float dayDuration = 60f; // Bir g�n�n s�resi (saniye cinsinden)
    private float timer = 0f; // Ge�en s�reyi hesaplamak i�in saya�

    void Update()
    {
        // Zaman� g�ncelle
        timer += Time.deltaTime;

        // Gece-g�nd�z d�ng�s� i�in bir d�ng� olu�tur
        if (timer > dayDuration)
        {
            // G�ne�in konumunu ve �����n� g�ncelle
            sun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            RenderSettings.ambientIntensity = 0.1f; // Gece atmosfer �����n� ayarla

            // Zaman� s�f�rla
            timer = 0f;
        }
        else
        {
            // G�ne�in konumunu ve �����n� g�ncelle
            float angle = Mathf.Lerp(0f, 180f, timer / dayDuration); // G�ne�in y�r�ngesini hesapla
            sun.transform.localRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
            RenderSettings.ambientIntensity = Mathf.Lerp(0.1f, 1f, timer / dayDuration); // Atmosfer �����n� ayarla
        }
    }
}

