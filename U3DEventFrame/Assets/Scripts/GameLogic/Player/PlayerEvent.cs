using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using U3DEventFrame;


public enum PlayerEvent
{
 
      Initial = ManagerID.CharatorManager +1 ,

       JoyStickCtr,

       ReduceBlood ,

       NormalAttack,

       BigAttack,
      
       StopRun,

       MaxValue 

      
}


public enum PlayerZhangSan
{

    Initial = PlayerEvent.MaxValue +1,

     Regist ,



     MaxValue
      
}