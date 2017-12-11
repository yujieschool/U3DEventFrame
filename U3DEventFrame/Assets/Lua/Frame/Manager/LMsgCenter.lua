

print("LoadMsgCenter")




LMsgCenter = {}



--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
LMsgCenter.__index = LMsgCenter


local  this = LMsgCenter

--构造体，构造体的名字是随便起的，习惯性改为New()
function LMsgCenter:New() 
    local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
    setmetatable(self, LMsgCenter);  ---将self的元表设定为Class

    return self;    
end



function LMsgCenter.GetInstance()



     return  this ;
	end



 

function LMsgCenter.SendToMsg(msg)
	-- body

	 this.AnasysMsg(msg);
end


function LMsgCenter.RecvMsg(arg1 ,arg2,arg3,arg4)

     if  arg1 == true then

          local    tmpMsg =    LMsgBase:New(arg2);

          tmpMsg.data = arg4 ;

          tmpMsg.state = arg3 ;

          --print ("arg2=="..arg2)

           this.AnasysMsg(tmpMsg);

         
     else 
       
           this.AnasysMsg(arg2);
      
     end
    
end

   function  LMsgCenter.Awake()

      if LuaAndCMsgCenter.instance ~= nil  then 



       LuaAndCMsgCenter.instance:SettingLuaCallBack(this.RecvMsg);

       end 

   end 

function LMsgCenter.AnasysMsg(msg)
	-- print ("LMsgCenter.AnasysMsg(msg)")
	if msg == nil then
		
		return
	end

	 local  managerId =  msg:GetManager();
    --print ("managerIdmanagerIdmanagerId = "..managerId)
      
     --print ("managerId   msgcenter  ===="..managerId)

       --print ("managerId   msgcenter   222===="..LManagerID.LCharatorManager)
	  if   managerId == LManagerID.LAssetManager then

       -- print ("LAssetManager  managerId   msgcenter   222===="..LManagerID.LAssetManager)
         LAssetManager.GetInstance():ProcessEvent(msg);


	  elseif  managerId == LManagerID.LAudioManager  then


	  	  LAudioManager.GetInstance():ProcessEvent(msg);


	  elseif  managerId == LManagerID.LCharatorManager  then
     
             LCharatorManager.GetInstance():ProcessEvent(msg);
  
	  	

	  elseif  managerId == LManagerID.LDataManager  then

           LDataManager.GetInstance():ProcessEvent(msg);

	  elseif  managerId == LManagerID.LGameManager  then

           LGameManager.GetInstance():ProcessEvent(msg);

	  elseif  managerId == LManagerID.LNetManger  then

            LNetManager.GetInstance():ProcessEvent(msg);

	  elseif  managerId == LManagerID.LNPCManager  then

           LNPCManager.GetInstance():ProcessEvent(msg);

	  elseif  managerId == LManagerID.LUIManager  then


                -- print("LUIManager:ProcessEvent 22222222222");
             LUIManager.GetInstance():ProcessEvent(msg);


       else 
            
          --print ("managerId   Lmsgcenter  ===="..msg.msgId)

            MsgCenter.instance:SendToMsg(msg);
	  end

	
	
	
end

this.Awake();

return  this  ;



