using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCheck : MonoBehaviour
{
    public GameObject CreatePrefab;

    private Vector3 now = new Vector3(0f,3f,0f);
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePosition = Input.mousePosition;
        now.x = transform.position.x + 30f +33f*MousePosition.x/Screen.width;
        now.z = transform.position.z + 12f +27f*MousePosition.y/Screen.height;
        Instantiate(CreatePrefab, now, transform.rotation);
    }
}
