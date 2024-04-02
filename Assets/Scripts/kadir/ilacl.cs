using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ilacl : MonoBehaviour
{
    // Haz�r ila�lar listesi
    public List<string> drugsList;

    // Se�ilen ilac�n ad�n� g�sterecek metin alan�
    public TMP_Text selectedDrugText;

    // Se�ilen ilac�n ad�n� tutacak de�i�ken
    private string selectedDrug;

    void Start()
    {
        // Ba�lang��ta se�ilen ilac� null olarak ayarla
        selectedDrug = "";

        // Haz�r ila�lar� listeye ekle (�rnek ila�lar)
        drugsList = new List<string>();
        drugsList.Add("Antibiyotik");
        drugsList.Add("Aspirin");

        // Metin alan�n� temizle
        selectedDrugText.text = "";
    }

    // Etiketli objeleri t�klad���m�zda �a�r�lacak fonksiyon
    public void OnObjectClick(string drugName)
    {
        // Se�ilen ilac� ayarla
        selectedDrug = drugName;

        // Se�ilen ilac�n ad�n� g�ncelle
        selectedDrugText.text = "Se�ilen �la�: " + selectedDrug;
    }
    void OnMouseDown()
    {
        // E�er t�klanan nesnenin etiketi "ilac" ise i�lem yap
        if (gameObject.CompareTag("ilac"))
        {
            // Nesneye t�kland���nda �a�r�lacak kod buraya yaz�labilir.
            // �rne�in, DrugManager'daki OnObjectClick fonksiyonunu �a��rabilirsiniz.
            FindObjectOfType<ilacl>().OnObjectClick(gameObject.name);
        }
    }
}
