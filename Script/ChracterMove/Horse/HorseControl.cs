using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorseControl : MonoBehaviour
{
    public GameObject knight;
    public KnightControl knightControl;
    

     int n;
    
    UnitMovement movement;
    Unit unit;
    [Header("Attribute")]
    public float damage = 10f;
    public float fireCountdown;
    [Header("Effect")]
    public GameObject effect;
    public Transform effectPosition;
    [Header("Others")]

    public LayerMask ground;
    public string enemyTag = "enemy";
    


    Animator animation;
    // NavMeshAgent  myAgent;
    Vector3 dir;
    void Start()
    {
        animation = GetComponent<Animator>();

        movement = GetComponent<UnitMovement>();
        unit = GetComponent<Unit>();
        knightControl = knight.GetComponent<KnightControl>(); 

    }

    // Update is called once per frame
    void Update()
    {
         
        //转向到目标点，
        if(animation.GetFloat("RunningSpeed")<0.02f && animation.GetInteger("Attack") > 0)
        {
            if(movement.focus != null)
                unit.SmoothFaceToTarget(movement.focus.position);
            else if(unit.autoEnemy != null)
                unit.SmoothFaceToTarget(unit.autoEnemy.position);
        }

       

         //移动指令
        if(movement.focus == null)
        {
            if(Vector3.Distance(transform.position, movement.InstructionPosition) < 0.1f)
                movement.hasInstruction = false;
        }
        else{//锁定目标指令，指令完成后，因为focus!=null，因此不会自动寻找敌人
                if(movement.ArriveBorder(movement.focus.position))
                {
                    // Debug.Log(hasInstruction);
                     movement.hasInstruction = false;
                }                        
        }

        //自动目标，在没有指令，且没有focus的情况下执行
        if(!movement.hasInstruction && movement.focus == null &&unit.autoEnemy != null)
        {
            movement.radius = unit.autoEnemy.GetComponent<CapsuleCollider>().radius;
            movement.MoveToBorder(unit.autoEnemy.transform.position,  movement.radius, movement.attackRange);
        }

          //先判断选中的目标是什么类型：是敌人则攻击，是食物则获取，这里先默认是敌人，等会回来改
        //移动到目标附近，再转化到战斗状态
    
        if(fireCountdown < 0)
        {
            if(movement.focus != null && movement.ArriveBorder(movement.focus.position))
            {
                
            
                    Fight(movement.focus);
                    fireCountdown = 1;
                
            }
            else if(  unit.autoEnemy != null && movement.ArriveBorder(unit.autoEnemy.position) )
            {
                Fight(unit.autoEnemy);
                fireCountdown = 1;
            }
            else
            {
                animation.SetInteger("Attack", 0);
                knightControl.Idle();
                // enemy = null;
            }
        }
        fireCountdown -= Time.deltaTime;
        



    }


    void Fight(Transform _enemy)
    { 
        // enemy = _enemy;
        n++;
            animation.SetInteger("Attack",(n%4)+1); 
            knightControl.Attack((n%4)+1);   
            // animation.SetBool("RunToStab",false);    
    }

    void Effect()
    {
        //  if(enemy != null)
        //     enemy.GetComponent<Health>().TakeDamage(damage);
        GameObject effect1= (GameObject) Instantiate(effect, effectPosition.position,transform.rotation * transform.rotation);
        Destroy(effect1, 0.5f);
    }

}
