using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStick : MonoBehaviour
{
    private Transform target;
    [Header("Attribute")]
    public float speed = 10f;
    public float damage = 10f;

    [Header("Effect")]
    public Transform FirePart;
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

        // fireBallEffect = (GameObject) Instantiate(FireBallEffect, FirePart.position, transform.rotation);
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
        if(target == null) return;
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Rotate(Vector3.right * Time.deltaTime* 1000f);
        Debug.Log(transform.rotation);

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
    //    Debug.Log(dir);
        // fireBallEffect.transform.position = FirePart.position; 
        transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
    }
     void HitTarget()
    {
         
        target.parent.GetComponent<Health>().TakeDamage(damage);
        GameObject exploreEffect= (GameObject) Instantiate(ExploreEffect, transform.position,transform.rotation);
        Destroy(exploreEffect, 2f);
        // Destroy(fireBallEffect, 2f);
        Destroy(gameObject);
    }
}
