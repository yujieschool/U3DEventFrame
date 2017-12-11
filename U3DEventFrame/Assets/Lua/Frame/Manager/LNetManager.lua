




LNetManager =  LManagerBase:New();





local this = LNetManager;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LNetManager.__index = LNetManager




function LNetManager:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LNetManager);  --将self的元表设定为Class

    return self;    --返回自身
end


-- 得到实例--
function LNetManager.GetInstance()
	
	return this ;
	
end




function LNetManager:SendMsg( msg)
	-- body

	if  msg:GetManager()  == LManagerID.LNetManager then


      self:ProcessEvent(msg);


    else

         LMsgCenter.SendToMsg(msg);
    
	end
end




--启动事件--
function LNetManager.Awake(obj)
	--gameObject = obj.gameObject;
	--transform = obj.transform;

    -- this.npcCount = 0 ;
	--this.InitPanel();
	--warn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function LNetManager.Init()
	
	  
	--this.btnOpen = transform:FindChild("Open").gameObject;
	--this.gridParent = transform:FindChild('ScrollView/Grid');
end

-- 销毁--
function LNetManager.OnDestroy()
	warn("OnDestroy---->>>");
end

