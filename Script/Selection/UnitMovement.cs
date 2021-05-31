using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    Camera  myCam;
    public UnityEngine.AI.NavMeshAgent  myAgent;
    public LayerMask ground ;

    Animator animation;

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, ground))
            {
                 myAgent.SetDestination(hit.point);
                 
            }
            animation.SetBool("IsWalking", true);
        }

        
    }
}
