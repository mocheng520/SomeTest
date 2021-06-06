using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpearControl))]
public class UnitMovement : MonoBehaviour
{
    Camera  myCam;
    public UnityEngine.AI.NavMeshAgent  myAgent;
    public LayerMask ground ;
    public float attackRange = 2f;
    Animator animation;
    public float radius = Mathf.Infinity;
    //current focus
    public Transform focus;

    SpearControl spearControl;


    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animation = GetComponent<Animator>();
        spearControl = GetComponent<SpearControl>();
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
                    print(hit.collider.tag);
                    focus = hit.transform;
                    float radius = Mathf.Infinity;
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
                        MoveToBorder(hit.transform.position , transform.position, radius, attackRange );
                    }
                    
                }
                else
                { 
                    DeFocus();
                    myAgent.SetDestination(hit.point);
                    spearControl.hasInstruction = true;
                    spearControl.InstructionPosition = hit.point;
                    // animation.SetBool("IsRuning", true);
                }

                 
            }

            // animation.SetBool("IsRuning", true);

        }
        
    }

    public void MoveToBorder(Vector3 _target, Vector3 myPosition, float _radius, float _attackRange)
    {
        Vector3 border = _target + (myPosition - _target).normalized * (_radius + _attackRange)  ;
                        // Debug.Log(border+" "+hit.transform.position);
        myAgent.SetDestination(border);
        // myAgent.SetDestination(target);
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
