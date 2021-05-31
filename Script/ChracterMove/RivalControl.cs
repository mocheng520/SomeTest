using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalControl : MonoBehaviour
{
    public GameObject GainEffect;
    GameObject gainEffect;

    public GameObject SpurtEffect;
    GameObject spurtEffect;

    public Transform FirePart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GainStart()
    {
        gainEffect = (GameObject)Instantiate(GainEffect, FirePart.position,FirePart.rotation);
    }
    void GainEnd()
    {
        Destroy(gainEffect,0.1f);
    }
    void SpurtStart()
    {
        spurtEffect = (GameObject)Instantiate(SpurtEffect, FirePart.position, FirePart.rotation);
    }
    void SpurtEnd()
    {
        Destroy(spurtEffect, 0.1f);
    }

}
