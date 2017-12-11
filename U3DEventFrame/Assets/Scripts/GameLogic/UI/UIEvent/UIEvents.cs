using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using U3DEventFrame;


// UI  起始  消息ID  到 最大 
public enum UIPlayerEvent
{
 
     ReduceBlood = ManagerID.UIManager +1 ,

     TeacherLoading,
     TeacherRegisting,

      

    MaxValue


}

public enum UIZhanagEvent
{
     Initila =  UIPlayerEvent.MaxValue+1 ,



      
       Regist,

     Unreigist ,

     MaxValue
}


public enum UILiSiEvent

{
 
        Initil =  UIZhanagEvent.MaxValue +1,

        UnRegist,

        MaxValue
}


