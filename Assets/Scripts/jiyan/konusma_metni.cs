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
        // �shal metni
        TextMessageList.Add("Merhaba, son birka� g�nd�r ishal ya��yorum ve kendimi �ok k�t� hissediyorum. Bu durum beni �ok zorluyor ve g�nl�k ya�ant�m� etkiliyor. Tedaviniz ne kadar maliyete geliyor?");

        // B�brek ta�� metni
        TextMessageList.Add("Merhaba, �iddetli b�brek a�r�lar�m var ve idrar�mda kan g�rd�m. Bu durum beni endi�elendiriyor ve g�nl�k hayat�m� aksat�yor. Tedavi maliyetinizi ��renebilir miyim?");

        // Mide a�r�s� ve bulant�s� metni
        TextMessageList.Add("Merhaba, son birka� g�nd�r mide a�r�s� ve bulant�yla u�ra��yorum. Bu durum beni rahats�z ediyor ve i�imi yapmam� zorla�t�r�yor. Tedavinizin fiyat�n� ��renmek istiyorum.");

        // Vir�s metni
        TextMessageList.Add("Merhaba, birka� g�nd�r ate� ve halsizlikle u�ra��yorum. Bu semptomlar g�nl�k aktivitelerimi s�n�rl�yor ve i�imi aksat�yor. Bu rahats�zl�k i�in tedavinizin fiyat� nedir?");

        // Grip metni
        TextMessageList.Add("Merhaba, burun ak�nt�s�, ba� a�r�s� ve v�cut a�r�lar�m var. Bu semptomlar g�nl�k ya�am�m� zorla�t�r�yor. Grip tedaviniz ne kadar maliyetli?");

        // So�uk alg�nl��� metni
        TextMessageList.Add("Merhaba, bo�az a�r�s�, �ks�r�k ve burun t�kan�kl��� ya��yorum. So�uk alg�nl��� belirtileri g�nl�k ya�am�m� etkiliyor. Tedavinizin fiyat�n� ��renebilir miyim?");

        // COVID-19 metni
        TextMessageList.Add("Merhaba, birka� g�nd�r �ks�r�k, ate� ve tat/koku kayb� ya��yorum. COVID-19 olup olmad���n� merak ediyorum. Tedavinizin maliyeti nedir?");

        // Ba� a�r�s� metni
        TextMessageList.Add("Merhaba, s�rekli ba� a�r�s� �ekiyorum ve bu beni rahats�z ediyor. G�nl�k i�lerimi yapmam� zorla�t�r�yor. Tedavinizin fiyat�n� ��renmek istiyorum.");

        // Nezle metni
        TextMessageList.Add("Merhaba, burun t�kan�kl��� ve hap��r�kla u�ra��yorum. Nezle belirtileri hayat�m� zorla�t�r�yor. Tedavinizin maliyeti nedir?");

        // Alerjiler metni
        TextMessageList.Add("Merhaba, cildimde ka��nt� ve k�zar�kl�k var. Alerji belirtileri g�nl�k ya�am�m� etkiliyor. Bu durum i�in tedavinizin maliyeti nedir?");

        int a = Random.Range(0, TextMessageList.Count-1);
        Debug.Log(a);

        Instantiate(konusmaMetni, ContentGameObject.transform);
        konusmaMetni.text = TextMessageList[a];



    }

    void Update()
    {
        // G�ncellenmesi gereken bir �ey yoksa Update fonksiyonunu bo� b�rakabilirsiniz.
    }

    public void HastanefiyatGirdisi()
    {
        
        fiyatMetni.gameObject.SetActive(true);
        // Girilen metni float bir de�ere d�n��t�r
        float.TryParse(FiyatGirdisiInputField.text, out fiyat);
        fiyatMetni.text = fiyat.ToString("F")+"$";
        Debug.Log(fiyat);
        if (!float.TryParse(FiyatGirdisiInputField.text, out fiyat))
        {
            Debug.LogError("Ge�ersiz fiyat giri�i!");
            fiyat = 0f; // Hata durumunda fiyat� s�f�rla veya ba�ka bir de�ere e�itle

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
