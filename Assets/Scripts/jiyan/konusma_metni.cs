using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KonusmaMetni : MonoBehaviour
{
    public TMP_Text konusmaMetni;
    public TMP_Text fiyatMetni;
    public TMP_InputField FiyatGirdisiInputField;

    public GameObject ContentGameObject;
    public GameObject Uyari;

    public List<string> TextMessageList;

    private float fiyat;

    void Start()
    {
        // Ýshal metni
        TextMessageList.Add("Merhaba, son birkaç gündür ishal yaþýyorum ve kendimi çok kötü hissediyorum. Bu durum beni çok zorluyor ve günlük yaþantýmý etkiliyor. Tedaviniz ne kadar maliyete geliyor?");

        // Böbrek taþý metni
        TextMessageList.Add("Merhaba, þiddetli böbrek aðrýlarým var ve idrarýmda kan gördüm. Bu durum beni endiþelendiriyor ve günlük hayatýmý aksatýyor. Tedavi maliyetinizi öðrenebilir miyim?");

        // Mide aðrýsý ve bulantýsý metni
        TextMessageList.Add("Merhaba, son birkaç gündür mide aðrýsý ve bulantýyla uðraþýyorum. Bu durum beni rahatsýz ediyor ve iþimi yapmamý zorlaþtýrýyor. Tedavinizin fiyatýný öðrenmek istiyorum.");

        // Virüs metni
        TextMessageList.Add("Merhaba, birkaç gündür ateþ ve halsizlikle uðraþýyorum. Bu semptomlar günlük aktivitelerimi sýnýrlýyor ve iþimi aksatýyor. Bu rahatsýzlýk için tedavinizin fiyatý nedir?");

        // Grip metni
        TextMessageList.Add("Merhaba, burun akýntýsý, baþ aðrýsý ve vücut aðrýlarým var. Bu semptomlar günlük yaþamýmý zorlaþtýrýyor. Grip tedaviniz ne kadar maliyetli?");

        // Soðuk algýnlýðý metni
        TextMessageList.Add("Merhaba, boðaz aðrýsý, öksürük ve burun týkanýklýðý yaþýyorum. Soðuk algýnlýðý belirtileri günlük yaþamýmý etkiliyor. Tedavinizin fiyatýný öðrenebilir miyim?");

        // COVID-19 metni
        TextMessageList.Add("Merhaba, birkaç gündür öksürük, ateþ ve tat/koku kaybý yaþýyorum. COVID-19 olup olmadýðýný merak ediyorum. Tedavinizin maliyeti nedir?");

        // Baþ aðrýsý metni
        TextMessageList.Add("Merhaba, sürekli baþ aðrýsý çekiyorum ve bu beni rahatsýz ediyor. Günlük iþlerimi yapmamý zorlaþtýrýyor. Tedavinizin fiyatýný öðrenmek istiyorum.");

        // Nezle metni
        TextMessageList.Add("Merhaba, burun týkanýklýðý ve hapþýrýkla uðraþýyorum. Nezle belirtileri hayatýmý zorlaþtýrýyor. Tedavinizin maliyeti nedir?");

        // Alerjiler metni
        TextMessageList.Add("Merhaba, cildimde kaþýntý ve kýzarýklýk var. Alerji belirtileri günlük yaþamýmý etkiliyor. Bu durum için tedavinizin maliyeti nedir?");

        int a = Random.Range(0, TextMessageList.Count-1);
        Debug.Log(a);

        Instantiate(konusmaMetni, ContentGameObject.transform);
        konusmaMetni.text = TextMessageList[a];



    }

    void Update()
    {
        // Güncellenmesi gereken bir þey yoksa Update fonksiyonunu boþ býrakabilirsiniz.
    }

    public void HastanefiyatGirdisi()
    {
        
        fiyatMetni.gameObject.SetActive(true);
        // Girilen metni float bir deðere dönüþtür
        float.TryParse(FiyatGirdisiInputField.text, out fiyat);
        fiyatMetni.text = fiyat.ToString("F")+"$";
        Debug.Log(fiyat);
        if (!float.TryParse(FiyatGirdisiInputField.text, out fiyat))
        {
            Debug.LogError("Geçersiz fiyat giriþi!");
            fiyat = 0f; // Hata durumunda fiyatý sýfýrla veya baþka bir deðere eþitle

            Uyari.SetActive(true);
            return;
        }

        
        FiyatGirdisiInputField.gameObject.SetActive(false);
        
    }

    public void UyariButton()
    {
        Uyari.SetActive(false);
    }
}
