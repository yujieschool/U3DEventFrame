--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion




local    CUIDemoMsgBegin  =   LManagerID.UIManager +1


CUIDemoMsgEvent = {


"DemoMsg",

DemoMsg


}


ResetTableKeyValue(CUIDemoMsgBegin,CUIDemoMsgEvent);


local   uiEventsBegin =  LManagerID.LUIManager +1  ;


UIDemoLoadingEvents = {


   "Initial",

   "GetResources",

   "TestMsg",

   "MaxValue",


   Initial,

   GetResources ,

   TestMsg,

   MaxValue


}

ResetTableKeyValue(uiEventsBegin,UIDemoLoadingEvents);



UIDemoRegistEvents = {

    "Initial",

   "GetResources",

     

    "MaxValue",

    Initial,

    GetResources ,

    MaxValue


}

ResetTableKeyValue(UIDemoLoadingEvents.MaxValue,UIDemoRegistEvents);



LRoateSelfEevent = {

    "Initial",

   "GetResources",

     

    "MaxValue",

    Initial,

    GetResources ,

    MaxValue


}

ResetTableKeyValue(UIDemoRegistEvents.MaxValue,LRoateSelfEevent);

