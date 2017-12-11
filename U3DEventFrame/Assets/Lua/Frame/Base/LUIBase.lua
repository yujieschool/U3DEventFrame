



-- 声明，这里声明了类名还有属性，并且给出了属性的初始值。
LUIBase = LMonoBase:New()



-- 这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LUIBase.__index = LUIBase


local this = LUIBase;

this.panelName= nil ;

-- 构造体，构造体的名字是随便起的，习惯性改为New()
function LUIBase:New()
    local self = { };
    -- 初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LUIBase);
    -- 将self的元表设定为Class

    self.msgIds = { };

   
    return self;
    -- 返回自身
end


function LUIBase:RegistSelf(npcScript, msgs)
    -- body

    -- print ("UIBase  Regist")

    LUIManager.GetInstance():RegistMsgs(npcScript, msgs);

end




function LUIBase:UnRegistSelf(npcScript, msgs)
    -- body

    LUIManager.GetInstance():UnRegistMsgs(npcScript, msgs);

end


function LUIBase:GetRes(msgid, scenceName, bundleName, resName, singleRes, backFucn)

    if this.assetMsg == nil then


        this.assetMsg = LAssetMsg:New(msgid, scenceName, bundleName, resName, singleRes, backFucn);

    else
        this.assetMsg.msgId = msgid;
        this.assetMsg.sceneName = scenceName;
        this.assetMsg.bundleName = bundleName;
        this.assetMsg.resName = resName;
        this.assetMsg.isSingle = singleRes;
        this.assetMsg.callBackFunc = backFucn;

    end




    -- error ("UI base  resName=="..resName);
    self:SendMsg(this.assetMsg);


end

function LUIBase:GetMutiRes(scenceName, bundleName, resName, singleRes, backFucn)

    if this.assetMsg == nil then


        this.assetMsg = LAssetMsg:New(LAssetEvent.HunkMutiRes, scenceName, bundleName, resName, singleRes, backFucn);
    else
        this.assetMsg.msgId = msgid;
        this.assetMsg.sceneName = scenceName;
        this.assetMsg.bundleName = bundleName;
        this.assetMsg.resName = resName;
        this.assetMsg.isSingle = singleRes;
        this.assetMsg.callBackFunc = backFucn;
    end


    self:SendMsg(this.assetMsg);


end

---  
function LUIBase:GetMutiBundlAndRes(backid, scenceName, bundName, resNames, singles, nuberArrayes)
if this.mutiABMsg == nil then

    this.mutiABMsg = LAssetRequest:New(backid, scenceName, bundName, resNames, singles, nuberArrayes);
else
    this.mutiABMsg:ChangeEvent(backid, scenceName, bundName, resNames, singles, nuberArrayes);
end

self:SendMsg(this.mutiABMsg);

end 



function  LUIBase:InstantiatePlaneGameObje( tmpObj)


 local   tmpGame =  GameObject.Instantiate(tmpObj);

 tmpGame.transform:SetParent(Canvas.transform,false);



 tmpGame.name = string.gsub(tmpGame.name,"%(Clone%)","");

  this.panelName= tmpGame.name ;


  LUIManager.GetInstance().RegistGameObject(tmpGame,this.panelName);


    tmpGame: AddComponent(typeof(LuaUIPanel)) ;
  
 
 return  tmpGame ;

end 




function LUIBase:ReleaseRes(msgid, scenceName, bundleName, resName)


    if this.assetMsg == nil then


        this.assetMsg = LAssetMsg:New(msgid, scenceName, bundleName, resName, nil, nil);
    else

        this.assetMsg:ChangeReleaseRes(msgid, scenceName, bundleName, resName);

    end


    self:SendMsg(this.assetMsg);


end




function LUIBase:SendMsg(msg,backId)
    -- body
    ---  print ("UIBase  send 11=="..msg.msgId)
    --print ("UIBase  send 222=="..backId)
    LUIManager.GetInstance():SendMsg(msg,backId);

end





function LUIBase:OnDestroy()
    -- body

    if this.assetMsg ~= nil then
        this.assetMsg = nil;
    end

    -- print("manager OnDestroy");
    self:UnRegistMsgs(self, self.msgIds);
end



