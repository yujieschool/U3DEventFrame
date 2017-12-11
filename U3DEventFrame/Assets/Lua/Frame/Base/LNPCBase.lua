
--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LNPCBase = LMonoBase:New()

local this = LNPCBase;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LNPCBase.__index = LNPCBase

--构造体，构造体的名字是随便起的，习惯性改为New()
function LNPCBase:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LNPCBase);  --将self的元表设定为Class

   self.msgIds = {};
    return self;    --返回自身
end


function  LNPCBase:RegistSelf(npcScript,msgs)
	-- body

     LNPCManager.GetInstance():RegistMsgs(npcScript,msgs);

end




function  LNPCBase:UnRegistSelf(npcScript,msgs)
	-- body

     LNPCManager.GetInstance():UnRegistMsgs(npcScript,msgs);

end



function  LNPCBase:SendMsg(msgs)
	-- body

     
     LNPCManager.GetInstance():SendMsg(msgs);

end


function   LNPCBase:GetRes(msgid,scenceName,bundleName,resName,singleRes, backFucn)

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

function   LNPCBase:GetMutiRes(scenceName,bundleName,resName,singleRes, backFucn)

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


function LNPCBase:GetMutiBundlAndRes(backid, scenceName, bundName, resNames, singles, nuberArrayes)

if this.mutiABMsg == nil then


    this.mutiABMsg = LAssetRequest:New(backid, scenceName, bundName, resNames, singles, nuberArrayes);
else

    this.mutiABMsg:ChangeEvent(backid, scenceName, bundName, resNames, singles, nuberArrayes);
end


self:SendMsg(this.mutiABMsg);

end 



function   LNPCBase:ReleaseRes(msgid, scenceName,bundleName,resName)


      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(msgid,scenceName,bundleName,resName,nil,nil);
     else 

         this.assetMsg:ChangeReleaseRes(msgid,scenceName,bundleName,resName);

      end 

      --   print("LNPCBase:ReleaseRes ==="..msgid);
      this:SendMsg( this.assetMsg);


end
function LNPCBase:OnDestroy( )
	-- body
    this.assetMsg = nil   ;

	self:UnRegistMsgs(self,self.msgIds);
end