using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ilacl : MonoBehaviour
{
    // Hazýr ilaçlar listesi
    public List<string> drugsList;

    // Seçilen ilacýn adýný gösterecek metin alaný
    public TMP_Text selectedDrugText;

    // Seçilen ilacýn adýný tutacak deðiþken
    private string selectedDrug;

    void Start()
    {
        // Baþlangýçta seçilen ilacý null olarak ayarla
        selectedDrug = "";

        // Hazýr ilaçlarý listeye ekle (Örnek ilaçlar)
        drugsList = new List<string>();
        drugsList.Add("Antibiyotik");
        drugsList.Add("Aspirin");

        // Metin alanýný temizle
        selectedDrugText.text = "";
    }

    // Etiketli objeleri týkladýðýmýzda çaðrýlacak fonksiyon
    public void OnObjectClick(string drugName)
    {
        // Seçilen ilacý ayarla
        selectedDrug = drugName;

        // Seçilen ilacýn adýný güncelle
        selectedDrugText.text = "Seçilen Ýlaç: " + selectedDrug;
    }
    void OnMouseDown()
    {
        // Eðer týklanan nesnenin etiketi "ilac" ise iþlem yap
        if (gameObject.CompareTag("ilac"))
        {
            // Nesneye týklandýðýnda çaðrýlacak kod buraya yazýlabilir.
            // Örneðin, DrugManager'daki OnObjectClick fonksiyonunu çaðýrabilirsiniz.
            FindObjectOfType<ilacl>().OnObjectClick(gameObject.name);
        }
    }
}
