

-- 一个类的申明
-- 声明，这里声明了类名还有属性，并且给出了属性的初始值。
LChoosePlayer = LUIBase:New()

local this = LChoosePlayer;

-- 这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LChoosePlayer.__index = LChoosePlayer;




-- 构造体，构造体的名字是随便起的，习惯性改为New()
function LChoosePlayer:New()
    local self = { };
    -- 初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LChoosePlayer);
    -- 将self的元表设定为Class

    self.msgIds = { };

    return self;
    -- 返回自身
end



------***********************----


-- 定义事件

-- 起始 消息 LDataEvent.MaxValue
LWordInfoEvent = {

    "GetWorldInfo",
    "MaxValue",
    GetWordInfo = 0,
    -- 单个字体

    MaxValue = 0,


}

ResetTableKeyValue(LDataEvent.MaxValue + 1, LWordInfoEvent);



------***********************----

-- 注册消息

function LuaClass.Regist()

    this.msgIds = { };

    this:RegistSelf(this, this.msgIds);

end 


this.Regist();

-----***************------
-- 申请一个资源

-- UIScene 场景名字 UIScene
-- 包名  ChoosePlayer
-- 资源名字   LChoosePlayer
-- 回调方法  this.InitialUIPrefable

this:GetRes(LAssetEvent.HunkRes, "UIScene", "ChoosePlayer", "LChoosePlayer", true, this.InitialUIPrefable);


-- 申请多个资源

mutiRes = {
    "LChoosePlayer1",
    "LChoosePlaye2",
    "LChoosePlaye3",
}

-- function  GetObject(tmpTable,name)

--       for i=1 , table.getn(tmpTable) do

--           if (tmpTable[i] == name)   then

--            return   i ;
--            end


--        end

--        return  0 ;


-- end 

function LChoosePlayer.InitialUIPrefable(scence, bundle, reses, objes)

    -- LChoosePlayer1==     objes[1] ;

end 
  

this:GetRes(LAssetEvent.HunkRes,"UIScene","ChoosePlayer",mutiRes,true,this.InitialUIPrefable);



-----***************------
 --- 申请多个bundle 里面多个资源

-- bundle 对应的名字
local bundle = {
    "Scence100BackGround","Scence200BackGround","BagUI"
};

-- 以下 和上面的bundle 个数对应起来 数值 和resNames
-- 表示每个bundle 资源请求的个数   对应起来
local numbers = {

    2,2,1
};

---  以下两个数组是一一对应关系
-- 一个一维数组表示每个bundle 里面资源的名字


------- -----------------------------这里面要加后缀 .prefab   .png----------BagUI0多个情况不用加----------
local resName = { "Scence101.prefab","Scence102.prefab",
  "Scence201.prefab",  "Scence202.prefab",
   "BagUI0"
    };
     

---- ture 表示 单个 false 表示多个
local singles = {

    true,true,
    true,true,
    false
};


--- 第一参数 表示  回调的你要接收的消息 
    
this:GetMutiBundlAndRes(LManagerID.LUIManager + 2, "UIScene", bundle, resName, singles, numbers);


------接收到回调消息
-- 第一个参数表示 哪个bundle  
-- 第二个参数  表示 哪个资源名字   
      local  tmpObj= msg:GetBundleRes("Scence200BackGround", "Scence201.prefab");


     --　这个地方不用释放了 
----用完了　　要释放
      --tmpObj:Dispose();


     local  tmpGame  GameObject.Instantiate(tmpObj[0]);

       --查找物体

       Canvas = GameObject.Find("UIScaler");

       tmpGame.transform:SetParent(Canvas.transform);


-----***************------
-- 释放资源
-- UIScene 场景名字 UIScene
-- 包名  ChoosePlayer

this:ReleaseRes(LAssetEvent.ReleaseBundleAndObjects, "UIScene", "ChoosePlayer", nil);


-----***************------


-- 遍历table 数组

for i = 1, table.getn(tmpTable) do

    print(tmpTable[i])

end 

  -- 遍历table 字典类型
	 for  k ,v  in pairs(self.eventTree) do
	  
	 	 
	 end


-- 插入操作

