using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorControl : MonoBehaviour
{
    Animator animation;
    AnimatorStateInfo animatorStateInfo;
    //n是随机数，用于随机动作
    int n;
    //state表示角色当前的状态
    // 1: 站立
    // 2: 等待战斗
    // 3：跑步
    // 4. 打架

    int state = 1;
    public GameObject effect;
    public Transform effectPosition;
    bool StartEffect = false;
    bool hasEnemy = false;
    float fireCountdown;

    public Transform enemyPosition;
    public float range = 2f;
    public string enemyTag = "enemy";
    public UnityEngine.AI.NavMeshAgent  myAgent;
    public LayerMask ground ;

    Vector3 dir;
    Quaternion lookRotation;
    void Start()
    {
        animation = GetComponent<Animator>();
        // InvokeRepeating("GetRandom",0f,1f);
        InvokeRepeating("UpdateEnemy",0f,0.5f);
        // myAgent = GetComponent<UnitMovement>().myAgent
        
        // Debug.Log(animatorStateInfo);
    }

    void Update()
    {

        if(myAgent.remainingDistance< 0.5f)
        {
            animation.SetBool("IsRuning", false);
        }

        // animatorStateInfo = animation.GetCurrentAnimatorStateInfo(0);
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
            {
                myAgent.SetDestination(hit.point);
               
                
            }
             animation.SetBool("IsRuning", true);

        }
       
           //移动转向
        if(animation.GetBool("IsRuning"))
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


        if(enemyPosition != null  && fireCountdown <= 0f)
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


       

            // target = new Vector3(hit.point.x,transform.position.y, hit.point.z) - transform.position;

            // lookRotation = Quaternion.LookRotation(target);

            
// Debug.Log(state);
            // if(state!= 4&& Vector3.Distance(transform.forward, target.normalized) > 0.001f)
            // { 

            //     Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*25).eulerAngles;
            //     transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            // }
            // else 
            // {
                //跑起来

//                  animation.SetBool("Stand",false);
//                     animation.SetBool("IsRun",true);
//                     animation.SetBool("FightWait",false);
//                     state =3;
//             // } 
//         // Debug.Log(myAgent.destination + " "+ transform.position);
//         if(!animation.GetBool("Stand") &&myAgent.remainingDistance < 0.5f   )
//         {
//             //站立
//             // myAgent.SetDestination(transform.position);
//             animation.SetBool("IsRun",false);
//             animation.SetBool("Stand",true);
//             state = 1;
//         }
// // Debug.Log(animatorStateInfo.normalizedTime );
//         if(enemyPosition != null  && !animation.GetBool("IsRun"))
//         {
//             hasEnemy = true;
//             state = 4;
//             fight();
//         }
//         else
//         {
//             hasEnemy = false;
//             StartEffect = false;
//         }
//         // if(state == 4 &&enemyPosition != null)
//         // {
//         // Vector3 dir = enemyPosition.position - transform.position;
//         // Quaternion lookRotation = Quaternion.LookRotation(dir);
//         // Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
//         // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

//         // }

//     }

    void fight()
    { 
         if(!animation.GetBool("IsRuning") && Vector3.Distance(transform.forward, (enemyPosition.position-transform.position).normalized) > 0.1f)
            transform.LookAt(new Vector3(enemyPosition.position.x,transform.position.y,enemyPosition.position.z));
            n++;
        animation.SetInteger("Attack",(n%3)+1);
       
       

        // if(animatorStateInfo.IsName("Attack3")|| animatorStateInfo.IsName("Attack2"))
        // {
        //     if(animatorStateInfo.normalizedTime >= 0.41f && animatorStateInfo.normalizedTime <= 0.43f)
        //     {GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation);
        //     Destroy(effect1, 0.5f);
        //     }
        // }
        // // else if (animatorStateInfo.IsName("Attack03"))
        // // {
        // //     if(animatorStateInfo.normalizedTime >= 0.21f && animatorStateInfo.normalizedTime <= 0.23f)
        // //     {GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation);
        // //     Destroy(effect1, 0.5f);
        // //     }
        // // }
        // else if(animatorStateInfo.normalizedTime >= 0.33f && animatorStateInfo.normalizedTime <= 0.35f)
        // {
        //     GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation);
        // Destroy(effect1, 0.5f);
        // }
       
        
    }

    void ChopEffect()
    {
        GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation);
            Destroy(effect1, 0.5f);
    }




    // void GetRandom()
    // {  
    //   n = Random.Range(1,4);
    // //   if(hasEnemy&& state!= 3)
    // //   {
    // //       transform.LookAt(enemyPosition);
    // //   }


    // }
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
