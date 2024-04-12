using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ_dolabi : MonoBehaviour
{
    

    public List<GameObject> kalplerListesi = new List<GameObject>();
    public List<Transform> dolapKapasitesi=new List<Transform>();
    
     
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
            // Collider içine giren obje "kalp" tag'ine sahip ise listeye ekle
            kalplerListesi.Add(other.gameObject);
            Debug.Log(kalplerListesi.Count);
        }
    }


    // Kalpler listesini baþka bir yerde kullanmak isterseniz bu metodu kullanabilirsiniz
    public List<GameObject> KalplerListesi()
    {
        Debug.Log(kalplerListesi.Count);
        return kalplerListesi;
    }
}