table.insert(table, "value")
-- 删除操作
-- 最后一个元素
table.remove(table)



----***************----


--- 得到字体

--  返回msgid   LUIEventChoosePlayerMsg.GetWordInfo

--  wordid= tmpMutiWord  必须为一个数组    
local tmpMutiWord = { "UserName", "CreateCharacter2", "CreateCharacter1", "EnterUsername" }
local getWord = LWorlInfoMsg:New();
getWord:ChangeSendMsg(LUIEventChoosePlayerMsg.GetWordInfo, tmpMutiWord);
this:SendMsg(getWord);

-- 得到字体的使用方式
this.data = msg.word;

local creatRoleTile = this:GetText(transform.name, "CreatRoleTitle");
creatRoleTile.text = this.data["UserName"];
       
-----*************------


--- UI 的一些事件  在UIBase 里 查看
--- transform.name panel 名字

-- 控件名字 CreatRole   UserInput  Man  defaultInput

-- 返回函数   this.CreatPlayerButton    this.UserInput    this.SelectMan
this:AddButtonLisenter(transform.name, "CreatRole", this.CreatPlayerButton);

this:AddInputFiledLisenter(transform.name, "UserInput", this.UserInput);

this:AddToggleLisenter(transform.name, "Man", this.SelectMan);

this:GetText(transform.name, "defaultInput");


 -----*************------
--lua  给C# 发送  消息
local  manMsg=   MsgMutiString.New(PoleGenEvent.PersonChangePersonSex:ToInt(),"Woman");
this:SendMsg(manMsg)


-----*************------

-- 实例化物体
gameObject = GameObject.Instantiate(obj);

-- 添加一个component
gameObject:AddComponent(typeof(LuaUIPanel));

Canvas = GameObject.FindGameObjectWithTag("MainCanvas");

-----*************------
-- transform 操作
transform = GameObject.Instantiate(obj).transform;
transform:SetParent(Canvas.transform);


-----*************- ma   net-----
-- 请求网络数据
local create = FishLNet_pb.request1003();
create.handlerId = tonumber(handlerId);
create.token = token;
create.flag = tostring(flag);
local msg = create:SerializeToString();
testMsg = LMsgBase:New(LTCPEvent.SendMsg);
testMsg.netId = 1003;
testMsg.data = msg;
this:SendMsg(testMsg);


-- 接收处理网络消息数据
local tmpMsg = FishLNet_pb.response1003();
tmpMsg:ParseFromString(msg.data);
local roleEquipCount = table.getn(tmpMsg.roleEquip)
local roleEquip = tmpMsg.roleEquip

-- 遍历table
for i, v in ipairs(roleEquip) do
    error(v.id)
    error(v.equipId)
end


---------------UI
-- 设置topbar位于同级最后
transform:SetAsLastSibling();

-- 设置topbar位于同级最前
transform:SetAsFirstSibling();
 
-- 设置topbar位于同级Index
transform:SetSiblingIndex(index);
 

-- 给图片赋值
itemfishUI:FindChild("item_star/starimgone"):GetComponent("Image").sprite = this.SaveSprite.GloboBackground["StarIcon"]

LUIManager.GetInstance("AquariumUI").GetText("tx_AquarumName")
LUIManager.GetInstance("AquariumUI").GetImage("fishDetailBg")

-- 设置物体宽高
gameObject:GetComponent("RectTransform").sizeDelta = Vector2.New(270, heightPanle);

-- 设置Image 属性
itemBag:FindChild("bagDialogItemIcon/bagDialogItemIconLock").gameObject:GetComponent("Image").raycastTarget = false;

-- 动画播放 进入界面  TweenPosition 
this.ScriptTools.LeftBagPosition1 = LUIManager.GetInstance("AquariumUI").AddComponent("bagDialog", typeof(TweenPosition));
-- 进入界面
this.ScriptTools.LeftBagPosition1.FowardCurve = this.ScriptTools.LeftBagPosition1:GetCurve("line");
this.ScriptTools.LeftBagPosition1:PlayFoward(0);
-- 退出界面
this.ScriptTools.LeftBagPosition1.FowardCurve = this.ScriptTools.LeftBagPosition1:GetCurve("line");
this.ScriptTools.LeftBagPosition1:PlayReverse(0);

