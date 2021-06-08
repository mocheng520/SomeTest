using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherControl : MonoBehaviour
{
       int n;
    
    UnitMovement movement;
    Unit unit;
    [Header("Attribute")]
    public float damage = 10f;
    public float fireCountdown;
    [Header("Arrow")]
    public GameObject arrowPrefab;
    public Transform shotPosition;
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
    }

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

        //移动指令优先级最高：已经写在UnitMovement里面了，这里判断到达目标地点，则指令完成
        //其次是锁定目标：也写在UnitMovement里面了
        //再其次是自动目标


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
            movement.MoveToBorder(unit.autoEnemy. transform.position, movement.radius, movement.attackRange);
        }

        //先判断选中的目标是什么类型：是敌人则攻击，是食物则获取，这里先默认是敌人，等会回来改
        //移动到目标附近，再转化到战斗状态
    
        if(fireCountdown < 0)
        {
            // if(movement.hasInstruction && movement.focus == null)
            // { 
            //         animation.ResetTrigger("RunToStab");
            //         animation.SetBool("Jump", false);
            // }


            if(movement.focus != null  && movement.ArriveBorder(movement.focus.position) )
            {
               
                    // animation.ResetTrigger("RunToStab");
                    Fight(movement.focus);
                    fireCountdown = 2;
            
            }
            else if(  unit.autoEnemy != null && movement.ArriveBorder(unit.autoEnemy.position) )
            {
                Fight(unit.autoEnemy);
                fireCountdown = 2;
            }
            else
            {
                animation.SetInteger("Attack", 0);
                // enemy = null;
            }
        }
        fireCountdown -= Time.deltaTime;
        
    }

    void Fight(Transform _enemy)
    { 
        // enemy = _enemy;
        n++;
        animation.SetInteger("Attack",(n%2)+1);    
            // animation.SetBool("RunToStab",false);
       
    }


    
    // void ChooseAnimation()
    // {
    //     Vector2 my2DPosition = new Vector2(transform.position.x, transform.position.z);
    //     Vector2 enemy2DPosition = new Vector2(enemyPosition.position.x, enemyPosition.position.z);
    //     n=1;
    //     if(Vector2.Distance(my2DPosition, enemy2DPosition) < 3f)
    //     {
    //         if(transform.position.y > enemyPosition.position.y+2)
    //             n =3;
    //         else if (transform.position.y+2 < enemyPosition.position.y)
    //         n =2;
    //     }
       
    // }

    // bool ArriveEnemyBorder(Vector3 targetPosition)
    // {
        
    //     Debug.Log( Vector3.Distance(targetPosition,transform.position));
    //    return Vector3.Distance(transform.position,  targetPosition + (transform.position - targetPosition).normalized * (movement.radius + movement.attackRange)) < 0.1f;
    // }

    void ShotArrow()
    {
        GameObject arrowGo = (GameObject)Instantiate(arrowPrefab,shotPosition.position,transform.rotation);
        Arrow  arrow = arrowGo.GetComponent<Arrow>();
        if(arrow != null)
            {
                Debug.Log(movement.focus+"11"+ unit.autoEnemy);
                Transform target = movement.focus == null ? unit.autoEnemy:movement.focus;
                arrow.Seek(target);
            }
    }

  

}
