


local transform;
local gameObject;

LNPCManager =  LManagerBase:New();



local this = LNPCManager;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LNPCManager.__index = LNPCManager




function LNPCManager:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LNPCManager);  --将self的元表设定为Class

    return self;    --返回自身
end

-- 得到实例--
function LNPCManager.GetInstance()
	
	return this ;
	
end




function LNPCManager:SendMsg( msg)
	-- body


    -- print("LNPCManager:SendMsg ==="..msg.msgId);
	if  msg:GetManager()  == LManagerID.LNPCManager then


      self:ProcessEvent(msg);


    else

        --  print (" LNPCManager:SendMsg =="..msg.msgId)
         LMsgCenter.SendToMsg(msg);
    
	end
end




--启动事件--
function LNPCManager.Awake(obj)
	--gameObject = obj.gameObject;
	--transform = obj.transform;

    -- this.npcCount = 0 ;
	--this.InitPanel();
	--warn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function LNPCManager.Init()
	
	  
	--this.btnOpen = transform:FindChild("Open").gameObject;
	--this.gridParent = transform:FindChild('ScrollView/Grid');
end

-- 销毁--
function LNPCManager.OnDestroy()
	warn("OnDestroy---->>>");
end