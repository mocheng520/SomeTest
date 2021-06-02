using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;

    public GameObject ShootFromEffect;
    public GameObject ArrowEffect;
    public GameObject ExploreEffect;
    // public GameObject ExploreEffect2;
      GameObject arrowEffect ;
    //    GameObject fireBallEffect2 ;
      int whichAnimation ;

    public void Seek(Transform _target)
    {
        target = _target;
    
    }

   
    void Start()
    {
         if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        arrowEffect = (GameObject) Instantiate(ArrowEffect, transform.position, transform.rotation);
        transform.LookAt(target);

        GameObject shootFromEffect= (GameObject) Instantiate(ShootFromEffect, transform.position,transform.rotation);
        Destroy(shootFromEffect, 0.8f);
        
        
    }
    //  IEnumerator SecondFire()
    //  {    
    //      yield return new WaitForSeconds(0.2f);
    //     fireBallEffect2 = (GameObject) Instantiate(FireBallEffect, transform.position, transform.rotation);
     
    //      } 

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
       
        arrowEffect.transform.position = transform.position; 
        transform.LookAt(target);
        transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
    }
     void HitTarget()
    {
        GameObject exploreEffect= (GameObject) Instantiate(ExploreEffect, transform.position,transform.rotation);
        Destroy(exploreEffect, 2f);
        Destroy(arrowEffect, 0.1f);
        Destroy(gameObject);
    }
}