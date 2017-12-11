using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using  U3DEventFrame ;


public enum UIDemoEvents
{

    Initial =  ManagerID.UIManager +1 ,



    DemoMsg,

    MaxValue 
}


public enum UIDemoLoading

{
 
     Initial =  UIDemoEvents.MaxValue +1 ,

     GetResources ,



      ShowPanel  ,

     MaxValue

}


public enum UIDemoRegisting
{

    Initial = UIDemoLoading.MaxValue + 1,

    GetResources,

    

    MaxValue

}

public enum UIDemoLoadingEvents
{
    Initial =ManagerID.LUIManager+2,

    GetResources,

    TestMsg,

    MaxValue

}

 