local showTipsMsg = nil
function LUIBase:ShowTips(showStr,autoClose)
    if showTipsMsg == nil then
        showTipsMsg = LMsgBase:New(LUIEventRankingList.ShowCommonTips);
    end

    if autoClose == nil or tonumber(autoClose) == "" then
        autoClose = 1;
    end

    showTipsMsg.autoClose = autoClose
    showTipsMsg.tipsStr = showStr;

    this:SendMsg(showTipsMsg);
end

function LUIBase:CollectgarbageLua()
    error("内存为        =====:"..collectgarbage("count"))
    collectgarbage("collect")--为了有干净的环境，先把可以收集的垃圾收集了
    collectgarbage()--为了保证内存的收集的相对干净，及内存的稳定，要执行多次收集
    error("now Lua内存为 =====:"..collectgarbage("count"))
end



function LUIBase:CreatObject(obj, parent)
    local tempObj = nil;
    if obj == nil then
        tempObj = GameObject.New().transform;
    else
        tempObj = newobject(obj).transform;
    end
    tempObj.name = obj.name;
    if parent ~= nil then
        tempObj:SetParent(parent,false);
    end
    

    ResetTransform(tempObj);
    return tempObj.gameObject;
end


function LUIBase:AddGradient(obj, r, g, b, a, r1, g1, b1, a1)
    local gra = obj:GetComponent("Gradient");
    if gra == nil then
        gra = obj:AddComponent(typeof(Gradient));
    end

    gra:TopCor(r, g, b, a);
    gra:BottomCor(r1, g1, b1, a1);
end



function  LUIBase:GetGameObject(panelName,objName)
  
     return  LUIManager.GetInstance().GetGameObject(panelName,objName);

end 


function  LUIBase:AddButtonLisenter(objName,action)

        
         local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

               luaBehave:AddButtonLisenter(action)
  
           end 
end

function  LUIBase:AddButtonLisenterOne(objName,action, obj1)


         local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddButtonLisenter(action, obj1)
  
           end 



end

function  LUIBase:AddButtonLisenterTwo(objName,action, obj1, obj2)


         local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddButtonLisenter(action, obj1, obj2)
  
           end 


end


function  LUIBase:AddButtonUpLisenter(objName,action)

  
            local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddButtonUpLisenter(action)
  
           end 

end


function  LUIBase:AddButtonDownLisenter(objName,action)


  
            local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddButtonDownLisenter(action)
  
           end 




end


function  LUIBase:AddSliderLisenter(objName,action)



            local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddSliderLisenter(action)
  
           end 


end

function  LUIBase:AddInputFiledLisenter(objName,action,endEditAction)

   

               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddInputFiledLisenter(action,endEditAction)
  
           end 


end


function  LUIBase:AddToggleLisenter(objName,action)



               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddToggleLisenter(action)
  
           end 

end

function  LUIBase :AddDropDownLisenter(objName,action)



               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                luaBehave:AddDropDownLisenter(action)
  
           end 


end

function  LUIBase:SetImageSprite(objName, sprite)


               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                return luaBehave:SetImageSprite(sprite);
  
           end 
           return nil;

end

function  LUIBase:GetTransform(objName)




               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

                return luaBehave:GetTransform();
  
           end 

	return nil;
end


function LUIBase:GetButton(objName)


               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

              return luaBehave:GetButton();
  
           end 

	return nil;

end


function LUIBase:AddComponent(objName, classType)

 local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

    if (tmpObj ~= nil) then

        return tmpObj:AddComponent(classType);
  
    end 
	return nil;

end


function  LUIBase:GetText(objName)




               local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

            if (tmpObj ~= nil) then

            local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

              return luaBehave:GetText();
  
           end 


	return nil;
end

function  LUIBase:GetImage(name)




    local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

    if (tmpObj ~= nil) then

    local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

    return luaBehave:GetImage();
  
    end 

	return nil;
end

function LUIBase:SetInteractable(objName, value)



    local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

    if (tmpObj ~= nil) then

    local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

    return luaBehave:SetInteractable(value);
  
    end 

	return nil;

end


function LUIBase:GetTextWidth(objName, str)


    local   tmpObj = self:GetGameObject(this.panelName,objName)   ;

    if (tmpObj ~= nil) then

    local   luaBehave= tmpObj:GetComponent("LuaUIBehaviour");

    return  luaBehave:GetTextWidth(str);
  
    end 

	return nil;


end



