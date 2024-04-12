using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC : MonoBehaviour
{
    public float raycastLength = 2.5f; // Raycast ���n�n�n uzunlu�u

    public Canvas PcCanvas; // A��lacak olan canvas
    public GameObject sell;
    public GameObject buy;
    public GameObject ilanlar�m;
    public GameObject ilanVer;
    public GameObject teklifPrefab;
    

    public bool pcCanvasisTrue;

    public GameObject sedye;
    public GameObject kesilecekYer;
    public GameObject kalp;
    public GameObject heldObject; // Elinde tutulan objeyi tutmak i�in bir GameObject referans�
    public GameObject Organ_satis;
    

   
    public Transform hand;
    public Transform objeHand;
    public Transform content_satis;
    public Transform contentIlanlar�m;
    

    public Vector3 SpawVector3;

    private Economy economy;
    private Organ_dolabi organDolabi;


    void Start()
    {
        economy= FindObjectOfType<Economy>();
        organDolabi = FindObjectOfType<Organ_dolabi>();
        pcCanvasisTrue = false;
        
    }

    void Update()
    {
       
        if (pcCanvasisTrue)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                PcCanvas.enabled = false;
                pcCanvasisTrue = false;
                foreach (Transform child in content_satis.transform)
                {
                    Destroy(child.gameObject);
                }

            }
        }
        else // Canvas kapal�yken
        {
            Eline_Obje_Alip_Birakma();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PcCanvas.enabled = false;
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
                    PcCanvas.enabled = true;

                    pcCanvasisTrue = true;
                    foreach (var organ in organDolabi.kalplerListesi)
                    {
                        Instantiate(Organ_satis, content_satis);
                       // Destroy(Organ_satis);
                    }
                }

                if (hit.collider.CompareTag("sedye") && heldObject == null)
                {
                    heldObject = hit.collider.gameObject;
                    heldObject.transform.SetParent(objeHand);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // S�f�r d�n�� a��lar�na ayarla
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Collider>().enabled = true;
                }

                if (hit.collider.CompareTag("obje") && heldObject == null) // E�er 'obje'ye bas�ld�ysa ve elinde bir obje yoksa
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
                    Debug.Log("kesildi ve d��t�");
                    
                }
                
            }
            if(hit.collider.CompareTag("dolap"))
            {
                if (Input.GetMouseButton(0)&&heldObject.CompareTag("kalp"))
                {
                    heldObject.GetComponent<Collider>().enabled = true;
                    heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    heldObject.GetComponent<Rigidbody>().useGravity = true;

                    /* for (int i=0; i < organDolabi.dolapKapasitesi.Count; i++)
                     {
                         heldObject.transform.SetParent(organDolabi.dolapKapasitesi[i]);
                         if ()
                         heldObject.transform.localPosition = organDolabi.dolapKapasitesi[0].localPosition;
                     }*/


                    bool alanBos = false; // Bo� alan�n bulunup bulunmad���n� kontrol etmek i�in bir bayrak

                    for (int i = 0; i < organDolabi.dolapKapasitesi.Count; i++)
                    {
                        // E�er dolapKapasitesi listesindeki i. eleman�n alt�nda hi� �ocuk yoksa (yani i�i bo�sa)
                        if (organDolabi.dolapKapasitesi[i].childCount == 0)
                        {
                            // heldObject'i bu bo� dolaba yerle�tir
                            heldObject.transform.parent = organDolabi.dolapKapasitesi[i];
                            heldObject.transform.localPosition = organDolabi.dolapKapasitesi[i].localPosition; // Dolab�n merkezine yerle�tir, iste�e ba�l� olarak de�i�tirebilirsiniz
                            alanBos = true; // Bo� alan bulundu�unda bayrak true yap�l�r
                            // E�er bir dolaba yerle�tirdiysek, d�ng�y� sonland�rabiliriz
                            break;
                        }
                    }

                    // E�er t�m alanlar dolu ise
                    if (!alanBos)
                    {
                        Debug.Log("Alan dolu"); // Uygun bir hata mesaj� g�sterilir
                        //buraya dolap dolu bildirimi yap�p setactive true yapt�r
                        heldObject.transform.SetParent(null);
                    }

                    heldObject = null;



                }

                if (heldObject == null && Input.GetKeyDown(KeyCode.E)) // dolaptan alma
                {
                    // dolapKapasitesi listesindeki her bir eleman� d�ng�ye alarak i�lem yapal�m
                    foreach (Transform dolap in organDolabi.dolapKapasitesi)
                    {
                        // Dolab�n her bir child'�n� d�ng�ye alarak i�lem yapal�m
                        foreach (Transform childTransform in dolap)
                        {
                            GameObject child = childTransform.gameObject; // Transform'u GameObject'e �eviriyoruz
                            // �ocuk objenin tag'ini kontrol edelim
                            if (child.CompareTag("kalp"))
                            {
                                heldObject = child; // child'i heldObject'e at�yoruz
                                heldObject.transform.SetParent(hand);
                                heldObject.transform.localPosition = Vector3.zero;
                                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                                heldObject.GetComponent<Rigidbody>().useGravity = false;
                                heldObject.GetComponent<Collider>().enabled = false;

                                Debug.Log("Kalp objesi collider'dan ��kt�!");

                                // Collider i�inden ��kan obje "kalp" tag'ine sahip ise listeden ��kar
                                organDolabi.kalplerListesi.Remove(child);

                                // Liste boyutunu kontrol et
                                Debug.Log("Kalpler listesinin boyutu: " + organDolabi.kalplerListesi.Count);

                                return; // Do�ru objeyi buldu�umuzda d�ng�y� sonland�r�yoruz
                            }
                        }
                    }
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

        if (Input.GetKeyUp(KeyCode.Mouse0) && heldObject.CompareTag("sedye"))
        {
            // Yerle�tirme kodu
            heldObject.transform.position = hit.point; // Objeyi raycast'�n �arpt��� yere yerle�tir
            heldObject.GetComponent<Rigidbody>().isKinematic = true; // Objeyi fiziksel etkile�ime a�
            heldObject.GetComponent<Rigidbody>().useGravity = true; // Objeyi fiziksel etkile�ime a�
            heldObject.GetComponent<Collider>().enabled = true; // Objeyi �arp��malara a�
            heldObject.transform.SetParent(null);
            heldObject = null; // Elindeki objeyi s�f�rla
        }
       /* if (heldObject.CompareTag("sedye"))
        {
            // Yerle�tirme kodu
            Vector3 placementPosition = hit.point;

            // �ki collider�n y�ksekliklerini al
            float raycastHitHeight = hit.point.y;
            float colliderHeight = hit.collider.bounds.max.y;

            // Y�ksekli�i ayarla
            float finalHeight = Mathf.Max(raycastHitHeight, colliderHeight); // Y�kseklikleri kar��la�t�r ve b�y�k olan� se�

            // Objeyi yerle�tir
            placementPosition.y = finalHeight; // Y�ksekli�i ayarla
            heldObject.transform.position = placementPosition; // Objeyi raycast'�n �arpt��� yere yerle�tir
            heldObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Objeyi d�nd�rme
        }*/
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

    public void Deepweb()
    {
        buy.SetActive(false);
        sell.SetActive(true);
    }

    public void Toptanci()
    {
        sell.SetActive(false);
        buy.SetActive(true);
    }

    public void Ilan_ver()
    {
        ilanVer.SetActive(true);
        
    }

    public void Ilanlar�m()
    {
      
        ilanVer.SetActive(false);
    }

    
}
