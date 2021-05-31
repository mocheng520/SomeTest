using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;

    public GameObject FireBallEffect;
    public GameObject ExploreEffect;
    // public GameObject ExploreEffect2;
      GameObject fireBallEffect ;
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

        fireBallEffect = (GameObject) Instantiate(FireBallEffect, transform.position, transform.rotation);
        // if(n == 2)
        //   StartCoroutine(Secondfire());  
        
        
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
       
        fireBallEffect.transform.position = transform.position; 
        transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
    }
     void HitTarget()
    {
        GameObject exploreEffect= (GameObject) Instantiate(ExploreEffect, transform.position,transform.rotation);
        Destroy(exploreEffect, 2f);
        Destroy(fireBallEffect, 2f);
        Destroy(gameObject);
    }
}
