using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC : MonoBehaviour
{
    public float raycastLength = 5f; // Raycast ýþýnýnýn uzunluðu
    public GameObject PcCanvas; // Açýlacak olan canvas
    public bool pcCanvasisTrue;
    public GameObject sedye;
    public Vector3 SpawVector3;

    private Economy economy;


    void Start()
    {
        economy= FindObjectOfType<Economy>();
        pcCanvasisTrue = false;
    }

    void Update()
    {
        if (pcCanvasisTrue == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PcCanvas.gameObject.SetActive(false);
                pcCanvasisTrue = false;
            }
        }
        else // Canvas kapalýyken
        {
            RaycastHit hit;
            // Karakterden ileri doðru bir raycast ýþýný çýkar
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength))
            {
                // Eðer raycast ýþýný "pc" etiketli bir nesneye çarparsa
                if (hit.collider.CompareTag("pc"))
                {
                    Debug.Log("hit");
                    // Eðer "E" tuþuna basýlýrsa
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Canvas'ý aktif hale getir
                        PcCanvas.gameObject.SetActive(true);
                        pcCanvasisTrue = true;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PcCanvas.gameObject.SetActive(false);
            pcCanvasisTrue = false;
        }
    }


    public void Buy_Sedye()
    {
        if (economy.money >= 100)
        {
            economy.ParaCikar(100);
            Instantiate(sedye, SpawVector3, Quaternion.identity);
        }
        else
        {
            economy.yeterliParaYok.SetActive(true);
        }
    }



}
