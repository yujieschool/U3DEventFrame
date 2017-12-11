--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
require "Logic.UI.Registing.LRegisting"


--endregion

--- 这里可以添加 当前每个人  干的 活的 分类
--声明，这里声明了类名还有属性，并且给出了属性的初始值。
LLoading  =  LUIBase:New()

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LLoading.__index = LLoading

local   this = LLoading 

--构造体，构造体的名字是随便起的，习惯性改为New()
function LLoading:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LLoading);  --将self的元表设定为Class

    return self;    --返回自身
end
 



function   LLoading:Initial()


-- bundle 对应的名字
local bundle = {
    "Loading","Registing"
};

-- 以下 和上面的bundle 个数对应起来 数值 和resNames
-- 表示每个bundle 资源请求的个数   对应起来
local numbers = {

    1,1
};

---  以下两个数组是一一对应关系
-- 一个一维数组表示每个bundle 里面资源的名字


------- -----------------------------这里面要加后缀 .prefab   .png----------BagUI0多个情况不用加----------
local resName = { "Loading.prefab",
"Reginst.prefab"
    };
     

---- ture 表示 单个 false 表示多个
local singles = {

    true,true,
};


--- 第一参数 表示  回调的你要接收的消息 
    
this:GetMutiBundlAndRes(LUILoadingEvent.Loading, "LuaScence", bundle, resName, singles, numbers);



end 



function LLoading.RegistBtn()


    print("Regist  Btn");

    local  tmpMsg=  LMsgBase:New( LUIRegistingEvent.Initial);

    this:SendMsg(tmpMsg);

   -- LRegisting.Regist();


end 

function LLoading:ProcessEvent(msg)

    if msg.msgId == LUILoadingEvent.Loading then

           local  tmpObj= msg:GetBundleRes("Loading", "Loading.prefab");


           self:InstantiatePlaneGameObje(tmpObj[0]);


           this:AddButtonLisenter("Regist",this.RegistBtn);
           -- tmpGame.transform.localPosition  =  Vector3.New(518,0,0)

    end 




    end 





    function  LLoading : Release()

    this:ReleaseRes(LAssetEvent.ReleaseBundleAndObjects, "LuaScence", "Loading");

    this:ReleaseRes(LAssetEvent.ReleaseBundleAndObjects, "LuaScence", "Registing");

    end 



    function  LLoading:Regist ()


    this.msgIds = {
        LUILoadingEvent.Loading
    };

    this:RegistSelf(this, this.msgIds);


    this:Initial();

end 

this:Regist()