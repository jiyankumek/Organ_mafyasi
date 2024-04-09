using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC : MonoBehaviour
{
    public float raycastLength = 5f; // Raycast ýþýnýnýn uzunluðu

    public Canvas PcCanvas; // Açýlacak olan canvas
    public GameObject sell;
    public GameObject buy;
    public GameObject ilanlarým;
    public GameObject ilanVer;
    public GameObject teklifPrefab;
    

    public bool pcCanvasisTrue;

    public GameObject sedye;
    public GameObject kesilecekYer;
    public GameObject kalp;
    public GameObject heldObject; // Elinde tutulan objeyi tutmak için bir GameObject referansý
    public GameObject Organ_satis;
    

   
    public Transform hand;
    public Transform organdolabi;
    public Transform content_satis;
    public Transform contentIlanlarým;
    

    public Vector3 organKonumu;
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
        else // Canvas kapalýyken
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
        // Karakterden ileri doðru bir raycast ýþýný çýkar
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

                if (hit.collider.CompareTag("obje") &&
                    heldObject == null) // Eðer 'obje'ye basýldýysa ve elinde bir obje yoksa
                {
                    Debug.Log("nester ");
                    heldObject = hit.collider.gameObject;
                    heldObject.transform.SetParent(hand);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Collider>().enabled = false;
                }

                if (hit.collider.CompareTag("kalp") && heldObject == null) // Eðer 'obje'ye basýldýysa ve elinde bir obje yoksa
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
            if(hit.collider.CompareTag("dolap"))
            {
                if (Input.GetMouseButton(0)&&heldObject.CompareTag("kalp"))
                {
                    heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    heldObject.GetComponent<Rigidbody>().useGravity = true;
                    heldObject.GetComponent<Collider>().enabled = true;
                    heldObject.transform.SetParent(organdolabi);
                    heldObject.transform.localPosition = organKonumu;
                    heldObject = null;
                    
                }

                if (heldObject == null && Input.GetKeyDown(KeyCode.E))//dolaba koyma
                {
                    // organdolabi'nin çocuk objelerini döngüye alarak iþlem yapalým
                    foreach (Transform childTransform in organdolabi.transform)
                    {
                        GameObject child = childTransform.gameObject; // Transform'u GameObject'e çeviriyoruz
                        // Çocuk objenin tag'ini kontrol edelim
                        if (child.CompareTag("kalp"))
                        {
                            heldObject = child; // child'i heldObject'e atýyoruz
                            heldObject.transform.SetParent(hand);
                            heldObject.transform.localPosition = Vector3.zero;
                            heldObject.GetComponent<Rigidbody>().isKinematic = true;
                            heldObject.GetComponent<Rigidbody>().useGravity = false;
                            heldObject.GetComponent<Collider>().enabled = false;

                            
                            Debug.Log("Kalp objesi collider'dan çýktý!");

                            // Collider içinden çýkan obje "kalp" tag'ine sahip ise listeden çýkar
                            organDolabi.kalplerListesi.Remove(child);

                            // Liste boyutunu kontrol et
                            Debug.Log("Kalpler listesinin boyutu: " + organDolabi.kalplerListesi.Count);

                            break;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && heldObject != null) // Eðer 'E' tuþu býrakýldýysa ve elinde bir obje varsa 
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

    public void Ilanlarým()
    {
      
        ilanVer.SetActive(false);
    }

    
}
