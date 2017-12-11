


--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LNetBase = LMonoBase:New()

local this = LNetBase;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LNetBase.__index = LNetBase

--构造体，构造体的名字是随便起的，习惯性改为New()
function LNetBase:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LNetBase);  --将self的元表设定为Class

   self.msgIds = {};
    return self;    --返回自身
end


function  LNetBase:RegistSelf(npcScript,msgs)
	-- body

    -- print ("UIBase  Regist")

     LNetManager.GetInstance():RegistMsgs(npcScript,msgs);

end




function  LNetBase:UnRegistSelf(npcScript,msgs)
	-- body

     LNetManager.GetInstance():UnRegistMsgs(npcScript,msgs);

end



function  LNetBase:SendMsg(msg)
	-- body

    --  print ("UIBase  send ")
    LNetManager.GetInstance():SendMsg(msg);

end



function   LNetBase:GetRes(msgid,scenceName,bundleName,resName,singleRes, backFucn)

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

function   LNetBase:GetMutiRes(scenceName,bundleName,resName,singleRes, backFucn)

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


function   LNetBase:ReleaseRes(msgid, scenceName,bundleName,resName)


      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(msgid,scenceName,bundleName,resName,nil,nil);
     else 

         this.assetMsg:ChangeReleaseRes(msgid,scenceName,bundleName,resName);

      end 

     
      this:SendMsg( this.assetMsg);


end



function LNetBase:OnDestroy( )
	-- body
              this.assetMsg = nil   ;
   --print("manager OnDestroy");
	self:UnRegistMsgs(self,self.msgIds);
end