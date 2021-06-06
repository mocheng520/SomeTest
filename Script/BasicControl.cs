using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicControl : MonoBehaviour
{
    Animator animation;
    NavMeshAgent  myAgent;
    Vector3 dir;
    void Start()
    {
        animation = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateEnemy",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        animation.SetFloat("RunningSpeed",myAgent.velocity.magnitude/myAgent.speed);

        if(animation.GetFloat("RunningSpeed") >=0.97f)
            SmoothFaceToTarget(myAgent.steeringTarget);
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
}
