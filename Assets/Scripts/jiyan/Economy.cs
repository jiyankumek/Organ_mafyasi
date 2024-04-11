using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public TMP_Text text_Money;
    public GameObject yeterliParaYok;

    public float money=250;
    // Start is called before the first frame update
    void Start()
    {
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
}
