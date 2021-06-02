using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SieldManControl : MonoBehaviour
{
    public float speed;
    public float range;
    Animator animation;
    UnityEngine.AI.NavMeshAgent myAgent;
    public LayerMask ground ;

    public Transform enemyPosition;
    public float fireCountdown = 1f;
    int n = 1;
    [Header("Attack Effect")]
    public GameObject effect;
    public Transform effectPosition;
    
    [Header("enemyTag")]
    public string enemyTag = "enemy";

    //用于走路时开始的转向
    Vector3 dir;



    void Start()
    {
        animation = GetComponent<Animator>();
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //   InvokeRepeating("GetRandom",0f,1f);
        InvokeRepeating("UpdateEnemy",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
         if(myAgent.remainingDistance< 0.2f)
        {
            animation.SetBool("IsWalking", false);
            // animation.SetBool("IsWalking", false);
        }
         if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
            {
                myAgent.SetDestination(hit.point);
               
                animation.SetBool("IsWalking", true);
            }
             
        // StartCoroutine(StandOrNot()); 
        }
       //移动转向
        if(animation.GetBool("IsWalking"))
        {

            // transform.LookAt(new Vector3(enemyPosition.position.x,transform.position.y,enemyPosition.position.z));
            dir.x  = myAgent.steeringTarget.x;
            dir.y = transform.position.y;
            dir.z = myAgent.steeringTarget.z;
            dir = dir - transform.position;
            // Vector3 dir = rotition - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }




            if(enemyPosition != null && fireCountdown <= 0f)
        {
            fight();
            fireCountdown = 1;
        }
        else if (enemyPosition == null)
        {
        animation.SetInteger("Attack",0);
        }

         fireCountdown -= Time.deltaTime;
        
    
}

    void fight()
    {
         if(!animation.GetBool("IsWalking") && Vector3.Distance(transform.forward, (enemyPosition.position-transform.position).normalized) > 0.1f)
            transform.LookAt(new Vector3(enemyPosition.position.x,transform.position.y,enemyPosition.position.z));
        n = 3-n;

        animation.SetInteger("Attack",n);
    }



    void AttackEffect()
    {
        GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation);
            Destroy(effect1, 0.5f);
    }



         void UpdateEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);


        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(shortestDistance > range)
        enemyPosition = null;

        if(nearestEnemy != null && shortestDistance <= range)
        {
            enemyPosition = nearestEnemy.transform;
        }

    }

}
