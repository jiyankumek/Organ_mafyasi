using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ney : MonoBehaviour
{
    // Ýlaçlarý listeye ekleyelim
    public List<string> ilaclar = new List<string>
    {
        "Parol",
        "Antibiyotik",
        // Diðer ilaçlarý da buraya ekleyebilirsiniz.
    };

    // Kategorize edilmiþ ilaçlarý tutacak bir sözlük
    private Dictionary<string, List<string>> kategoriler = new Dictionary<string, List<string>>();

    void Start()
    {
        // Ýlaçlarý kategorize edelim
        KategorizeEt();
    }

    void KategorizeEt()
    {
        foreach (string ilac in ilaclar)
        {
            if (IlacBasAgrisinaIyiGelir(ilac))
            {
                KategoriyeEkle("Baþ Aðrýsý Ýlaçlarý", ilac);
            }
            // Diðer kategorilere göre ilaçlarý burada kontrol edebilirsiniz.
        }
    }

    void KategoriyeEkle(string kategoriAdi, string ilacAdi)
    {
        if (!kategoriler.ContainsKey(kategoriAdi))
        {
            kategoriler[kategoriAdi] = new List<string>();
        }
        kategoriler[kategoriAdi].Add(ilacAdi);
    }

    bool IlacBasAgrisinaIyiGelir(string ilacAdi)
    {
        // Burada ilacýn baþ aðrýsýna iyi gelip gelmediðini kontrol edebilirsiniz.
        // Örneðin, bir veritabanýnda bu bilgileri saklayabilirsiniz.
        return true; // Geçici olarak her ilacýn baþ aðrýsýna iyi geldiðini varsayalým.
    }

    // Fare ile seçim yapma iþlevi
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týklama
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string secilenIlac = hit.transform.name;
                Debug.Log("Seçilen ilaç: " + secilenIlac);
            }
        }
    }
}
