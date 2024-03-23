using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlacListesi : MonoBehaviour
{
    // Ilac kategorilerini içerecek olan sözlük
    public Dictionary<string, List<string>> ilacKategorileri;

    // Constructor (Yapýcý metod) ile sözlüðü oluþturuyoruz
    public IlacListesi()
    {
        ilacKategorileri = new Dictionary<string, List<string>>();

        // Ýlaçlarý ve kategorileri ekleyin
        ilacKategorileri.Add("Baþ Aðrýsý", new List<string> { "Parol", "Aspirin" });
        ilacKategorileri.Add("Mide Bulantýsý", new List<string> { "Meteospazmil", "Nexium" });
        // Ýhtiyacýnýza göre ilaçlarý ve kategorileri geniþletebilirsiniz
    }
}
