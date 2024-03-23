using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlacListesi : MonoBehaviour
{
    // Ilac kategorilerini i�erecek olan s�zl�k
    public Dictionary<string, List<string>> ilacKategorileri;

    // Constructor (Yap�c� metod) ile s�zl��� olu�turuyoruz
    public IlacListesi()
    {
        ilacKategorileri = new Dictionary<string, List<string>>();

        // �la�lar� ve kategorileri ekleyin
        ilacKategorileri.Add("Ba� A�r�s�", new List<string> { "Parol", "Aspirin" });
        ilacKategorileri.Add("Mide Bulant�s�", new List<string> { "Meteospazmil", "Nexium" });
        // �htiyac�n�za g�re ila�lar� ve kategorileri geni�letebilirsiniz
    }
}
