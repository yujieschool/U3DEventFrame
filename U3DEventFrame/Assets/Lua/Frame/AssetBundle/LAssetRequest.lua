--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion
LAssetRequest = LMsgBase:New()


--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LAssetRequest.__index = LAssetRequest

--构造体，构造体的名字是随便起的，习惯性改为New()


--    local   bundName = {
--                              "Scence100BackGround","Scence200BackGround","BagUI"
--                           };

--以下 和上面的bundle 个数对应起来 数值 和resNames 每个bundle 资源个数对应起来

--            local  nuberArrayes  = {

--            2,2,1
--      };

---  以下是一一对应关系

--     local  resNames = { "Scence101.prefab","Scence102.prefab", "Scence201.prefab","Scence202.prefab","BagUI0"} ;



--      local  singles = {

--            true,true,true,true,false
--      };


function LAssetRequest:New(backid, scenceName, bundName, resNames, singles, nuberArrayes)
    local self = { };
    -- 初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LAssetRequest);
    -- 将self的元表设定为Class

    self.msgId =  LAssetEvent.HunkMutiBundleAndRes ;
    self.backId = backid;

    self.scenceName = scenceName;

    self.bundleName = bundName;

    self.resName = resNames;
    self.isSingle = singles;
    self.arrNumbers = nuberArrayes;


    return self;
    -- 返回自身
end


function LAssetRequest:ChangeEvent(backid,scenceName, bundName, resNames, singles, nuberArrayes)

    self.backId = backid;

    self.scenceName = scenceName;

    self.bundleName = bundName;

    self.resName = resNames;
    self.isSingle = singles;
    self.arrNumbers = nuberArrayes;
end 

