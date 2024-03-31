using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ilanlarım : MonoBehaviour
{
    public GameObject inputPrice;

    public TMP_Text price;
    public TMP_InputField newPrice;

    private Satilacak_Organ satilacakOrgan;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change_Price()//button olan 
    {
        inputPrice.SetActive(true);
    }

    public void Edit_Price()// input girildiginde calisacak olan
    {
        inputPrice.SetActive(false);
        price.text = newPrice.text+"$";
    }
}
