using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(Unit))]
public class SpearControl : MonoBehaviour
{



    
    int n;
    public bool hasInstruction = false;
    public Vector3 InstructionPosition;
    UnitMovement movement;
    Unit unit;
    [Header("Attribute")]
    public float findRange = 10f;
    public float damage = 10f;
    public float fireCountdown;
    [Header("Effect")]
    public GameObject effect;
    public Transform effectPosition;
    [Header("Others")]
    public Transform autoEnemy;
    public Transform enemy;
    public LayerMask ground;
    public string enemyTag = "enemy";
    


    Animator animation;
    // NavMeshAgent  myAgent;
    Vector3 dir;
    void Start()
    {
        animation = GetComponent<Animator>();
        // myAgent = GetComponent<NavMeshAgent>();
        movement = GetComponent<UnitMovement>();
        InvokeRepeating("UpdateEnemy",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if(animation.GetFloat("RunningSpeed")<0.02f && animation.GetInteger("Attack") > 0)
        {
            if(movement.focus != null)
                SmoothFaceToTarget(movement.focus.position);
            else if(autoEnemy != null)
                SmoothFaceToTarget(autoEnemy.position);
        }


        if(movement.focus == null)
        {
            if(Vector3.Distance(transform.position, InstructionPosition) < 0.1f)
                hasInstruction = false;
        }
        else{
                if(Vector3.Distance(transform.position,  movement.focus.position + (transform.position - movement.focus.position).normalized * (movement.radius + movement.attackRange)) < 0.1f)
                hasInstruction = false;
        }
        
        //     hasInstruction = false;
        // else
        //     hasInstruction = true;




        if(!hasInstruction && autoEnemy != null)
        {
            Debug.Log(Time.deltaTime+" "+1);
            // Vector3 border = hit.transform.position + (transform.position - hit.transform.position).normalized * (radius + attackRange)  ;
            movement.MoveToBorder(autoEnemy.transform.position, transform.position, 0.5f, 2f);
        }

        //先判断选中的目标是什么类型：是敌人则攻击，是食物则获取，这里先默认是敌人，等会回来改
        if(fireCountdown <= 0f)
        {
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
            {
                animation.SetInteger("Attack", 0);
                // enemy = null;
            }
        }
        fireCountdown -= Time.deltaTime;
        
    }

    void SmoothFaceToTarget(Vector3 target)
    {
            dir.x  = target.x;
            dir.y = transform.position.y;
            dir.z = target.z;
            dir = dir - transform.position;
            // Vector3 dir = rotition - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Fight(Transform _enemy)
    { 
        // enemy = _enemy;
        n++;
        if(animation.GetFloat("RunningSpeed") > 0.1f && movement.focus != null && Vector3.Distance(movement.focus.position, transform.position)>=4f && Vector3.Distance(movement.focus.position, transform.position)<=5f )
            animation.SetBool("RunToStab",true);
        else
        {
            animation.SetInteger("Attack",(n%2)+1);    
            animation.SetBool("RunToStab",false);
        }
            
    }

    void Effect()
    {
        //  if(enemy != null)
        //     enemy.GetComponent<Health>().TakeDamage(damage);
        GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation * transform.rotation);
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
        if(shortestDistance > findRange)
            autoEnemy = null;

        if(nearestEnemy != null && shortestDistance <= findRange)
        {
            autoEnemy = nearestEnemy.transform;
        }

    }

}
