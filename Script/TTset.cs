using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("die",0f,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void die()
    {
        Destroy(gameObject,7f);
    }
}
