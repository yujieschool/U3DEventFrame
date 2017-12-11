

print("LoadUIManager")

LUIManager =  LManagerBase:New();






--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LUIManager.__index = LUIManager

local this = LUIManager;
LUIManager.Objects = {}
local currentPanel = "";

function LUIManager:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LUIManager);  --将self的元表设定为Class

    return self;    --返回自身
end



-- 得到实例--
function LUIManager.GetInstance(name)
	
    currentPanel = name;
	return this ;
	
end


function   LUIManager.ProcessBackMsg()


      this:FrameUpdate();
end 


function LUIManager:ProcessEvent( msg)



      self:ProcessEvent2(msg);

end 




function LUIManager:SendMsg( msg,backMsgId)
	-- body
	

     self:ProcessSendBackMsg(backMsgId);

	if  msg:GetManager()  == LManagerID.LUIManager then

        self:ProcessEvent(msg);

    else

     LMsgCenter.SendToMsg(msg);
    --LMsgCenter.AnasysMsg(msg)
	end
end



--启动事件--
function LUIManager.Awake(obj)
	--gameObject = obj.gameObject;
	--transform = obj.transform;

    -- this.npcCount = 0 ;
	--this.InitPanel();
	--warn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function LUIManager.Init()
	
	  
	--this.btnOpen = transform:FindChild("Open").gameObject;
	--this.gridParent = transform:FindChild('ScrollView/Grid');
end

-- 销毁--
function LUIManager.OnDestroy()
	warn("OnDestroy---->>>");
end




function  LUIManager.RegistGameObject(tmpObj, panelName)
    if this.Objects[panelName] == nil then
        this.Objects[panelName] = {};
    end
          
   this.Objects[panelName][tmpObj.name] = tmpObj ;

     

end

 function  LUIManager.UnRegistGameObject(tmpObj, panelName)

    
   this.Objects[panelName][tmpObj.name] = nil ;



end

function  LUIManager.GetGameObject(panelName,objName)

    
     return  this.Objects[panelName][objName] ;

end



this:InitialTimer(this.ProcessBackMsg);




