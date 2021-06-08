using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    //findRand表示可以自动攻击 跟随的范围
    public float findRange;
    public Transform autoEnemy;
    //sd
    Animator animation;
    UnityEngine.AI.NavMeshAgent  myAgent;
    Vector3 dir;
    string enemyTag = "enemy";
    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
        animation = GetComponent<Animator>();
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("UpdateEnemy",0f,0.5f);
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
        
    }



     void Update()
    {
        animation.SetFloat("RunningSpeed",myAgent.velocity.magnitude/myAgent.speed);

        if(animation.GetFloat("RunningSpeed") >=0.97f)
            SmoothFaceToTarget(myAgent.steeringTarget);
    }

    public void SmoothFaceToTarget(Vector3 target)
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


     void UpdateEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);


        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(shortestDistance > findRange)
        autoEnemy = null;

        if(nearestEnemy != null && shortestDistance <= findRange)
        {
            autoEnemy = nearestEnemy.transform;
        }

    }
}
