using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daychan : MonoBehaviour
{
    public Light sun; // Güneþ ýþýðý
    public float dayDuration = 60f; // Bir günün süresi (saniye cinsinden)
    private float timer = 0f; // Geçen süreyi hesaplamak için sayaç

    void Update()
    {
        // Zamaný güncelle
        timer += Time.deltaTime;

        // Gece-gündüz döngüsü için bir döngü oluþtur
        if (timer > dayDuration)
        {
            // Güneþin konumunu ve ýþýðýný güncelle
            sun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            RenderSettings.ambientIntensity = 0.1f; // Gece atmosfer ýþýðýný ayarla

            // Zamaný sýfýrla
            timer = 0f;
        }
        else
        {
            // Güneþin konumunu ve ýþýðýný güncelle
            float angle = Mathf.Lerp(0f, 180f, timer / dayDuration); // Güneþin yörüngesini hesapla
            sun.transform.localRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
            RenderSettings.ambientIntensity = Mathf.Lerp(0.1f, 1f, timer / dayDuration); // Atmosfer ýþýðýný ayarla
        }
    }
}

