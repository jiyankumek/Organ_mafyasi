using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ_dolabi : MonoBehaviour
{
    

    private List<GameObject> kalplerListesi = new List<GameObject>();

     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("kalp"))
        {
            // Collider i�ine giren obje "kalp" tag'ine sahip ise listeye ekle
            kalplerListesi.Add(other.gameObject);
            Debug.Log(kalplerListesi.Count);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit �a�r�ld�!");
        Debug.Log("��k�� yapan objenin tag'i: " + other.tag);

        if (other.CompareTag("kalp"))
        {
            Debug.Log("Kalp objesi collider'dan ��kt�!");

            // Collider i�inden ��kan obje "kalp" tag'ine sahip ise listeden ��kar
            kalplerListesi.Remove(other.gameObject);

            // Liste boyutunu kontrol et
            Debug.Log("Kalpler listesinin boyutu: " + kalplerListesi.Count);
        }
    }



    // Kalpler listesini ba�ka bir yerde kullanmak isterseniz bu metodu kullanabilirsiniz
    public List<GameObject> KalplerListesi()
    {
        Debug.Log(kalplerListesi.Count);
        return kalplerListesi;
    }
}
