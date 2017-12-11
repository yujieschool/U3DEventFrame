
--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LLRegisting = LUIBase:New()

local this = LLRegisting;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LLRegisting.__index = LLRegisting ;



local transform = nil;
local gameObject = nil;


--构造体，构造体的名字是随便起的，习惯性改为New()
function LLRegisting:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LLRegisting);  --将self的元表设定为Class

   self.msgIds = {};
   
    return self;    --返回自身
end


 function  LLRegisting.Awake(object)
    -- body

    gameObject = object.gameObject;
    transform = object.transform;

--   这里面 就干这么多事 不要加其它的 ;

end


function  LLRegisting.Regist()

         this.msgIds = {
        UIDemoRegistEvents.GetResources,

         UIDemoRegistEvents.Initial

       };

       this:RegistSelf(this,this.msgIds);

end 


this.Regist();






function LLRegisting:ReleaseRes()
	-- body
   this:ReleaseRes(LAssetEvent.ReleaseBundleAndObjects, "LuaScence", "ChoosePlayer", nil);

end

function   LLRegisting:GetResoures()

-- bundle 对应的名字
local bundle = {
    "Regist"
};

-- 以下 和上面的bundle 个数对应起来 数值 和resNames
-- 表示每个bundle 资源请求的个数   对应起来
local numbers = {

    1
};

---  以下两个数组是一一对应关系
-- 一个一维数组表示每个bundle 里面资源的名字


------- -----------------------------这里面要加后缀 .prefab   .png----------BagUI0多个情况不用加----------
local resName = {
"Regist.prefab"
    };
     

---- ture 表示 单个 false 表示多个
local singles = {

    true
};


--- 第一参数 表示  回调的你要接收的消息 
    
this:GetMutiBundlAndRes( UIDemoRegistEvents.GetResources, "Demo", bundle, resName, singles, numbers);



end 
--  执行 退出 消除逻辑
 function  LLRegisting:Exist()
    
           transform = nil;
           gameObject = nil;
           this = nil ;

end


 function  LLRegisting:JumpNextView()
    
   local   testMsg = LMsgBase:New(LTCPEvent.SendMsg);

   SendMsg(testMsg)

   testMsg:ChangeEventId(LTCPEvent.RecvMsg)

   SendMsg(testMsg);

end


--  进行 重新设置值
 function  LLRegisting:Reset()
    


end


-- 外界的入口  进行初始化操作
 function  LLRegisting:Initial()
    


end



function  LLRegisting.DoneBtnPress ()


     print(" LLRegisting.DoneBtnPress  ");

end 

-- 这里面 就是做一些 事件的处理
function  LLRegisting:ProcessEvent(msg)

      
         if   msg.msgId  ==   UIDemoRegistEvents.GetResources  then


              local  tmpObj = msg:GetBundleRes("Regist", "Regist.prefab");

                this: InstantiatePlaneGameObje(tmpObj[0])


                this:AddButtonLisenter("Done",this.DoneBtnPress);

         elseif    msg.msgId  ==  UIDemoRegistEvents.Initial   then

                this: GetResoures();

           
         end 

end