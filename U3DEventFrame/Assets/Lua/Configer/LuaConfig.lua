--region *.lua
--Date
--此文件由[BabeLua]插件自动生成


                  
--endregion

LanguageOption = {
    English = 1,
    Chinease = 2,
}

LFishType = {
    Normal = 1,
    PVP = 2,
}


GloableConfig  ={
    
    ipAdrr = "192.168.0.64",  
    --ipAdrr = "115.28.102.93", 
 
    port = 20500,

    token = "",

    IsNeedConnect = true,
    IsNetPlayer = false ,

    HeartbeatTimeCount = 20,

    IsDubugMode = false,



    chatLine = nil,

    --跳过行.
    MarkSkipLine = "--",

    --跳过列.
    MarkSkipCell = "&&",

    --单元格标记.
    MarkDivideCell = '|',

    --行标记.
    MarkDivideLine = '\n',

    --工作簿标记.
    MarkDivideSheet = '#',

    --数组标记.
    MarkDivideArray = ';',

    --数组标记.
    MarkArray = ',',

    CurrentShowUI = 0,

    --是否开始引导
    IsOpenGuide = false, 
    --前半部分引导标识  服务器要求加上的 1028 引导前发送 1 否则 0
    IsGuideStateServerTag = 1,
    --当前引导的ID
    CurrentGuideID = -1,


    IsPVPGame = false,
    IsFishing = false,
    PVPRoomId = nil,
}