using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinControl : MonoBehaviour
{
      public float LongAttackRange;
      public float MeleeAttackRange;
      public float fightcount;
      public string heroTag = "hero";
    Animator animation;
    UnityEngine.AI.NavMeshAgent myAgent;
    public Transform Hero;
    public GameObject fireStickPrefab;
    public Transform ThrowPosition;

    void Start()
    {
                animation = GetComponent<Animator>();
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("UpdateHero",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
         if(myAgent.remainingDistance< 0.5f)
        {
            animation.SetBool("IsRuning", false);
        }
        if(Hero == null) return ;
        if(Vector3.Distance(Hero.transform.position, transform.position) > LongAttackRange )
        {
            Move();
        }
        else if (Vector3.Distance(Hero.transform.position, transform.position) > MeleeAttackRange && fightcount <= 0f)
        {
            LongAttack();
            
        }
        else if (fightcount <= 0f)
        {
            MeleeAttack();
        }
        fightcount -= Time.deltaTime;
    }

      void Move()
    {
        Vector3 destination = Hero.position + (transform.position - Hero.position).normalized * LongAttackRange* 0.7f;
        myAgent.SetDestination(destination);
        animation.SetBool("IsThrowing", false);
        animation.SetBool("IsAttack", false);
        animation.SetBool("IsRuning", true);
    }
    void LongAttack()
    {
        animation.SetBool("IsRuning", false);
        animation.SetBool("IsThrowing", true);
        fightcount = 2f;
    }
    void MeleeAttack()
    {
        animation.SetBool("IsRuning", false);
        animation.SetBool("IsAttack", true);

        fightcount = 1.5f;
    }

    void ThrowFireStick()
    {
         GameObject fireStickGo = (GameObject)Instantiate(fireStickPrefab,ThrowPosition.position,transform.rotation);
        FireStick  fireStick = fireStickGo.GetComponent<FireStick>();
        if(fireStick != null)
            {
                
                fireStick.Seek(Hero);
            }
    }







       void UpdateHero()
    {
        GameObject[] heros = GameObject.FindGameObjectsWithTag(heroTag);


        float shortestDistance = Mathf.Infinity;
        GameObject nearestHero = null;

        foreach (GameObject hero in heros)
        {
            float distanceToHero = Vector3.Distance(transform.position, hero.transform.position);
            if (distanceToHero < shortestDistance)
            {
                shortestDistance = distanceToHero;
                nearestHero = hero;
            }
        }
        if(shortestDistance > LongAttackRange)
            nearestHero = null;

        if(nearestHero != null )
        {
            Hero = nearestHero.transform;
        }

    }


}
