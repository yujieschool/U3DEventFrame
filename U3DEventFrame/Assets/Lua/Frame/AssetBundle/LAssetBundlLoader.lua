
require "Frame.AssetBundle.LAssetEvent"
require "Frame.AssetBundle.LAssetMsg"
require  "Frame.AssetBundle.LAssetRequest"


--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion




--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LAssetBundleLoader = LAssetBase:New()

local this = LAssetBundleLoader;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LAssetBundleLoader.__index = LAssetBundleLoader

--构造体，构造体的名字是随便起的，习惯性改为New()
function LAssetBundleLoader:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LAssetBundleLoader);  --将self的元表设定为Class

   self.msgIds = {};

    return self;    --返回自身
end



function  LAssetBundleLoader:Awake()


       this.msgIds = {
       LAssetEvent.HunkRes,
       LAssetEvent.HunkMutiRes,
        LAssetEvent.HunkMutiBundleAndRes,
       LAssetEvent.ReleaseSingleObj,

        LAssetEvent.ReleaseSingleObj,

        LAssetEvent.ReleaseBundleObjes,

        LAssetEvent.ReleaseScenceObjes,

        LAssetEvent.ReleaseBundleAndObjects,
         LAssetEvent.ReleaseSingleBundle,

        LAssetEvent.ReleaseScenceBundle,

        LAssetEvent.ReleaseAll,
       };

       this:RegistSelf(this,this.msgIds);


end


function LAssetBundleLoader.SendMsgs()
    -- body

    -- tmpMsg  = LMsgBase:New(LUIEvent.LRegist);
     
     
   --   this:SendMsg(tmpMsg);

end





function  LAssetBundleLoader:ProcessEvent(msg)

        if (msg.msgId == LAssetEvent.HunkRes) then

          --  error("lua  get res ==="..msg.resName);
            LuaLoadRes.Instance:GetResources(msg.scenceName,msg.bundleName,msg.resName,msg.isSingle,msg.callBackFunc);
        elseif (msg.msgId == LAssetEvent.HunkMutiRes)  then
           LuaLoadMutiRes.Instance:GetResources( msg.scenceName,  msg.bundleName,  msg.isSingle, msg.callBackFunc , msg.resName);


          elseif (msg.msgId == LAssetEvent.HunkMutiBundleAndRes)  then

           local  tmpMsg =  AssetRequesetMsg.New(AssetEvent.HunkMutiRes:ToInt(),msg.backId, msg.scenceName,  msg.bundleName, msg.resName,msg.isSingle ,msg.arrNumbers);

            this:SendMsg(tmpMsg);

        elseif (msg.msgId == LAssetEvent.ReleaseSingleObj)  then

              LuaLoadRes.Instance:UnloadResObj(msg.scenceName,msg.bundleName,msg.resName);

        elseif (msg.msgId == LAssetEvent.ReleaseBundleObjes)then
         
            LuaLoadRes.Instance:UnloadBundleObj(msg.scenceName,msg.bundleName);

        elseif (msg.msgId == LAssetEvent.ReleaseScenceObjes)then

                 LuaLoadRes.Instance:UnloadScenceObjes(msg.scenceName);


       elseif (msg.msgId == LAssetEvent.ReleaseBundleAndObjects)then
                 
                  print ("ReleaseBundleAndObjects ");
                  LuaLoadRes.Instance:UnLoadBundleAndRes(msg.scenceName,msg.bundleName);
      elseif (msg.msgId == LAssetEvent.ReleaseSingleBundle)then

                LuaLoadRes.Instance:UnloadSingleBundle(msg.scenceName,msg.bundleName);

      elseif (msg.msgId == LAssetEvent.ReleaseScenceBundle)then

                LuaLoadRes.Instance:UnloadScenceBundle(msg.scenceName);

       elseif (msg.msgId == LAssetEvent.ReleaseAll)then

                LuaLoadRes.Instance:UnloadAll(msg.scenceName);
        end

end


this:Awake();
