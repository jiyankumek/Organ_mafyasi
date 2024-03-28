using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Satilacak_Organ : MonoBehaviour
{
    public TMP_InputField satisInput;

    private Economy economy;
    private Organ_dolabi organDolabi;
    private PC pc;
    // Start is called before the first frame update
    void Start()
    {
        economy = FindObjectOfType<Economy>();
        organDolabi = FindObjectOfType<Organ_dolabi>();
        pc = FindObjectOfType<PC>();
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
        foreach (Transform childTransform in pc.organdolabi.transform)
        {
            Debug.Log("Child obje bulundu: " + childTransform.name);

            GameObject child = childTransform.gameObject; // Transform'u GameObject'e çeviriyoruz
            // Çocuk objenin tag'ini kontrol edelim
            if (child.CompareTag("kalp"))
            {
                Destroy(child);
                organDolabi.kalplerListesi.Remove(child);
                Debug.Log("kalp sil");
                break;
            }

           
        }

        economy.ParaEkle(fiyat);
    }
    

}
