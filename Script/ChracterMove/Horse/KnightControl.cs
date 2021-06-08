using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightControl : MonoBehaviour
{
    Animator animation;
    void Start()
    {
        animation = GetComponent<Animator>();
    }


    public void Idle()
    {
        //Idle发生在两种情况，马在跑 或 马在Idle
        //即马没在攻击，即马的attack == 0
        animation.SetBool("Running", false); 
        animation.SetBool("Idle",true);
    }

    public void Attack(int n)
    {
        animation.SetBool("Running", false);
        animation.SetBool("Idle", false);
        animation.SetInteger("Attack", n);
        
    }

    public void Run()
    {
        animation.SetBool("Idle",false);
        animation.SetBool("Running", true);
    }
}
