using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
     private Transform targetPos;
     private Vector3 startPos;
    [Header("Attribute")]
    public float speed = 10f;
    public float damage = 10f;
    public float arcHeight = 1;

    float wholeDistance;
    bool done = false;

    void Start()
    {
         if (targetPos == null)
        {
            Destroy(gameObject);
            return;
        }
        wholeDistance =  AbsoluteDistance(startPos, targetPos.position);
    }

    // Update is called once per frame
    void Update()
    {
                if(targetPos == null ) return ;
                if(done)
                {
                    transform.position = targetPos.position;
                    return ;
                } 
                float x0 = startPos.x;
                
                float x1 = targetPos.position.x;
                float z0 = startPos.z;
                float z1 = targetPos.position.z;


                float dist = x1 - x0;
                
                Vector3 nex = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
                float nextX = nex.x;
                float nextZ = nex.z;

                // float nextX = Mathf.MoveTowards(propToThrow.position.x, x1, speed * Time.deltaTime);
                // float nextZ = Mathf.MoveTowards(propToThrow.position.z, z1, speed * Time.deltaTime);
                float baseY = Mathf.Lerp(startPos.y, targetPos.position.y, (nextX - x0) / dist);
                float arc = 0f;
                
                float a2b =  AbsoluteDistance(transform.position, startPos);
                float b2c = AbsoluteDistance(transform.position, targetPos.position);
                float a2c =  AbsoluteDistance(startPos, targetPos.position);
                if(a2c >= 15f)
                {
                    arc = arcHeight * a2b * b2c /(1.2f* a2c * a2c);
                    // arc = Mathf.Min(5 ,arc);
                }
                else if(a2c >= 8f)
                    arc = arcHeight * a2b * b2c /(3f* a2c * a2c);
                else if(a2c >= 4f)
                    arc = arcHeight * a2b * b2c /(7f* a2c * a2c);

                arc =  Mathf.Clamp(arc, 0, 5f);
                

                    

                
                    
                Vector3 nextPos = new Vector3(nextX, baseY + arc, nextZ);
                // Debug.Log(arcHeight * AbsoluteDistance(transform.position, startPos) * AbsoluteDistance(transform.position, targetPos.position)+" "+(AbsoluteDistance(startPos, targetPos.position) * AbsoluteDistance(startPos, targetPos.position)));
            
                transform.up = nextPos - transform.position;
                // propToThrow.rotation = LookAt2D(nextPos - propToThrow.position);
                transform.position = nextPos;
     
                float currentDistance = Mathf.Abs(targetPos.position.x - transform.position.x) + Mathf.Abs(targetPos.position.z - transform.position.z);
                if(currentDistance < 0.5f)
                {
                    HitTarget();
                    done = true;

                    
                }





    }


    float AbsoluteDistance(Vector3 a, Vector3 b) {
            return Mathf.Sqrt((a.x-b.x)*(a.x-b.x) + (a.y-b.y)*(a.y-b.y) +(a.z-b.z)*(a.z-b.z) ) ;
        }

    public void Seek(Transform _target, Vector3 _startPos)
    {
            targetPos = _target;
            startPos = _startPos;
    }


    void HitTarget()
    {
         
        // target.parent.GetComponent<Health>().TakeDamage(damage);
        // GameObject exploreEffect= (GameObject) Instantiate(ExploreEffect, transform.position,transform.rotation);
        // Destroy(exploreEffect, 2f);
        // Destroy(fireBallEffect, 2f);
        transform.position = targetPos.position;
        Destroy(gameObject, 2f);
    }
}
