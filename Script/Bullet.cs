using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public GameObject explosionEffect;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
         

    }
    void HitTarget()
    {
        GameObject effect= (GameObject) Instantiate(explosionEffect, transform.position,transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }


}
