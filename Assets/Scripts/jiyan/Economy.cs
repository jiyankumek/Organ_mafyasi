using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public List<Transform> hastaKayitSirasiList;
    private GameObject hastakayit;


    public TMP_Text text_Money;
    public GameObject yeterliParaYok;

    public float money=250;

    public float sure=10;

    public List<GameObject> sedyeSayisi;

    void Awake()
    {
        hastakayit = GameObject.FindWithTag("hastakayit");
        hastaKayitSirasiList.Add(hastakayit.transform.GetChild(0));
        hastaKayitSirasiList.Add(hastakayit.transform.GetChild(1));
        hastaKayitSirasiList.Add(hastakayit.transform.GetChild(2));
        hastaKayitSirasiList.Add(hastakayit.transform.GetChild(3));
        hastaKayitSirasiList.Add(hastakayit.transform.GetChild(4));
        hastaKayitSirasiList.Add(hastakayit.transform.GetChild(5));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SatisSuresiBelirle());
        sedyeSayisi.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        text_Money.text = money.ToString("0.00") + "$";
    }

    public void ParaEkle(float eklenecek_Money)
    {
        money = money + eklenecek_Money;
        text_Money.text = money.ToString();
    }

    public void ParaCikar(float eksilecek_Money)
    {
        money = money - eksilecek_Money;
        if (money < 0)
        {
            yeterliParaYok.SetActive(true);
        }
        else
        {
            text_Money.text = money.ToString();
        }
    }

    public void YeterliParaYok_kapatmaButonu()
    {
        yeterliParaYok.SetActive(false);
    }
    public IEnumerator SatisSuresiBelirle()
    {
        sedyeSayisi.Clear();
        while (sure > 0)
        {
            yield return new WaitForSeconds(1);
            sure--;

            Debug.Log("Kalan Süre: " + sure); // Kalan süreyi kontrol etmek için Debug.Log kullanýmý

            
        }

        GameObject[] sedyeler = GameObject.FindGameObjectsWithTag("sedye");

        // Bulunan sedye objelerini listeye ekleyin
        foreach (GameObject sedye in sedyeler)
        {
            if (!sedyeSayisi.Contains(sedye))
            {
                sedyeSayisi.Add(sedye);
            }
        }
        
        Debug.Log("Zaman doldu!");
        Debug.Log(sedyeSayisi.Count);
        
        
        
    }

}
