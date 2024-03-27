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
        // Input alan�ndaki metni al�p bir tam say�ya d�n��t�r�yoruz
        int fiyat;
        if (!int.TryParse(satisInput.text, out fiyat))
        {
            Debug.LogError("Ge�ersiz fiyat giri�i!");
            return;
        }
        

        // Sat�� fiyat� belirlendi�i zaman prefab� yok ediyoruz
        Destroy(gameObject);
        economy.ParaEkle(fiyat);
    }
    

}
