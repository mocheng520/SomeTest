using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitMovement : MonoBehaviour
{
    Camera  myCam;
    public UnityEngine.AI.NavMeshAgent  myAgent;
    public LayerMask ground ;

    Animator animation;
    //current focus
    public Transform focus;


    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(focus);

         if(myAgent.remainingDistance< 0.1f)
        {
           animation.SetBool("IsRuning", false);
        }


        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
            {
                // motor.MoveToPoint(hit.point);
                // Debug.Log(hit.collider.GetComponent<CapsuleCollider>().radius);
                // if(Vector3.Distance(transform.position, hit.point) > 0.1f)
                //     animation.SetBool("IsRuning", true);
                if( hit.collider.tag =="enemy")
                {
                    focus = hit.transform;
                    float radius = Mathf.Infinity;
                    //判断是人还是建筑
                    if(hit.collider.GetComponent<CapsuleCollider>() != null )
                    {
                        //走到敌人边界
                        radius = hit.collider.GetComponent<CapsuleCollider>().radius;
                    }   
                    if (Vector3.Distance(transform.position, hit.transform.position) > radius)
                    { 
                        animation.SetBool("IsRuning", true);
                        Vector3 border = hit.transform.position + (transform.position - hit.transform.position).normalized * radius * 0.8f;
                        myAgent.SetDestination(border);
                    }
                    
                }
                else
                {
                    myAgent.SetDestination(hit.point);
                    DeFocus();
                    animation.SetBool("IsRuning", true);
                }

                 
            }

            // animation.SetBool("IsRuning", true);

        }
        
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
