using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class orosbu: MonoBehaviour
{
    public ney ilacListesi; // Ýlaç listesini referans alalým
    public TMP_Text secilenIlacText; // Kullanýcýnýn seçtiði ilacý göstermek için bir metin alaný

    private void Start()
    {
        // Ýlaçlarý ekranda göster
        IlacListesiniGoster();
    }

    private void IlacListesiniGoster()
    {
        foreach (string ilac in ilacListesi.ilaclar)
        {
            // Ýlaçlarý ekranda liste olarak göster
            Debug.Log(ilac);
        }
    }

    public void IlacSecildi(string ilacAdi)
    {
        // Kullanýcýnýn seçtiði ilacý göster
        secilenIlacText.text = "Seçilen ilaç: " + ilacAdi;
    }
}
