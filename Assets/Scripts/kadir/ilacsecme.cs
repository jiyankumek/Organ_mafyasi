using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ney : MonoBehaviour
{
    // �la�lar� listeye ekleyelim
    public List<string> ilaclar = new List<string>
    {
        "Parol",
        "Antibiyotik",
        // Di�er ila�lar� da buraya ekleyebilirsiniz.
    };

    // Kategorize edilmi� ila�lar� tutacak bir s�zl�k
    private Dictionary<string, List<string>> kategoriler = new Dictionary<string, List<string>>();

    void Start()
    {
        // �la�lar� kategorize edelim
        KategorizeEt();
    }

    void KategorizeEt()
    {
        foreach (string ilac in ilaclar)
        {
            if (IlacBasAgrisinaIyiGelir(ilac))
            {
                KategoriyeEkle("Ba� A�r�s� �la�lar�", ilac);
            }
            // Di�er kategorilere g�re ila�lar� burada kontrol edebilirsiniz.
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
        // Burada ilac�n ba� a�r�s�na iyi gelip gelmedi�ini kontrol edebilirsiniz.
        // �rne�in, bir veritaban�nda bu bilgileri saklayabilirsiniz.
        return true; // Ge�ici olarak her ilac�n ba� a�r�s�na iyi geldi�ini varsayal�m.
    }

    // Fare ile se�im yapma i�levi
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�klama
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string secilenIlac = hit.transform.name;
                Debug.Log("Se�ilen ila�: " + secilenIlac);
            }
        }
    }
}
