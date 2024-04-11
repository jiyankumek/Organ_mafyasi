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
   
    public GameObject ÝnputGameObject;
    public GameObject sayiGiriniz;
    public GameObject teklifFoto;
    public GameObject organFoto;
    public GameObject fiyatTeklifPrefab;
    public GameObject teklifListesi;

    public Transform contentTeklifListesi;
    public bool satisyapildi=false;

    private Economy economy;
    private Organ_dolabi organDolabi;
    private PC pc;
    private Fiyat_teklifi fiyatTeklifi;
    private Coroutine satisCoroutine; // Coroutine referansýný sakla

    public float onerilenFiyat = 100f;
    public float fiyat;

    void Start()
    {
        economy = FindObjectOfType<Economy>();
        organDolabi = FindObjectOfType<Organ_dolabi>();
        pc = FindObjectOfType<PC>();
        fiyatTeklifi = FindObjectOfType<Fiyat_teklifi>();

    }

    public void Organ_Satildi() //organlarý dolaptan silen fonksiyon
    {
        
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
        Destroy(gameObject);
    }
    public void Sell()
    {
        if (!float.TryParse(satisInput.text, out fiyat))//float input girdisi istiyor
        {
            Debug.LogError("Geçersiz fiyat giriþi!");
            sayiGiriniz.SetActive(true);
            return;
        }

        ÝnputGameObject.SetActive(false);
        sellButton.SetActive(false);
        organFoto.SetActive(false);
        changepriceButton.SetActive(true);
        gameObject.transform.SetParent(pc.contentIlanlarým.transform);//ilanlarým paneline atýyor 

        StartCoroutine(SatisSuresiBelirle(fiyat));
        
        price.text = fiyat.ToString("0.00") + "$";
       
        

    }


    public void Change_Price()
    {
        inputPrice.SetActive(true);
    }

    public void Edit_Price()
    {

        inputPrice.SetActive(false);

        Debug.Log(satisCoroutine);
        
        float floatVal;
        bool isFloat = float.TryParse(newPrice.text, out floatVal);

        if (isFloat)
        {
            price.text = floatVal.ToString("0.00") + "$";
            sayiGiriniz.SetActive(false);
            teklifFoto.SetActive(false);
            fiyat = floatVal;
           

            // Yeni bir satýþ süresi belirleme Coroutine'u baþlat ve referansýný sakla
            satisCoroutine = StartCoroutine(SatisSuresiBelirle(fiyat));

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
        // Girilen metni float bir deðere dönüþtür
        if (!float.TryParse(satisInput.text, out fiyat))
        {
            Debug.LogError("Geçersiz fiyat giriþi!");
            fiyat = 0f; // Hata durumunda fiyatý sýfýrla veya baþka bir deðere eþitle
            return;
        }
        

        // Fiyatý satisInput alanýnýn metin deðeriyle eþitle
        satisInput.text = fiyat.ToString();
        
    }

    private IEnumerator SatisSuresiBelirle(float fiyat)
    {
        float satisSuresi;

        if (fiyat < onerilenFiyat)
        {
            // Fiyat, önerilen fiyatýn altýndaysa, satýþ süresi 5-15 saniye arasýnda olacak
            satisSuresi = Random.Range(5f, 15f);
        }
        else if (fiyat < onerilenFiyat + 99.99f)
        {
            // Fiyat, önerilen fiyat ile bu fiyatýn +99.99'u arasýndaysa, satýþ süresi 20-40 saniye arasýnda olacak
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

            Debug.Log("Kalan Süre: " + kalanSure); // Kalan süreyi kontrol etmek için Debug.Log kullanýmý
        }

        Debug.Log("Zaman doldu!");
        teklifFoto.SetActive(true);
        organFoto.SetActive(false);

        // Coroutine tamamlandýðýnda, referansý null olarak ayarla
        satisCoroutine = null;



    }

    public void Teklif_Foto_button()
    {
        
        /*foreach (Transform child in contentTeklifListesi)//burasý teklifleri siliyor
        {
            // Child'i yok et
            Destroy(child.gameObject);
        }*/
        
        Instantiate(fiyatTeklifPrefab, contentTeklifListesi);
        

        teklifListesi.SetActive(true);
        
        /*if (teklifListesi.activeSelf)
        {
            teklifListesi.SetActive(false);
        }*/

    }
    public void Tekliflistesi_Sat()
    {
        Organ_Satildi();
        satisyapildi = true;

    }

    public void Tekliflistesi_Satma()
    {

        foreach (Transform child in contentTeklifListesi)
        {
            // Child'i yok et
            Destroy(child.gameObject);
        }
        teklifListesi.SetActive(false);
        teklifFoto.SetActive(false);
        StartCoroutine(SatisSuresiBelirle(fiyat));

    }
}