-- 弹窗的动画效果
this.ScriptTools.SaleFishDialog = LUIManager.GetInstance("AquariumUI").AddComponent("selAllFishDialogMask", typeof(TweenScale));
this.ScriptTools.SaleFishDialog.FowardCurve = this.ScriptTools.SaleFishDialog:GetCurve("oneBigOne");
LUIManager.GetInstance("AquariumUI").GetTransform("selAllFishDialogMask").gameObject:SetActive(false);
-- 弹出框
this.ScriptTools.SaleFishDialog.gameObject:SetActive(true);
this.ScriptTools.SaleFishDialog:PlayFoward(0);
-- 关闭框
this.ScriptTools.SaleFishDialog:PlayReverse(0);
Invoke(this.WaitCloseFishDialog, 0.3);
function AquariumUI.WaitCloseFishDialog()
    this.ScriptTools.SaleFishDialog.gameObject:SetActive(false);
end

--属性
local capacity = 0;
LPlayerProperty.Capacity = {
    get = function()
		return capacity;
	end,
	set = function(value)
        
        capacity = value;

		if this.isInit then
			this.UpdateValue("capacity", value);
		end
		
	end

--毫秒转换为日期
local tab = os.date("*t", this.FishInfo.FishList[index].GotTime/1000);     
error(tab.year.."   "..tab.month.."  "..tab.day)

--取整
minute = math.modf(second/60);

-----*************- ma  end -----

-----*************- zhang  UI-----
-- 改变字体颜色
ChangeColor.ColorString(this.PhysicalValue.get(), "#F5D78CFF")
-- 字体颜色渐变
this.Variable.MoneyTextColor = moneyText.gameObject:AddComponent(typeof(Gradient));
this.Variable.MoneyTextColor:TopCor(241, 237, 212, 222);
this.Variable.MoneyTextColor:BottomCor(243, 204, 117, 255);

-- 设置TopBar自由条最前显示
local tempMsg = LMsgBase:New()
tempMsg = LMsgBase:New(LUIEventChooseLvlMsg.SetTopBarLayer);
this:SendMsg(tempMsg);
-- 设置TopBar自由条最后显示
tempMsg = LMsgBase:New(LUIEventChooseLvlMsg.UnSetTopBarLayer);
this:SendMsg(tempMsg);
-- 设置TopBar自由条位于某个index
tempMsg = LMsgBase:New(LUIEventChooseLvlMsg.SetTopBarLayerIndex);
tempMsg.TopBarLayer= index;
this:SendMsg(tempMsg);
-- 设置序列帧动画
effect_Bet = this:AddComponent(lGUIMainScene.name, "BetEffect1", typeof(SerialFramesManager));
effect_Bet.playSupporter = SerialFramesManager.ForType.Image;
effect_Bet:SetPicturesCount(1);
effect_Bet.pictures[0] = Streamer_2;
effect_Bet.playSpeed = 30;
effect_Bet.interval = 1.2;
effect_Bet.horizontalCount = 6;
effect_Bet.verticalCount = 5;
-----*************- zhang   End-----



-----*************- zhao  coroutine-----

function TestCoroutine(str)
	print("from TestCoroutine  str == " .. str);
end


Invoke(this.StopLoading,0.5);
Invoke(class.func,time)         --定时器  启动一个方法
co = coroutine.create(TestCoroutine);		--创建协程

coroutine.resume(co, "haha");	--开启协程，传参数


coroutine.yield(co);		--挂起协程


-----*************- zhao   End-----



-----*************- zheng   UI-----
    --提示弹窗用法
    local tempMsg = LMsgBase:New(); 
    tempMsg.msgId = LUIEventRankingList.ShowCommonTips;
    tempMsg.tipsStr = "游戏币不足，请及时购买";
    tempMsg.autoClose = 3; --默认2秒后自动关闭   0及以下不自动关闭
    tempMsg.callbackID = LUIEventRankingList.ShowComonTips; --要回调的消息号  不需要不填写
    this:SendMsg(tempMsg);

-----*************- zheng   end-----


