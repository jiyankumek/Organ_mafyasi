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

            // Bir hedef bulup bulmad���n� belirten bir bayrak
            bool hedefBulundu = false;
            Debug.Log(economy.hastaKayitSirasiList.Count);
            // Hasta kayit sirasi listesindeki her indeksi kontrol et
            for (int i = 0; i < economy.hastaKayitSirasiList.Count; i++)
            {
                if ((int)this.gameObject.transform.position.x == (int)economy.hastaKayitSirasiList[i].transform.position.x)
                {
                    if (npcAnimator != null)
                    {
                        // "idle" durumuna ge�mek i�in "isWalking" parametresini false olarak ayarlay�n
                        npcAnimator.SetBool("isWalking", false);
                    }
                }
                Transform hedef = economy.hastaKayitSirasiList[i];

                Debug.Log($"StartWalking: Kontrol edilen hedef {i}. �ndeks: {hedef.position}");

                // Hedefin mevcut etiketini kontrol et
                if (hedef.gameObject.tag != "dolu")
                {
                    // Hedef uygun ise, NavMeshAgent'�n hedefini bu pozisyona ayarla
                    agent.SetDestination(hedef.position);
                    Debug.Log($"StartWalking: Hedef belirleniyor. Hedef pozisyonu: {hedef.position}");

                    // Hedefin etiketini "dolu" olarak de�i�tir
                    hedef.gameObject.tag = "dolu";
                    Debug.Log($"StartWalking: Hedefin etiketini 'dolu' olarak de�i�tirildi: {hedef.gameObject.tag}");

                    // NPC'nin y�r�meye ba�lad���n� belirtmek i�in animat�r� g�ncelle
                    if (npcAnimator != null)
                    {
                        npcAnimator.SetBool("isWalking", true);
                        Debug.Log("StartWalking: npcAnimator isWalking true olarak ayarland�");
                    }

                    hedefBulundu = true; // Hedef bulundu�unu belirten bayra�� ayarla
                    break; // D�ng�den ��k, hedefi buldu�umuz i�in daha fazla kontrol etmeye gerek yok
                }
                else
                {
                    Debug.Log($"StartWalking: Hedef {i}. �ndeks 'dolu' etiketine sahip, atlan�yor.");
                }
            }

            // E�er hi�bir uygun hedef bulunamad�ysa, durdur
            /*if (!hedefBulundu)
            {
                agent.ResetPath(); // NavMeshAgent'� durdur
                Debug.Log("StartWalking: Uygun hedef bulunamad�, agent durduruldu");

                if (npcAnimator != null)
                {
                    npcAnimator.SetBool("isWalking", false);
                    Debug.Log("StartWalking: npcAnimator isWalking false olarak ayarland�");
                }
            }*/
        }
        
    }



    // Karakteri durdurmak i�in �a�r�labilecek bir metod
    public void StopWalking()
    {
        
        
    }

}
