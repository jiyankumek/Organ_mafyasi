using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private GameObject hastakayit;
    public NavMeshAgent agent;

    private Economy economy;

    private Animator npcAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        hastakayit =GameObject.FindWithTag("hastakayit");
        
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
        
        if (economy.sedyeSayisi.Count > 0)
        {
            agent.SetDestination(hastakayit.transform.position);
            if (npcAnimator != null)
            {
                Debug.Log("walk");
                // "walk" durumuna ge�mek i�in "isWalking" parametresini true olarak ayarlay�n
                npcAnimator.SetBool("isWalking", true);
            }
        }
    }

    // Karakteri durdurmak i�in �a�r�labilecek bir metod
    public void StopWalking()
    {
        if ((int)this.gameObject.transform.position.x == (int)hastakayit.transform.position.x)
        {
            if (npcAnimator != null)
            {
                // "idle" durumuna ge�mek i�in "isWalking" parametresini false olarak ayarlay�n
                npcAnimator.SetBool("isWalking", false);
            }
        }
        
    }

}
