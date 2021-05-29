using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{
    public Color hoverColor;
    public Color startColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }

    void OnMouseEnter ()
    {
        rend.material.color = hoverColor; 
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
