



--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LAudioBase = LMonoBase:New()

local this = LAudioBase;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LAudioBase.__index = LAudioBase

--构造体，构造体的名字是随便起的，习惯性改为New()
function LAudioBase:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LAudioBase);  --将self的元表设定为Class

   self.msgIds = {};
    return self;    --返回自身
end


function  LAudioBase:RegistSelf(npcScript,msgs)
	-- body

     LAudioManager.GetInstance():RegistMsgs(npcScript,msgs);

end




function  LAudioBase:UnRegistSelf(npcScript,msgs)
	-- body

     LAudioManager.GetInstance():UnRegistMsgs(npcScript,msgs);

end



function  LAudioBase:SendMsg(msg)
	-- body

     LAudioManager.GetInstance():SendMsg(msg);

end

     function   LAudioBase:GetRes(msgid,scenceName,bundleName,resName,singleRes, backFucn)

      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(msgid,scenceName,bundleName,resName,singleRes,backFucn);
     
      end 
     
      this:SendMsg(this.assetMsg );


end


function   LAudioBase:ReleaseRes(msgid, scenceName,bundleName,resName)


      if this.assetMsg == nil  then
          

       this.assetMsg   = LAssetMsg:New(msgid,scenceName,bundleName,resName,nil,nil);
     else 

         this.assetMsg:ChangeReleaseRes(msgid,scenceName,bundleName,resName);

      end 

     
      this:SendMsg( this.assetMsg);


end


function LAudioBase:GetMutiBundlAndRes(backid, scenceName, bundName, resNames, singles, nuberArrayes)

if this.mutiABMsg == nil then


    this.mutiABMsg = LAssetRequest:New(backid, scenceName, bundName, resNames, singles, nuberArrayes);
else

    this.mutiABMsg:ChangeEvent(backid, scenceName, bundName, resNames, singles, nuberArrayes);
end


self:SendMsg(this.mutiABMsg);

end 




function LAudioBase:OnDestroy( )
	-- body

     this.assetMsg  = nil ;
	self:UnRegistMsgs(self,self.msgIds);
end