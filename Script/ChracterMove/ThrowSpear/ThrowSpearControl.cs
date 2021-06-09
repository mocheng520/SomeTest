using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpearControl : MonoBehaviour
{
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
    [Header("HoldSpear&ThrowSpear")]
    public Transform throwPart;
    public GameObject propToThrow;
    public GameObject propToThrowPrefab;

    Animator animation ;
    // UnitMovement movement;
    // Unit unit;

    Vector3 firstDir;
    void Start()
    {
        animation = GetComponent<Animator>();
        movement = GetComponent<UnitMovement>();
        unit = GetComponent<Unit>();

        firstDir = propToThrow.transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.forward);
         //转向到目标点，
        if(animation.GetFloat("RunningSpeed")<0.02f && animation.GetInteger("Attack")>0 )
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
            else
                animation.SetInteger("Attack", 0);
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



        if(fireCountdown < 0)
        {
            // if(movement.hasInstruction && movement.focus == null)
            // { 
            //         animation.ResetTrigger("RunToStab");
            //         animation.SetBool("Jump", false);
            // }


            if(movement.focus != null && movement.ArriveBorder(movement.focus.position) )
            {
                // if(animation.GetFloat("RunningSpeed") > 0.1f && Vector3.Distance(movement.focus.position, transform.position)>=4f && Vector3.Distance(movement.focus.position, transform.position)<=5f )
                //     animation.SetTrigger("RunToStab");
                
                // if(movement.ArriveBorder(movement.focus.position))
                // {
                    // animation.ResetTrigger("RunToStab");
                    Fight(movement.focus);
                //    animation.SetInteger("Attack", n);
                    fireCountdown = 1.5f;
                // }
            }
            else if(  unit.autoEnemy != null && movement.ArriveBorder(unit.autoEnemy.position) )
            {
                Fight(unit.autoEnemy);
                // animation.SetBool("Attack", true);
                fireCountdown = 1.5f;
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



    public void ThrowSpear()
    {
            // tomahawk = false;
            // spear = true;
            
            // startPos = propToThrow.transform;
            GameObject throwSpearGo = (GameObject) Instantiate(propToThrowPrefab, throwPart.position, throwPart.rotation);
            Spear spearGo = throwSpearGo.GetComponent<Spear>();
            if(spearGo != null)
            {
                if(movement.focus!= null)
                    spearGo.Seek(movement.focus,propToThrow.transform.position);
                else if( unit.autoEnemy != null)
                    spearGo.Seek(unit.autoEnemy,propToThrow.transform.position);
            }
        
            propToThrow.SetActive(false);
            // propToThrow.GetComponent<MeshRenderer>().material.color = firstColor;
  
            // launched = true;
    }

    public void RotateSpear()
    {
            // if(propToThrow!= null)
            propToThrow.transform.up = -propToThrow.transform.up;

    }

    public void RecoverProp()
    {


            // launched = false;
            // if(propToThrow != null)
                // propToThrow.GetComponent<MeshRenderer>().material.color = disapearColor;
            propToThrow.SetActive(true);
//             propToThrow.transform.up = transform.forward;
// Debug.Log("12e213re231re");

// Debug.Log(transform.forward);
            // propToThrow.transform.localPosition = zeroPosition;
            // propToThrow.transform.localRotation = zeroRotation;
 
    }

}
