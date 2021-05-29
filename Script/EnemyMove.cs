using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1f;
    private Transform target;
    private int wavepointIndex = 0;
    private Animator animation;



    void Start()
    {
        animation = GetComponent<Animator>();
        target = WayPoints.points[0];    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        //动画自己会走，所以只需要改变朝向就可以了
        // transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
       
        animation.SetBool("IsRun", true);
        
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f && wavepointIndex < WayPoints.points.Length)
        {
            target = WayPoints.points[++wavepointIndex];
        }
        

    }

}
