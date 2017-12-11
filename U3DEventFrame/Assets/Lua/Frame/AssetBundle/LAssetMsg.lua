--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion


 LAssetMsg = LMsgBase:New()

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LAssetMsg.__index = LAssetMsg

 local this = LAssetMsg;

--构造体，构造体的名字是随便起的，习惯性改为New()

 function LAssetMsg:New(msgid,scence,bundle,res,single,backFunc) 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
	
	
    setmetatable(self, LAssetMsg);  --将self的元表设定为Class

    self.msgId = msgid ;
    self.scenceName = scence;
    self.bundleName = bundle ;

    self.resName = res ;

    self.isSingle = single ;

    self.callBackFunc  = backFunc ;
    return self;    --返回自身
end

    function       LAssetMsg :ChangeResName(name)

       self.resName  = name ;
    end


    function       LAssetMsg :ChangeHunkRes(msgid,scence,bundle,res,single,backFunc)

     self.msgId = msgid ;
    self.scenceName = scence;
    self.bundleName = bundle ;

    self.resName = res ;

    self.isSingle = single ;

    self.callBackFunc  = backFunc ;
    end

   function       LAssetMsg :ChangeReleaseRes(msgid,scence,bundle,res)

             self.msgId = msgid ;
            self.scenceName = scence;
            self.bundleName = bundle ;

            self.resName = res ;

    
    end







