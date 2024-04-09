using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject teklifFoto;
    public GameObject organFoto;
    public GameObject fiyatTeklifPrefab;

    private Economy economy;
    private Organ_dolabi organDolabi;
    private PC pc;
    private Fiyat_teklifi fiyatTeklifi;

    public float onerilenFiyat = 100f;
    public float fiyat;

    void Start()
    {
        economy = FindObjectOfType<Economy>();
        organDolabi = FindObjectOfType<Organ_dolabi>();
        pc = FindObjectOfType<PC>();
        fiyatTeklifi = FindObjectOfType<Fiyat_teklifi>();
    }

    public void Sell()
    {
        if (!float.TryParse(satisInput.text, out fiyat))
        {
            Debug.LogError("Ge�ersiz fiyat giri�i!");
            sayiGiriniz.SetActive(true);
            return;
        }

        �nputGameObject.SetActive(false);
        sellButton.SetActive(false);
        organFoto.SetActive(false);
        changepriceButton.SetActive(true);
        gameObject.transform.SetParent(pc.contentIlanlar�m.transform);

        StartCoroutine(SatisSuresiBelirle(fiyat));
        
        price.text = fiyat.ToString("0.00") + "$";
        foreach (Transform childTransform in pc.organdolabi.transform)//burada dolaptaki objeleri siliyor
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

    private IEnumerator SatisSuresiBelirle(float fiyat)
    {
        float satisSuresi;

        if (fiyat < onerilenFiyat)
        {
            // Fiyat, �nerilen fiyat�n alt�ndaysa, sat�� s�resi 5-15 saniye aras�nda olacak
            satisSuresi = Random.Range(5f, 15f);
        }
        else if (fiyat < onerilenFiyat + 99.99f)
        {
            // Fiyat, �nerilen fiyat ile bu fiyat�n +99.99'u aras�ndaysa, sat�� s�resi 20-40 saniye aras�nda olacak
            satisSuresi = Random.Range(20f, 40f);
        }
        else
        {
            satisSuresi = Random.Range(200f, 300f);
        }

        float kalanSure = satisSuresi;

        while (kalanSure > 0)
        {
            yield return new WaitForSeconds(1);
            kalanSure--;

            Debug.Log("Kalan S�re: " + kalanSure); // Kalan s�reyi kontrol etmek i�in Debug.Log kullan�m�
        }

        Debug.Log("Zaman doldu!");
        teklifFoto.SetActive(true);
        organFoto.SetActive(false);
        
        
    }

    public void Teklif_Foto_button()
    {

        foreach (Transform child in pc.contentTeklifListesi)//buras� teklifleri siliyor
        {
            // Child'i yok et
            Destroy(child.gameObject);
        }
        
        Instantiate(fiyatTeklifPrefab, pc.contentTeklifListesi);
        pc.teklifListesi.SetActive(false);
        pc.teklifListesi.SetActive(true);
        
    }
}
