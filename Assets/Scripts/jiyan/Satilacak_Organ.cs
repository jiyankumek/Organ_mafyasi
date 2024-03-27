using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Satilacak_Organ : MonoBehaviour
{
    public TMP_InputField satisInput;

    private Economy economy;
    // Start is called before the first frame update
    void Start()
    {
        economy = FindObjectOfType<Economy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Sell()
    { 
        // Input alanýndaki metni alýp bir tam sayýya dönüþtürüyoruz
        int fiyat;
        if (!int.TryParse(satisInput.text, out fiyat))
        {
            Debug.LogError("Geçersiz fiyat giriþi!");
            return;
        }
        

        // Satýþ fiyatý belirlendiði zaman prefabý yok ediyoruz
        Destroy(gameObject);
        economy.ParaEkle(fiyat);
    }
    

}
