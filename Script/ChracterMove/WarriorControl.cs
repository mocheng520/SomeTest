using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnitMovement))]
public class WarriorControl : MonoBehaviour
{
    Animator animation;
    AnimatorStateInfo animatorStateInfo;
    //n是随机数，用于随机动作
    int n;
    UnitMovement movement;
    [Header("Attribute")]
    public float range = 2f;
    public float damage = 10f;
    public float fireCountdown;
    [Header("Effect")]
    public GameObject effect;
    public Transform effectPosition;
    [Header("Others")]
    public Transform autoEnemy;
    public Transform enemy;
    public LayerMask ground ;
    public string enemyTag = "enemy";
    

    
    
    
     UnityEngine.AI.NavMeshAgent  myAgent;
    

    Vector3 dir;
    Quaternion lookRotation;
    void Start()
    {
        animation = GetComponent<Animator>();
        // InvokeRepeating("GetRandom",0f,1f);
        InvokeRepeating("UpdateEnemy",0f,0.5f);
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        movement = GetComponent<UnitMovement>();
        
        // Debug.Log(animatorStateInfo);
    }

    void Update()
    {

        

        // animatorStateInfo = animation.GetCurrentAnimatorStateInfo(0);
        // if(Input.GetMouseButtonDown(1))
        // {
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
        //     {
        //         myAgent.SetDestination(hit.point);
               
                
        //     }
        //      animation.SetBool("IsRuning", true);

        // }
        if(animation.GetBool("IsDead")) return ;
           //移动转向
        if(animation.GetBool("IsRuning"))
        {

            // transform.LookAt(new Vector3(enemyPosition.position.x,transform.position.y,enemyPosition.position.z));
            SmoothFaceToTarget(myAgent.steeringTarget);
            // dir.x  = myAgent.steeringTarget.x;
            // dir.y = transform.position.y;
            // dir.z = myAgent.steeringTarget.z;
            // dir = dir - transform.position;
            // // Vector3 dir = rotition - transform.position;
            // Quaternion lookRotation = Quaternion.LookRotation(dir);
            // Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        //加入在战斗，使角色始终面对怪物
        if(!animation.GetBool("IsRuning") && animation.GetInteger("Attack") > 0 && enemy != null)
        {
            SmoothFaceToTarget(enemy.position);
        }
        
        if(fireCountdown <= 0f)
        {
            // Debug.Log(movement.focus);
            if(movement.focus != null)
            {
                Fight(movement.focus);
                fireCountdown = 1;
            }
            else if(autoEnemy != null)
            {
                Fight(autoEnemy);
                fireCountdown = 1;
            }
            else 
                animation.SetInteger("Attack", 0);
        }
        fireCountdown -= Time.deltaTime;
        // if(movement.focus != null)
        // {
        //     FightFocus(movement.focus);
        // }
        // else if(movement.focus==null&&enemyPosition != null  && fireCountdown <= 0f)
        // {
        //      Autofight(enemyPosition);
        //      fireCountdown = 1;
        // }
        // else if (enemyPosition == null)
        // {
        //  animation.SetInteger("Attack",0);
        // }
        // fireCountdown -= Time.deltaTime;

    }

    // void FightFocus(Transform enemy)
    // {
    //     enemy.GetComponent<Health>().TakeDamage(damage);
    // }
    // public void StartRun()
    // {
    //     animation.SetBool("IsRuning", true);
    // }


       

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
    void SmoothFaceToTarget(Vector3 target)
    {
            dir.x  = target.x;
            dir.y = transform.position.y;
            dir.z = target.z;
            dir = dir - transform.position;
            // Vector3 dir = rotition - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    void Fight(Transform _enemy)
    { 
        enemy = _enemy;
        // if(!animation.GetBool("IsRuning") && Vector3.Distance(transform.forward, (enemy.position-transform.position).normalized) > 0.1f)
        //     transform.LookAt(new Vector3(enemy.position.x,transform.position.y,enemy.position.z));
            n++;
        animation.SetInteger("Attack",(n%3)+1);
       

        
    }

    void ChopEffect()
    {
        //把伤害传递写在特效这里
        if(enemy != null)
            enemy.GetComponent<Health>().TakeDamage(damage);
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
        autoEnemy = null;

        if(nearestEnemy != null && shortestDistance <= range)
        {
            autoEnemy = nearestEnemy.transform;
        }

    }
}
