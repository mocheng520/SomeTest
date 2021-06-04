using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    public float health = 100f;
    public Image healthBar;
    public GameObject DieEffect ;
    public GameObject BornEffect ;
    

    void Start()
    {
        GameObject bornEffectGO = (GameObject)Instantiate(BornEffect,transform.position,transform.rotation);
        Destroy(bornEffectGO, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage )
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;

        if(health <= 0f)
        {
            die();
        }
    }

    void die()
    {
        GetComponent<Animator>().SetBool("IsDead", true);
        GameObject dieEffectGO = (GameObject)Instantiate(DieEffect,transform.position,transform.rotation);
        Destroy(dieEffectGO, 2f);
        Destroy(gameObject, 2f);
    }
}
