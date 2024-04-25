using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
   

    public NavMeshAgent agent;

    private Economy economy;

    private Animator npcAnimator;
    // Start is called before the first frame update
    void Awake()
    {
      
        
        economy = FindObjectOfType<Economy>();
        npcAnimator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        StartWalking();
        StopWalking();
    }


    public void StartWalking()
    {

        if (economy.hastaKayitSirasiList == null)
        {
            Debug.Log("StartWalking: hastaKayitSirasiList null");
            return;
        }

        if (economy.sedyeSayisi.Count > 0)
        {

            // Bir hedef bulup bulmadýðýný belirten bir bayrak
            bool hedefBulundu = false;
            Debug.Log(economy.hastaKayitSirasiList.Count);
            // Hasta kayit sirasi listesindeki her indeksi kontrol et
            for (int i = 0; i < economy.hastaKayitSirasiList.Count; i++)
            {
                if ((int)this.gameObject.transform.position.x == (int)economy.hastaKayitSirasiList[i].transform.position.x)
                {
                    if (npcAnimator != null)
                    {
                        // "idle" durumuna geçmek için "isWalking" parametresini false olarak ayarlayýn
                        npcAnimator.SetBool("isWalking", false);
                    }
                }
                Transform hedef = economy.hastaKayitSirasiList[i];

                Debug.Log($"StartWalking: Kontrol edilen hedef {i}. Ýndeks: {hedef.position}");

                // Hedefin mevcut etiketini kontrol et
                if (hedef.gameObject.tag != "dolu")
                {
                    // Hedef uygun ise, NavMeshAgent'ýn hedefini bu pozisyona ayarla
                    agent.SetDestination(hedef.position);
                    Debug.Log($"StartWalking: Hedef belirleniyor. Hedef pozisyonu: {hedef.position}");

                    // Hedefin etiketini "dolu" olarak deðiþtir
                    hedef.gameObject.tag = "dolu";
                    Debug.Log($"StartWalking: Hedefin etiketini 'dolu' olarak deðiþtirildi: {hedef.gameObject.tag}");

                    // NPC'nin yürümeye baþladýðýný belirtmek için animatörü güncelle
                    if (npcAnimator != null)
                    {
                        npcAnimator.SetBool("isWalking", true);
                        Debug.Log("StartWalking: npcAnimator isWalking true olarak ayarlandý");
                    }

                    hedefBulundu = true; // Hedef bulunduðunu belirten bayraðý ayarla
                    break; // Döngüden çýk, hedefi bulduðumuz için daha fazla kontrol etmeye gerek yok
                }
                else
                {
                    Debug.Log($"StartWalking: Hedef {i}. Ýndeks 'dolu' etiketine sahip, atlanýyor.");
                }
            }

            // Eðer hiçbir uygun hedef bulunamadýysa, durdur
            /*if (!hedefBulundu)
            {
                agent.ResetPath(); // NavMeshAgent'ý durdur
                Debug.Log("StartWalking: Uygun hedef bulunamadý, agent durduruldu");

                if (npcAnimator != null)
                {
                    npcAnimator.SetBool("isWalking", false);
                    Debug.Log("StartWalking: npcAnimator isWalking false olarak ayarlandý");
                }
            }*/
        }
        
    }



    // Karakteri durdurmak için çaðrýlabilecek bir metod
    public void StopWalking()
    {
        
        
    }

}
