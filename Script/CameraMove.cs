using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed = 10f;
    public float zoomSpeed = 10f;
    public float rotateSpeed = 10f;

    public float maxHeight ;
    public float minHeight;

    Vector2 p1;
    Vector2 p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal");
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical");
        float scrollSp = Mathf.Log(transform.position.y) * - zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
        Vector3 VerticalMove = new Vector3(0, scrollSp, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;
       
        Vector3 move = VerticalMove + lateralMove + forwardMove;
        
        transform.position += move;

        getCameraRotation();
    }

    void getCameraRotation()
    {
        if(Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 -p1).x * rotateSpeed;
            float dy = (p2 -p1).y * rotateSpeed;
            
            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy,0,0));

            p1 = p2;

        }
    }
}
