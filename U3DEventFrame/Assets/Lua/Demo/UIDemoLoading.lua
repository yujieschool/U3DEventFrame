

require  "Demo.LLoadingCtrl"


--声明，这里声明了类名还有属性，并且给出了属性的初始值。
UIDemoLoading = LUIBase:New()

local this = UIDemoLoading;

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
UIDemoLoading.__index = UIDemoLoading ;



local transform = nil;
local gameObject = nil;


--构造体，构造体的名字是随便起的，习惯性改为New()
function UIDemoLoading:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, UIDemoLoading);  --将self的元表设定为Class

   self.msgIds = {};
   
    return self;    --返回自身
end


 function  UIDemoLoading.Awake(object)
    -- body

    gameObject = object.gameObject;
    transform = object.transform;

--   这里面 就干这么多事 不要加其它的 ;

end



function   UIDemoLoading:GetResoures()

-- bundle 对应的名字
local bundle = {
    "Loading"
};

-- 以下 和上面的bundle 个数对应起来 数值 和resNames
-- 表示每个bundle 资源请求的个数   对应起来
local numbers = {

    1
};

---  以下两个数组是一一对应关系
-- 一个一维数组表示每个bundle 里面资源的名字


------- -----------------------------这里面要加后缀 .prefab   .png----------BagUI0多个情况不用加----------
local resName = { "Loading.prefab",

    };
     

---- ture 表示 单个 false 表示多个
local singles = {

    true
};


--- 第一参数 表示  回调的你要接收的消息 
    
this:GetMutiBundlAndRes( UIDemoLoadingEvents.GetResources, "Demo", bundle, resName, singles, numbers);



end 




function  UIDemoLoading.Regist()



              
         
       print("loading ===="..tonumber( UIDemoLoadingEvents.GetResources));
         this.msgIds = {
        UIDemoLoadingEvents.GetResources,
        UIDemoLoadingEvents.Initial,
        UIDemoLoadingEvents.TestMsg
       };

       this:RegistSelf(this,this.msgIds);



       this: GetResoures();


end 


this.Regist();




function UIDemoLoading.ReleaseRess()
	-- body


    print ("release  coming!!!")
   this:ReleaseRes(LAssetEvent.ReleaseBundleAndObjects, "LuaScence", "Loading", nil);

end


--  执行 退出 消除逻辑
 function  UIDemoLoading:Exist()
    
           transform = nil;
           gameObject = nil;
           this = nil ;

end


 function  UIDemoLoading:JumpNextView()
    
   local   testMsg = LMsgBase:New(LTCPEvent.SendMsg);

   SendMsg(testMsg)

   testMsg:ChangeEventId(LTCPEvent.RecvMsg)

   SendMsg(testMsg);

end


--  进行 重新设置值
 function  UIDemoLoading:Reset()
    


end


-- 外界的入口  进行初始化操作
 function  UIDemoLoading:Initial()
    


end



function   UIDemoLoading.RegistBtnPress( tmpObj)


      print("Regist btn press"..tmpObj.name);

        local   testMsg = LMsgBase:New(UIDemoRegistEvents.Initial);

     this:SendMsg(testMsg)


   


       

end  


--region 
function     UIDemoLoading.SettingBtn()

   --LMsgBase:New()

      print ("setting btn Coming!!!"..tonumber( CUIDemoMsgEvent.DemoMsg))
       local   testMsg = MsgBase.New(tonumber( CUIDemoMsgEvent.DemoMsg));

      
     this:SendMsg(testMsg)


       this.ReleaseRess();

end

--end region

-- 这里面 就是做一些 事件的处理
function  UIDemoLoading:ProcessEvent(msg)


          print("loading ===="..tonumber( msg.msgId));
      if   msg.msgId ==  UIDemoLoadingEvents.GetResources  then


      --AssetResponseMsg     Resource.load

                   local  tmpObj = msg:GetBundleRes("Loading", "Loading.prefab");

                  this.gameObject= this: InstantiatePlaneGameObje(tmpObj[0])

                   


                  LLoadingCtrl:SettingParent(this);

                 this: AddButtonLisenter("Registing",UIDemoLoading.RegistBtnPress);

                
                  this: AddButtonLisenter("Setting",UIDemoLoading.SettingBtn);

          
          elseif  msg.msgId ==  UIDemoLoadingEvents.TestMsg  then



           print (" lua  recv  msg")

      end 


     

end