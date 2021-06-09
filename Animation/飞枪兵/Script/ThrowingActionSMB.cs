﻿///////////////////////////////////////////////////////////////////////////
//  ThrowingActionSMB                                                    //
//  Kevin Iglesias - https://www.keviniglesias.com/       			     //
//  Contact Support: support@keviniglesias.com                           //
///////////////////////////////////////////////////////////////////////////

/* This Scripts is needed for other scripts (IdleThrowTrick, ChangeSpear, 
ThrowProp, ThrowMultipleProps & ThrowBigAxe) to work */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KevinIglesias {
    public class ThrowingActionSMB : StateMachineBehaviour {

        //Which action will be used
        public int action;
    
        //Point in the animation in which the prop will be thrown (0.5 means middle of the animation)
        public float timePoint;
    
        //Scripts for the different actions
        private IdleThrowTrick iTTScript;
        private ChangeSpear cSScript;
        // private ThrowProp throwSpear;
        private ThrowSpearControl throwSpear;
        private ThrowMultipleProps tMPScript;
        private ThrowBigAxe tBAScript;
        
        //Needed for avoiding multiple throwns
        private bool actionDone;
        private bool rotateDone;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
           
            //Get the script of the assigned action
            switch(action)
            {
                case 0:
                    if(iTTScript == null)
                    {
                        iTTScript = animator.GetComponent<IdleThrowTrick>();
                    }
                    
                    actionDone = false;
                    
                    if(iTTScript == null)
                    {
                        actionDone = true;
                    }
                break;
                case 1:
                    if(cSScript == null)
                    {
                        cSScript = animator.GetComponent<ChangeSpear>();
                    }
                    
                    actionDone = false;
                    
                    if(cSScript == null)
                    {
                        actionDone = true;
                    }
                break;
                case 2:

                    if(throwSpear == null)
                        {
                            throwSpear = animator.GetComponent<ThrowSpearControl>();
                        }
                        
                        actionDone = false;
                        
                        if(throwSpear == null)
                        {
                            actionDone = true;
                        }
                        Debug.Log("rev");
                        throwSpear.RecoverProp();
                        throwSpear.RotateSpear();
                    break;
                
                    
                case 3:
                case 4:
                    if(throwSpear == null)
                    {
                        throwSpear = animator.GetComponent<ThrowSpearControl>();
                    }
                    
                    actionDone = false;
                    
                    if(throwSpear == null)
                    {
                        actionDone = true;
                    }
                break;
                case 5:
                case 6:
                case 7:
                case 8:
                    if(tMPScript == null)
                    {
                        tMPScript = animator.GetComponent<ThrowMultipleProps>();
                    }
                    
                    actionDone = false;
                    
                    if(tMPScript == null)
                    {
                        actionDone = true;
                    }
                break;
                
                case 9:
                    if(tBAScript == null)
                    {
                        tBAScript = animator.GetComponent<ThrowBigAxe>();
                    }
                    
                    actionDone = false;
                    
                    if(tBAScript == null)
                    {
                        actionDone = true;
                    }
                break;
            }
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        {
            // Debug.Log(rotateDone);
            //长矛兵改变武器方向
            // if(!rotateDone && action == 2)
            // {
            //     if(stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < timePoint)
            //     {
                    
            //         // throwSpear.RecoverProp();
            //         throwSpear.RotateSpear();
            //     }
            //      rotateDone = true;
            //      actionDone = false;
            // }

            //Do the action if it wasn't done yet at the assigned point
            // Debug.Log(stateInfo.normalizedTime);
            if(!actionDone)
            {
                
                // Debug.Log(stateInfo.normalizedTime);
                if(stateInfo.normalizedTime >= timePoint && stateInfo.normalizedTime < 1f)
                {
                    switch(action)
                    {
                        case 0:
                            iTTScript.SpinProp();
                        break;
                        case 1:
                            cSScript.DoChangeSpear();
                        break;
                        case 2:
                            throwSpear.ThrowSpear();
                        break;
                        case 3:
                            throwSpear.RecoverProp();
                        break;
                        case 4:
                            // throwSpear.ThrowTomahawk();
                        break;
                        case 5:
                            tMPScript.Throw1();
                        break;
                        case 6:
                            tMPScript.Throw2();
                        break;
                        case 7:
                            tMPScript.RecoverProp1();
                        break;
                        case 8:
                            tMPScript.RecoverProp2();
                        break;
                        case 9:
                            tBAScript.SpinProp();
                        break;
                    }
                    actionDone = true;
                    rotateDone = false;
                }
               
                
            }

        }


        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       throwSpear.RotateSpear();
    }
    }
}
