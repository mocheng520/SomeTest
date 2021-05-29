
using UnityEngine;

public class LookController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 20f;
    public Vector2 panLimit;
    public float scrollSpeed = 2f;
    public float maxY = 120f;
    public float minY = 11f; 

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector2 MousePosition = Input.mousePosition;
        if(Input.GetKey("w") || (MousePosition.y >= Screen.height - panBorderThickness && MousePosition.y<Screen.height))
            pos.x += panSpeed * Time.deltaTime;
        if(Input.GetKey("s") || MousePosition.y <= panBorderThickness && MousePosition.y>0)
            pos.x -= panSpeed * Time.deltaTime;
        if(Input.GetKey("a") || MousePosition.x <= panBorderThickness && MousePosition.x>0)
            pos.z +=panSpeed * Time.deltaTime;
        if(Input.GetKey("d") || MousePosition.x >= Screen.width - panBorderThickness && MousePosition.y<Screen.width)
            pos.z -=panSpeed * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime;


        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);



        transform.position = pos;
    }
}
