using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
        
    }
}
