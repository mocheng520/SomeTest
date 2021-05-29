using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
     public List<GameObject> unitList = new List<GameObject>();
     public List<GameObject> unitsSelected = new List<GameObject>();

     private static UnitSelections _instance;
     public static UnitSelections Instance { get { return _instance; } }

     void Awake()
     {
         if(_instance !=null && _instance != this)
         {
             Destroy(this.gameObject);
         }
         else
         {
             _instance = this;
         }
     }


     public void ClickSelect(GameObject unitToAdd)
     {
         DeSelectAll();
         unitsSelected.Add(unitToAdd);
         unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
         unitToAdd.GetComponent<UnitMovement>().enabled = true;
     }

     public void ShiftClickSelect(GameObject unitToAdd)
     {
         if(!unitsSelected.Contains(unitToAdd))
         {
             unitsSelected.Add(unitToAdd);             
             unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
         }
         else
         {
             unitsSelected.Remove(unitToAdd);            
             unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            unitToAdd.GetComponent<UnitMovement>().enabled = false;
         }
         
     }
     
     public void DragSelect(GameObject unitToAdd)
     {
         if(!unitsSelected.Contains(unitToAdd))
         {
             unitsSelected.Add(unitToAdd);
             unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
         }
         
     }
     public void DeSelectAll()
     {
         foreach(var unit in unitsSelected)
         {
             
         unit.transform.GetChild(0).gameObject.SetActive(false);
         unit.GetComponent<UnitMovement>().enabled = false;
         }
         unitsSelected.Clear();
     }          

     public void DeSelect(GameObject unitToAdd)
     {
         
     }










}
