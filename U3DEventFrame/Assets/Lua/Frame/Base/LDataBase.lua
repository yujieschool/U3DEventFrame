--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion


--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LDataBase = LMonoBase:New()

local this = LDataBase;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LDataBase.__index = LDataBase

--构造体，构造体的名字是随便起的，习惯性改为New()
function LDataBase:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LDataBase);  --将self的元表设定为Class

   self.msgIds = {};
    return self;    --返回自身
end


function  LDataBase:RegistSelf(npcScript,msgs)
	-- body

     LDataManager.GetInstance():RegistMsgs(npcScript,msgs);

end




function  LDataBase:UnRegistSelf(npcScript,msgs)
	-- body

     LDataManager.GetInstance():UnRegistMsgs(npcScript,arg);

end



function  LDataBase:SendMsg(msg)
	-- body

     LDataManager.GetInstance():SendMsg(msg);

end


function   LDataBase:GetRes(msgid,scenceName,bundleName,resName,singleRes, backFucn)

      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(msgid,scenceName,bundleName,resName,singleRes,backFucn);

       else
            this.assetMsg.msgId = msgid;
            this.assetMsg.sceneName = scenceName;
            this.assetMsg.bundleName = bundleName;
            this.assetMsg.resName = resName;
            this.assetMsg.isSingle = singleRes;
            this.assetMsg.callBackFunc = backFucn;
     
      end 

     


     -- error ("UI base  resName=="..resName);
      self:SendMsg(this.assetMsg );


end

function   LDataBase:GetMutiRes(scenceName,bundleName,resName,singleRes, backFucn)

      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(LAssetEvent.HunkMutiRes,scenceName,bundleName,resName,singleRes,backFucn);
      else
            this.assetMsg.msgId = msgid;
            this.assetMsg.sceneName = scenceName;
            this.assetMsg.bundleName = bundleName;
            this.assetMsg.resName = resName;
            this.assetMsg.isSingle = singleRes;
            this.assetMsg.callBackFunc = backFucn;
      end 

 
      self:SendMsg(this.assetMsg );


end

function LDataBase:GetMutiBundlAndRes(backid, scenceName, bundName, resNames, singles, nuberArrayes)
if this.mutiABMsg == nil then


    this.mutiABMsg = LAssetRequest:New(backid, scenceName, bundName, resNames, singles, nuberArrayes);
else

    this.mutiABMsg:ChangeEvent(backid, scenceName, bundName, resNames, singles, nuberArrayes);
end


self:SendMsg(this.mutiABMsg);

end 





function   LDataBase:ReleaseRes(msgid, scenceName,bundleName,resName)


      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(msgid,scenceName,bundleName,resName,nil,nil);
     else 

         this.assetMsg:ChangeReleaseRes(msgid,scenceName,bundleName,resName);

      end 

     
      this:SendMsg( this.assetMsg);


end





function LDataBase:OnDestroy( )
	-- body

   
     this.assetMsg = nil ;
    
	self:UnRegistMsgs(self,self.msgIds);
end