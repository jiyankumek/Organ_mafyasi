using System.Collections;
using System.Collections.Generic;
using Knife.RealBlood.SimpleController;
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
    private BloodFX kaneffect;


    void Start()
    {
        economy= FindObjectOfType<Economy>();
        organDolabi = FindObjectOfType<Organ_dolabi>();
        pcCanvasisTrue = false;
        kaneffect = GetComponent<BloodFX>();
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
                    heldObject.transform.rotation = Quaternion.Euler(0, heldObject.transform.rotation.eulerAngles.y, 0);
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Rigidbody>().drag=0;
                    
                }

                if (hit.collider.CompareTag("nester") && heldObject == null) // Eðer 'obje'ye basýldýysa ve elinde bir obje yoksa
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
                
                if (heldObject != null && Input.GetMouseButton(0)&&heldObject.CompareTag("nester"))
                {
                    kesilecekYer.SetActive(false);
                    kaneffect.PlayFX(null);
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

        // Eðer elinizde sedye objesi varsa
        if (heldObject != null && heldObject.CompareTag("sedye"))
        {
            // Her karede objenin açýsýný sýfýrlayýn
           // heldObject.transform.rotation = Quaternion.Euler(0, 0, 0);


            // Elinizdeki objeyi oyuncunun önünde sabit bir mesafede tutun
            float distanceFromPlayer = 5.0f;
            Vector3 direction = transform.forward;
            heldObject.transform.position = transform.position + direction * distanceFromPlayer;

            // Çarpýþma kontrolü
            Collider heldCollider = heldObject.GetComponent<Collider>();
            Collider[] hitColliders = Physics.OverlapSphere(heldCollider.bounds.center, heldCollider.bounds.extents.magnitude);

            // `hitColliders` dizisini kontrol ediyoruz
            if (heldCollider != null && hitColliders != null)
            {
                bool isColliding = false;

                // Çarpýþan her bir nesneyi kontrol ediyoruz
                foreach (Collider collider in hitColliders)
                {
                    // Eðer çarpýþan nesne `heldCollider` deðilse ve `tag`'i 'zemin' veya 'Player' deðilse
                    if (collider != heldCollider && !collider.CompareTag("zemin") && !collider.CompareTag("Player"))
                    {
                        isColliding = true;
                        // Çarpýþan nesnenin adýný yazdýr
                        Debug.Log($"Çarpýþma var: Nesne adý - {collider.gameObject.name}");
                        break;
                    }
                }

                // Çarpýþma kontrolü sonucuna göre renk ayarlamasý yapýyoruz
                Renderer renderer = heldObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Çakýþma varsa renkleri kýrmýzý yapýyoruz
                    if (isColliding)
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            // Materyalin albedo (ana rengi) özelliðini kýrmýzý yapýyoruz
                            mat.color = Color.red;
                        }
                    }
                    // Çakýþma yoksa renkleri yeþil yapýyoruz
                    else
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            // Materyalin albedo (ana rengi) özelliðini yeþil yapýyoruz
                            mat.color = Color.green;
                        }
                    }
                }
            }

            // Sol fare tuþu býrakýldýðýnda yerleþtirme iþlemi
            if (Input.GetKeyUp(KeyCode.Mouse0) && heldObject != null)
            {
                // Çarpýþma kontrolü gerçekleþtirildi ve çakýþma yoksa
                bool isColliding = false;

                // `hitColliders` dizisini kontrol ediyoruz
                if (hitColliders != null)
                {
                    foreach (Collider collider in hitColliders)
                    {
                        // Eðer çarpýþan nesne `heldCollider` deðilse ve `tag`'i 'zemin' veya 'Player' deðilse
                        if (collider != heldCollider && !collider.CompareTag("zemin") && !collider.CompareTag("Player"))
                        {
                            isColliding = true;
                            break;
                        }
                    }
                }

                // Eðer çakýþma yoksa yerleþtirme iþlemini gerçekleþtir
                if (!isColliding)
                {
                    // Fiziksel ve çarpýþma özelliklerini etkinleþtir
                    Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.useGravity = true;
                        rb.freezeRotation = true;
                        rb.drag = 1;
                    }

                    Collider collider = heldObject.GetComponent<Collider>();
                    if (collider != null)
                    {
                        collider.enabled = true;
                    }

                    // Objeyi ebeveyninden ayýrýn
                    heldObject.transform.SetParent(null);

                    // Renkleri beyaza döndür
                    Renderer renderer = heldObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            // Materyalin albedo (ana rengi) özelliðini beyaz yapýyoruz
                            mat.color = Color.white;
                        }
                    }

                    // Elinizdeki objeyi sýfýrlayýn
                    heldObject = null;
                }
            }

            // Q ve E tuþlarý ile y açýsýný deðiþtirme
            float rotationSpeed = 50f; // Açýyý deðiþtirme hýzý
            if (Input.GetKey(KeyCode.Q))
            {
                // Y ekseninde açýyý azalt
                heldObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                // Y ekseninde açýyý artýr
                heldObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
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

        StartCoroutine(economy.SatisSuresiBelirle());
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
