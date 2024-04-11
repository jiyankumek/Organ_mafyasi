using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fiyat_teklifi : MonoBehaviour
{
    public TMP_Text fiyatTeklifi;
    public float satilacakfiyat;

    private Satilacak_Organ satilacakOrgan;

    private Economy economy;
    // Start is called before the first frame update
    void Start()
    {
        economy = FindObjectOfType<Economy>();
        satilacakOrgan = GetComponentInParent<Satilacak_Organ>();
        if (satilacakOrgan.fiyat < satilacakOrgan.onerilenFiyat)
        {
            satilacakfiyat = Random.Range(satilacakOrgan.fiyat, satilacakOrgan.fiyat - 10);
            fiyatTeklifi.text = satilacakfiyat.ToString("0.00") + "$";
        }
        else if (satilacakOrgan.fiyat > satilacakOrgan.onerilenFiyat)
        {
            satilacakfiyat = Random.Range(satilacakOrgan.onerilenFiyat + 10, satilacakOrgan.onerilenFiyat - 20);
            fiyatTeklifi.text = satilacakfiyat.ToString("0.00") + "$";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (satilacakOrgan.satisyapildi==true)
        {
            economy.ParaEkle(satilacakfiyat);
            
        }   
    }
   
}
