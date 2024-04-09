using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fiyat_teklifi : MonoBehaviour
{
    public TMP_Text fiyatTeklifi;

    private Satilacak_Organ satilacakOrgan;
    // Start is called before the first frame update
    void Start()
    {
        satilacakOrgan = FindObjectOfType<Satilacak_Organ>();
        if (satilacakOrgan.fiyat < satilacakOrgan.onerilenFiyat)
        {
            fiyatTeklifi.text = Random.Range(satilacakOrgan.fiyat, satilacakOrgan.fiyat - 10).ToString("0.00") + "$";
        }
        else if (satilacakOrgan.fiyat > satilacakOrgan.onerilenFiyat)
        {
            fiyatTeklifi.text = Random.Range(satilacakOrgan.onerilenFiyat+10, satilacakOrgan.onerilenFiyat - 20).ToString("0.00") + "$";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
