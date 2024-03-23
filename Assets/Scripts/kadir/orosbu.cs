using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class orosbu: MonoBehaviour
{
    public ney ilacListesi; // �la� listesini referans alal�m
    public TMP_Text secilenIlacText; // Kullan�c�n�n se�ti�i ilac� g�stermek i�in bir metin alan�

    private void Start()
    {
        // �la�lar� ekranda g�ster
        IlacListesiniGoster();
    }

    private void IlacListesiniGoster()
    {
        foreach (string ilac in ilacListesi.ilaclar)
        {
            // �la�lar� ekranda liste olarak g�ster
            Debug.Log(ilac);
        }
    }

    public void IlacSecildi(string ilacAdi)
    {
        // Kullan�c�n�n se�ti�i ilac� g�ster
        secilenIlacText.text = "Se�ilen ila�: " + ilacAdi;
    }
}
