using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Satilacak_Organ : MonoBehaviour
{
    public TMP_Text price;
    public TMP_InputField newPrice;
    public TMP_InputField satisInput;

    public GameObject inputPrice;
    public GameObject sellButton;
    public GameObject changepriceButton;
    public GameObject OrganGameObject;
    public GameObject �nputGameObject;
    public GameObject sayiGiriniz;

    private Economy economy;
    private Organ_dolabi organDolabi;
    private PC pc;

    private float fiyat;

    void Start()
    {
        economy = FindObjectOfType<Economy>();
        organDolabi = FindObjectOfType<Organ_dolabi>();
        pc = FindObjectOfType<PC>();
    }

    public void Sell()
    {
        if (!float.TryParse(satisInput.text, out fiyat))
        {
            Debug.LogError("Ge�ersiz fiyat giri�i!");
            sayiGiriniz.SetActive(true);
            return;
        }

        sellButton.SetActive(false);
        changepriceButton.SetActive(true);
        gameObject.transform.SetParent(pc.contentIlanlar�m);
        OrganGameObject.SetActive(false);
        �nputGameObject.SetActive(false);

        price.text = fiyat.ToString("0.00") + "$";
        foreach (Transform childTransform in pc.organdolabi.transform)
        {
            GameObject child = childTransform.gameObject;
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

    public void Change_Price()
    {
        inputPrice.SetActive(true);
    }

    public void Edit_Price()
    {
        inputPrice.SetActive(false);

        float floatVal;
        bool isFloat = float.TryParse(newPrice.text, out floatVal);

        if (isFloat)
        {
            price.text = floatVal.ToString("0.00") + "$";
            sayiGiriniz.SetActive(false);
        }
        else
        {
            sayiGiriniz.SetActive(true);
            return;
        }
    }

    public void Close_sayiGiriniz()
    {
        sayiGiriniz.SetActive(false);
    }

    public void SatisInputGuncelle()
    {
        // Girilen metni float bir de�ere d�n��t�r
        if (!float.TryParse(satisInput.text, out fiyat))
        {
            Debug.LogError("Ge�ersiz fiyat giri�i!");
            fiyat = 0f; // Hata durumunda fiyat� s�f�rla veya ba�ka bir de�ere e�itle
            return;
        }

        // Fiyat� satisInput alan�n�n metin de�eriyle e�itle
        satisInput.text = fiyat.ToString();
    }

}
