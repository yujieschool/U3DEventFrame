




LAudioManager =  LManagerBase:New();



local this = LAudioManager;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LAudioManager.__index = LAudioManager




function LAudioManager:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LAudioManager);  --将self的元表设定为Class

    return self;    --返回自身
end



-- 得到实例--
function LAudioManager.GetInstance()
	
	return this ;
	
end




function LAudioManager:SendMsg( msg)
	-- body

	if  msg:GetManager()  == LManagerID.LAudioManager then


      self:ProcessEvent(msg);


    else

         LMsgCenter.SendToMsg(msg);
    
	end
end




--启动事件--
function LAudioManager.Awake(obj)
	--gameObject = obj.gameObject;
	--transform = obj.transform;

    -- this.npcCount = 0 ;
	--this.InitPanel();
	--warn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function LAudioManager.Init()
	
	  
	--this.btnOpen = transform:FindChild("Open").gameObject;
	--this.gridParent = transform:FindChild('ScrollView/Grid');
end

-- 销毁--
function LAudioManager.OnDestroy()
	warn("OnDestroy---->>>");
end

