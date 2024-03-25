using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC : MonoBehaviour
{
    public float raycastLength = 5f; // Raycast ���n�n�n uzunlu�u
    public GameObject PcCanvas; // A��lacak olan canvas
    public bool pcCanvasisTrue;
    public bool releaseFlag = false;
    public GameObject sedye;
    public GameObject kesilecekYer;
    public GameObject kalp;
    public GameObject heldObject; // Elinde tutulan objeyi tutmak i�in bir GameObject referans�


    public Transform hand;
    public Vector3 SpawVector3;

    private Economy economy;




    void Start()
    {
        economy= FindObjectOfType<Economy>();
        pcCanvasisTrue = false;
    }

    void Update()
    {
        if (pcCanvasisTrue)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PcCanvas.gameObject.SetActive(false);
                pcCanvasisTrue = false;
            }
        }
        else // Canvas kapal�yken
        {
            Eline_Obje_Alip_Birakma();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PcCanvas.gameObject.SetActive(false);
                pcCanvasisTrue = false;
            }
        }
    }

    private void Eline_Obje_Alip_Birakma()
    {
        RaycastHit hit;
        // Karakterden ileri do�ru bir raycast ���n� ��kar
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("pc"))
                {
                    PcCanvas.SetActive(true);
                    pcCanvasisTrue = true;
                }

                if (hit.collider.CompareTag("obje") &&
                    heldObject == null) // E�er 'obje'ye bas�ld�ysa ve elinde bir obje yoksa
                {
                    Debug.Log("nester ");
                    heldObject = hit.collider.gameObject;
                    heldObject.transform.SetParent(hand);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Collider>().enabled = false;
                }

                if (hit.collider.CompareTag("kalp") && heldObject == null) // E�er 'obje'ye bas�ld�ysa ve elinde bir obje yoksa
                {
                    Debug.Log("nester ");
                    heldObject = hit.collider.gameObject;
                    heldObject.transform.SetParent(hand);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Collider>().enabled = false;
                }
            }

            if (hit.collider.CompareTag("kesilecek"))
            {
                Debug.Log("KES");
                if (heldObject != null && Input.GetMouseButton(0))
                {
                    kesilecekYer.SetActive(false);
                    kalp.GetComponent<Rigidbody>().isKinematic = false;
                    kalp.GetComponent<Rigidbody>().useGravity = true;
                    
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && heldObject != null) // E�er 'E' tu�u b�rak�ld�ysa ve elinde bir obje varsa 
        {
            Debug.Log("birak");
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.transform.SetParent(null);
            heldObject = null;
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
