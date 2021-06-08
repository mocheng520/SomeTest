using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(SpearControl))]
public class UnitMovement : MonoBehaviour
{
    Camera  myCam;
    UnityEngine.AI.NavMeshAgent  myAgent;
    public LayerMask ground ;
    
    Animator animation;
    public float radius = Mathf.Infinity;
    [Header("Attribute")]
    public Transform focus;
    public float attackRange ;
    [Header("Instruction")]
    public bool hasInstruction = false;
    public Vector3 InstructionPosition;

    // SpearControl spearControl;


    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animation = GetComponent<Animator>();
        // spearControl = GetComponent<SpearControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(animation.GetBool("IsDead")) return ;

        //  if(myAgent.remainingDistance< 0.1f)
        // {
        //    animation.SetBool("IsRuning", false);
        // }


        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
            {
                // hasInstruction = true;

                if( hit.collider.tag !="Untagged")
                {
                    // print(hit.collider.tag);
                    focus = hit.transform;
                    // float radius = Mathf.Infinity;
                    //判断是人还是建筑
                    if(hit.collider.GetComponent<CapsuleCollider>() != null )
                    {
                        //走到敌人边界
                        radius = hit.collider.GetComponent<CapsuleCollider>().radius;
                    }   
                    //if(是建筑)
                    //


                    //如果离目标过远，会先走过去
                    if (Vector3.Distance(transform.position, hit.transform.position) > radius)
                    { 
                        // animation.SetBool("IsRuning", true);
                        // Vector3 border = hit.transform.position + (transform.position - hit.transform.position).normalized * (radius + attackRange)  ;
                        // Debug.Log(border+" "+hit.transform.position);
                        MoveToBorder(hit.transform.position, radius, attackRange );
                        hasInstruction = true;
                        InstructionPosition = hit.transform.position + (transform.position - hit.transform.position).normalized * (radius + attackRange);
                       
                    }
                    
                }
                else
                { 
                    DeFocus();
                    myAgent.SetDestination(hit.point);
                    print("移动指令");
                    hasInstruction = true;
                    InstructionPosition = hit.point;
                    // animation.SetBool("IsRuning", true);
                }

                 
            }
      }
        
    }

    public void MoveToBorder(Vector3 _target,  float _radius, float _attackRange)
    {
        Vector3 border = _target + (transform.position - _target).normalized * (_radius + _attackRange);
        //如果自身与目标的距离 大于 border与目标的距离，则动
        if(Vector3.Distance(border,_target)< Vector3.Distance(transform.position, _target))
        {
             myAgent.SetDestination(border);
        }   
      
    }

    public bool  ArriveBorder(Vector3 targetPosition)
    {
        Vector3 border = targetPosition + (transform.position - targetPosition).normalized * (radius + attackRange);
        bool outside = Vector3.Distance(transform.position,  border) < 0.1f;
        bool inside = Vector3.Distance(transform.position, targetPosition) < Vector3.Distance(border, targetPosition);
        return outside || inside;

    }

    // void SetFocus(Transform _focus)
    // {
    //     focus = _focus;
    //     i
        
    // }
    public void DeFocus()
    {
        focus = null;
    }

   
}
