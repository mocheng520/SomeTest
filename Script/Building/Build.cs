using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject build_blueprint;

    public void spawn_blueprint()
    {
        Instantiate(build_blueprint);
    }

  
}
