using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMove2 : MonoBehaviour
{
    private NavMeshAgent nav;
    // public Transform cubePos;
    void Start()
    {
        nav = this.transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                 nav.SetDestination(hit.point);
                // animation.SetBool("IsRun", true);
                               
            }
        }
        //  Debug.Log(transform.position+" "+nav.remainingDistance);

        


       
    }
}
