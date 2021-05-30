using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public LayerMask Ground;
    public GameObject prefab;
    Camera myCam;

    void Start()
    {
        myCam = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
        {
            transform.position = hit.point;
        }

        if(Input.GetMouseButton(0))
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
