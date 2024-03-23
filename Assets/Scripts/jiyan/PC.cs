using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC : MonoBehaviour
{
    public float raycastLength = 5f; // Raycast ���n�n�n uzunlu�u
    public GameObject PcCanvas; // A��lacak olan canvas
    public bool pcCanvasisTrue=false;
    public GameObject sedye;
    public Vector3 SpawVector3;

    private Economy economy;


    void Start()
    {
        economy= FindObjectOfType<Economy>();
    }

    void Update()
    {
        RaycastHit hit;
        // Karakterden ileri do�ru bir raycast ���n� ��kar
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength))
        {
            // E�er raycast ���n� "pc" etiketli bir nesneye �arparsa
            if (hit.collider.CompareTag("pc"))
            {
                // E�er "E" tu�una bas�l�rsa
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Canvas'� aktif hale getir
                    PcCanvas.gameObject.SetActive(true);
                    pcCanvasisTrue=true;
                   
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
            economy.money = economy.money - 100;
            Instantiate(sedye, SpawVector3, Quaternion.identity);
        }
        else
        {
            economy.yeterliParaYok.SetActive(true);
        }
    }



}
