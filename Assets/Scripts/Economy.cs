using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public TMP_Text text_Money;
    public GameObject yeterliParaYok;

    private float money=100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ParaEkle(100);
            Debug.Log(money);
        }
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
