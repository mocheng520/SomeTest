using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArMoveTest : MonoBehaviour
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

    public Transform enemyPosition;
    public float range = 2f;
    public string enemyTag = "enemy";
    public UnityEngine.AI.NavMeshAgent  myAgent;
    public LayerMask ground ;

    Vector3 target;
    Quaternion lookRotation;
    void Start()
    {
        animation = GetComponent<Animator>();
        InvokeRepeating("GetRandom",0f,1f);
        InvokeRepeating("UpdateEnemy",0f,0.5f);
        
        Debug.Log(animatorStateInfo);
    }

    void Update()
    {
        animatorStateInfo = animation.GetCurrentAnimatorStateInfo(0);
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
            {
                myAgent.SetDestination(hit.point);
                 //跑起来
            }

            target = new Vector3(hit.point.x,transform.position.y, hit.point.z) - transform.position;

            lookRotation = Quaternion.LookRotation(target);

            }
// Debug.Log(state);
            if(state!= 4&& Vector3.Distance(transform.forward, target.normalized) > 0.001f)
            { 

                Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*25).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
            else 
            {
                //跑起来
                //  animation.SetBool("Stand",false);
                    animation.SetBool("IsRun",true);
                    // animation.SetBool("FightWait",false);
                    // state =3;
            } 
        Debug.Log(myAgent.destination + " "+ transform.position);
        if(myAgent.remainingDistance < 0.5f   )
        {
            //站立
            // myAgent.SetDestination(transform.position);
            animation.SetBool("IsRun",false);
            // animation.SetBool("Stand",true);
            // state = 1;
        }
// Debug.Log(animatorStateInfo.normalizedTime );
        if(enemyPosition != null )
        {
            hasEnemy = true;
            // state = 4;
            fight();
        }
        else
        {
            hasEnemy = false;
            StartEffect = false;
        }
        // if(state == 4 &&enemyPosition != null)
        // {
        // Vector3 dir = enemyPosition.position - transform.position;
        // Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // }

    }

    void fight()
    {    
        // Debug.Log(transform.forward);
    // if(hasEnemy&& state!= 3)
    //   {
    //       transform.LookAt(enemyPosition);
    //   }
        // animation.SetBool("FightWait",true);
        animation.SetBool("Attack",true);
        // StartEffect = true;
        // stateInfo.IsName("shrink")
         if(animatorStateInfo.normalizedTime >= 0.33f && animatorStateInfo.normalizedTime <= 0.35f)
        {
            GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation);
        Destroy(effect1, 0.5f);
        }
       
        
    }




    void GetRandom()
    {  
      n = Random.Range(1,5);
      if(hasEnemy&& state!= 3)
      {
          transform.LookAt(enemyPosition);
      }


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
