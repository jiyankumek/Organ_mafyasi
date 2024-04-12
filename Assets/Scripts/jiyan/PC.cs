using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC : MonoBehaviour
{
    public float raycastLength = 2.5f; // Raycast ýþýnýnýn uzunluðu

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
    public Transform objeHand;
    public Transform content_satis;
    public Transform contentIlanlarým;
    

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

                if (hit.collider.CompareTag("sedye") && heldObject == null)
                {
                    heldObject = hit.collider.gameObject;
                    heldObject.transform.SetParent(objeHand);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Sýfýr dönüþ açýlarýna ayarla
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Collider>().enabled = true;
                }

                if (hit.collider.CompareTag("obje") && heldObject == null) // Eðer 'obje'ye basýldýysa ve elinde bir obje yoksa
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
                    Debug.Log("kesildi ve düþtü");
                    
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


                    bool alanBos = false; // Boþ alanýn bulunup bulunmadýðýný kontrol etmek için bir bayrak

                    for (int i = 0; i < organDolabi.dolapKapasitesi.Count; i++)
                    {
                        // Eðer dolapKapasitesi listesindeki i. elemanýn altýnda hiç çocuk yoksa (yani içi boþsa)
                        if (organDolabi.dolapKapasitesi[i].childCount == 0)
                        {
                            // heldObject'i bu boþ dolaba yerleþtir
                            heldObject.transform.parent = organDolabi.dolapKapasitesi[i];
                            heldObject.transform.localPosition = organDolabi.dolapKapasitesi[i].localPosition; // Dolabýn merkezine yerleþtir, isteðe baðlý olarak deðiþtirebilirsiniz
                            alanBos = true; // Boþ alan bulunduðunda bayrak true yapýlýr
                            // Eðer bir dolaba yerleþtirdiysek, döngüyü sonlandýrabiliriz
                            break;
                        }
                    }

                    // Eðer tüm alanlar dolu ise
                    if (!alanBos)
                    {
                        Debug.Log("Alan dolu"); // Uygun bir hata mesajý gösterilir
                        //buraya dolap dolu bildirimi yapýp setactive true yaptýr
                        heldObject.transform.SetParent(null);
                    }

                    heldObject = null;



                }

                if (heldObject == null && Input.GetKeyDown(KeyCode.E)) // dolaptan alma
                {
                    // dolapKapasitesi listesindeki her bir elemaný döngüye alarak iþlem yapalým
                    foreach (Transform dolap in organDolabi.dolapKapasitesi)
                    {
                        // Dolabýn her bir child'ýný döngüye alarak iþlem yapalým
                        foreach (Transform childTransform in dolap)
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

                                return; // Doðru objeyi bulduðumuzda döngüyü sonlandýrýyoruz
                            }
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

        if (Input.GetKeyUp(KeyCode.Mouse0) && heldObject.CompareTag("sedye"))
        {
            // Yerleþtirme kodu
            heldObject.transform.position = hit.point; // Objeyi raycast'ýn çarptýðý yere yerleþtir
            heldObject.GetComponent<Rigidbody>().isKinematic = true; // Objeyi fiziksel etkileþime aç
            heldObject.GetComponent<Rigidbody>().useGravity = true; // Objeyi fiziksel etkileþime aç
            heldObject.GetComponent<Collider>().enabled = true; // Objeyi çarpýþmalara aç
            heldObject.transform.SetParent(null);
            heldObject = null; // Elindeki objeyi sýfýrla
        }
       /* if (heldObject.CompareTag("sedye"))
        {
            // Yerleþtirme kodu
            Vector3 placementPosition = hit.point;

            // Ýki colliderýn yüksekliklerini al
            float raycastHitHeight = hit.point.y;
            float colliderHeight = hit.collider.bounds.max.y;

            // Yüksekliði ayarla
            float finalHeight = Mathf.Max(raycastHitHeight, colliderHeight); // Yükseklikleri karþýlaþtýr ve büyük olaný seç

            // Objeyi yerleþtir
            placementPosition.y = finalHeight; // Yüksekliði ayarla
            heldObject.transform.position = placementPosition; // Objeyi raycast'ýn çarptýðý yere yerleþtir
            heldObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Objeyi döndürme
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

    public void Ilanlarým()
    {
      
        ilanVer.SetActive(false);
    }

    
}
