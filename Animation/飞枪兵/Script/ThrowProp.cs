///////////////////////////////////////////////////////////////////////////
//  ThrowProp                                                            //
//  Kevin Iglesias - https://www.keviniglesias.com/       			     //
//  Contact Support: support@keviniglesias.com                           //
///////////////////////////////////////////////////////////////////////////

/* This Scripts needs 'ThrowingActionSMB' script as StateMachineBehaviour 
in the character Animator Controller state that throws the prop */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KevinIglesias {

    public class ThrowProp : MonoBehaviour {

        //Prop to move
        public GameObject propToThrow;
        public GameObject propToThrowPrefab;
        //Hand that holds the prop
        // public Transform hand;
        public Transform throwPart;

        //Target to throw the prop
        public Transform targetPos;

        //Speed of the prop
        // public float speed = 10;
        
        //Maximum arc the prop will make
        // public float arcHeight = 1;
        
        //Needed for checking if prop was thrown or not
        // public bool launched = false;

        //Character root (for parenting when prop is thrown)
        private Transform characterRoot;
        
        //Different movements for different prop types
        private bool spear;
        private bool tomahawk;
        
        //Needed for calculate prop trajectory
        // private Vector3 startPos; 
        Vector3 firstDir;
        
        void Start() 
        {
            firstDir = propToThrow.transform.up;
        }
        
        //This will make the prop move when launched
        void Update() 
        {
            //Arc throw facing the target
            // if(launched && spear)
            // {
              

            //     float x0 = startPos.x;
                
            //     float x1 = targetPos.position.x;
            //     float z0 = startPos.z;
            //     float z1 = targetPos.position.z;


            //     float dist = x1 - x0;
                
            //     Vector3 nex = Vector3.MoveTowards(propToThrow.position, targetPos.position, speed * Time.deltaTime);
            //     float nextX = nex.x;
            //     float nextZ = nex.z;

            //     // float nextX = Mathf.MoveTowards(propToThrow.position.x, x1, speed * Time.deltaTime);
            //     // float nextZ = Mathf.MoveTowards(propToThrow.position.z, z1, speed * Time.deltaTime);
            //     float baseY = Mathf.Lerp(startPos.y, targetPos.position.y, (nextX - x0) / dist);
            //     float arc = arcHeight * AbsoluteDistance(propToThrow.position, startPos) * AbsoluteDistance(propToThrow.position, targetPos.position) / (AbsoluteDistance(startPos, targetPos.position) * AbsoluteDistance(startPos, targetPos.position));
            //     arc = Mathf.Clamp(arc, 0,5);
            //     Vector3 nextPos = new Vector3(nextX, baseY + arc, nextZ);
            
            //     propToThrow.up = nextPos - propToThrow.position;
            //     // propToThrow.rotation = LookAt2D(nextPos - propToThrow.position);
            //     propToThrow.position = nextPos;
     
            //     float currentDistance = Mathf.Abs(targetPos.position.x - propToThrow.position.x) + Mathf.Abs(targetPos.position.z - propToThrow.position.z);
            //     if(currentDistance < 0.5f)
            //     {
            //         launched = false;

                    
            //     }
            //     // Debug.Log(AbsoluteDistance(new Vector3(0,0,0), new Vector3(1,2,1)));
             
            // }
            
            // //Arc throw rotating forwards
            // if(launched && tomahawk)
            // {
            //     float x0 = startPos.x;
            //     float x1 = targetPos.position.x;
            //     float dist = x1 - x0;
            //     float nextX = Mathf.MoveTowards(propToThrow.position.x, x1, speed * Time.deltaTime);
            //     float baseY = Mathf.Lerp(startPos.y, targetPos.position.y, (nextX - x0) / dist);
            //     float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            //     Vector3 nextPos = new Vector3(nextX, baseY + arc, propToThrow.position.z);
            
            //     propToThrow.transform.Rotate(19f, 0.0f, 0.0f, Space.Self);
                
            //     propToThrow.position = nextPos;
     
            //     float currentDistance = Mathf.Abs(targetPos.position.x - propToThrow.position.x) + Mathf.Abs(targetPos.position.z - propToThrow.position.z);
            //     if(currentDistance < 0.5f)
            //     {
                
            //         launched = false;
                    
            //     }
            // }
        }
        
        float AbsoluteDistance(Vector3 a, Vector3 b) {
            return Mathf.Sqrt((a.x-b.x)*(a.x-b.x) + (a.y-b.y)*(a.y-b.y) +(a.z-b.z)*(a.z-b.z) ) ;
        }

        static Quaternion LookAt2D(Vector3 forward) {
            return Quaternion.Euler(0, 0, (Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg)-90f);
        }   
        
        public void RotateSpear()
        {
            // if(propToThrow!= null)
            propToThrow.transform.up = -propToThrow.transform.up;

        }


        //Function called by 'ThrowingActionSMB' script
        public void ThrowSpear()
        {
            // tomahawk = false;
            // spear = true;
            // startPos = propToThrow.transform;
            GameObject throwSpearGo = (GameObject) Instantiate(propToThrowPrefab, throwPart.position, throwPart.rotation);
            Spear spearGo = throwSpearGo.GetComponent<Spear>();
            if(spearGo != null)
            {
                spearGo.Seek(targetPos,propToThrow.transform.position);
            }
        
            propToThrow.SetActive(false);
            // propToThrow.GetComponent<MeshRenderer>().material.color = firstColor;
  
            // launched = true;
        }
        
        //Function called by 'ThrowingActionSMB' script
        public void ThrowTomahawk()
        {
            
        //     spear = false;
        //     tomahawk = true;
        //     startPos = propToThrow.transform.position;
        //     propToThrow.SetParent(characterRoot);
        //     launched = true;
        }
        
        //Function called by 'ThrowingActionSMB' script
        public void RecoverProp()
        {


            // launched = false;
            // if(propToThrow != null)
                // propToThrow.GetComponent<MeshRenderer>().material.color = disapearColor;
            propToThrow.SetActive(true);
            propToThrow.transform.up = throwPart.up;

            // propToThrow.transform.localPosition = zeroPosition;
            // propToThrow.transform.localRotation = zeroRotation;
 
        }
    }

}