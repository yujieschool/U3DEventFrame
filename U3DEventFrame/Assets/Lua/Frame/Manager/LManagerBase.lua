




--ÉùÃ÷£¬ÕâÀïÉùÃ÷ÁËÀàÃû»¹ÓÐÊôÐÔ£¬²¢ÇÒ¸ø³öÁËÊôÐÔµÄ³õÊ¼Öµ¡£
LManagerBase = {}

local this = LManagerBase;

--Õâ¾äÊÇÖØ¶¨ÒåÔª±íµÄË÷Òý£¬¾ÍÊÇËµÓÐÁËÕâ¾ä£¬Õâ¸ö²ÅÊÇÒ»¸öÀà¡£
LManagerBase.__index = LManagerBase

--¹¹ÔìÌå£¬¹¹ÔìÌåµÄÃû×ÖÊÇËæ±ãÆðµÄ£¬Ï°¹ßÐÔ¸ÄÎªNew()
function LManagerBase:New() 
    local self = {};    --³õÊ¼»¯self£¬Èç¹ûÃ»ÓÐÕâ¾ä£¬ÄÇÃ´ÀàËù½¨Á¢µÄ¶ÔÏó¸Ä±ä£¬ÆäËû¶ÔÏó¶¼»á¸Ä±ä
    setmetatable(self, LManagerBase);  --½«selfµÄÔª±íÉè¶¨ÎªClass
    self.eventTree= {}
    return self;    --·µ»Ø×ÔÉí
end




function   LManagerBase:ProcessSendBackMsg(backMsgId)

    if backMsgId ~= nil then 


        if not  self:FindKey(self.backMsgTree,backMsgId) then 

                self.backMsgTree[backMsgId] =  LMsgBackNode:New();
        else 

              self.backMsgTree[backMsgId]:Reset();
        end 

    end 
    LInvalidMsgManager.lastEventMsgId = backMsgId
end 

function   LManagerBase:FrameUpdate()


      for  k ,v  in pairs(self.backMsgTree) do
	  
            
	 	    if(self.backMsgTree[k]:ReduceTime(Time.deltaTime)) then

                  LInvalidMsgManager.AddInvalidMsgData(k,2) --添加已经无效的ID
                  self.backMsg:ChangeEventId(k);
                  self.backMsg.state = -1;
                  self:SendMsg(self.backMsg);
                  self.backMsgTree[k] = nil ;
            end 
	 end
 

end 


function   LManagerBase:InitialTimer(func)


        self.backMsgTree = {} ;

        self.backMsg = LMsgBase:New(0);
       -- this.backMsg.stateId = 127 ;
        self.timer =  FrameTimer.New(func,1,-1);

        self.timer:Start();


end 







function  LManagerBase:FindKey(dict,key)

	for  k ,v  in pairs(dict) do
	
		 if key == k then
			 
			return  true
	      end
		
	end
	
	return false
end


function  LManagerBase:Destroy( )
	-- body

   local keys  ={}

  local  keyCount =0 
	 for  k ,v  in pairs(self.eventTree) do
	      keys[keyCount] = k ;
		  
	      keyCount = keyCount + 1;
	 	 
	 end


	 for i=1,keyCount do 
	 	
	 	self.eventTree[ keys[keyCount]] = nil;
	 end


end


function LManagerBase:RemoveEvent( key)
	-- body
		if this:FindKey(self.eventTree,key) then

             self.eventTree[key]  = nil;
            
		end
     

end

function LManagerBase:GetEventTree()


      return  self.eventTree ;
end 


----local tempNetMsg = nil
-- function  LManagerBase.TryNetId()
--     ----[[
--   --0921 by kerwin 
--    local netId = tempNetMsg.netId
--    if  netId~=nil then
--        printRed(netId.."  =============================================NetId")
--        --[[
--         local tmpMsg = FishLNet_pb['request'..netId]();
--        local protobuf = msg.data
--         tmpMsg:ParseFromString(protobuf);
--         error("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
--        error(tmpMsg.handlerId)
--        --]]
--    end
--    ----]]
-- end



function  LManagerBase:Process(msg)

		if LInvalidMsgManager.CheckIsInvalidMsgId(msg.msgId) then
			error("invalid ===================================================== msg.msgId ====="..msg.msgId)
			return --废弃这个ID 不执行回调
		end

      	if this:FindKey(self.eventTree,msg.msgId) then
		
		local   TmpNod = self.eventTree[msg.msgId]  ;
		
		while TmpNod ~= nil do
			
		 -- print ("TmpNod === "..msg.msgId);



            TmpNod.value:ProcessEvent(msg)	;

            if TmpNod.next  ~= nil then
			  TmpNod = TmpNod.next	;
			 else
                TmpNod = nil ;
			 end



			
		end
	else

		error ("msg.msgId  is not contain "..msg.msgId)
		
	end

end 
--- ÐèÒª´¦Àí»Øµ÷ 
function LManagerBase:ProcessEvent2( msg)


     if(self:FindKey(self.backMsgTree,msg.msgId))then 

              self.backMsgTree[msg.msgId] = nil ;
     end 
      
      self:Process(msg);


end 
function LManagerBase:ProcessEvent( msg)
--    tempNetMsg = msg
--    pcall(LManagerBase.TryNetId) 
 


    self:Process(msg) ;

end

function LManagerBase:RegistMsg(id, evnetNode)





	 if not this:FindKey(self.eventTree,id) then 
	
	    
		self.eventTree[id] = evnetNode  ;
		
	 else
		
	  local TmpNode =  self.eventTree[id]  ;
		
		while TmpNode.next ~= nil do
			
			TmpNode= TmpNode.next  ;
			
		end
		
		TmpNode.next = evnetNode  ;
		
		
     end 
		
		


end

--²âÊÔ´òÓ¡·½·¨--
function LManagerBase:RegistMsgs(lscript,msgs) 




   for i,v in pairs(msgs)  do
   

       local  evntNode = LEventNode:New(lscript)   ;


          -- print ("RegistMsgs === "..v);
         self:RegistMsg( v,evntNode)  ;
           

	end 


end




function  LManagerBase:UnRegistMsg(script,id)
	
	 if this:FindKey(self.eventTree,id) then
	
	     local TmpNod = self.eventTree[id]  ;
		
			 if TmpNod.value == script  then    
			 
					 if TmpNod.next ~= nil then     -- Í·²¿
					
                        self.eventTree[id] = TmpNod.next ;   --Ö±½ÓÖ¸ÏòÏÂÒ»¸ö
					  
						TmpNod.next = nil ;   --ÕÒµ½µÄÕâ¸ö nil
					      
					 else
					     TmpNod = nil  ;  
					 end 
					
			  else
			     
				 while TmpNod.next ~= nil  and TmpNod.next.value ~= script do
					
					 TmpNod = TmpNod.next  ;
				 end
				
				 if  TmpNod.next.next ~= nil then
				
                     local curNode = TmpNod.next ;
                      TmpNod.next = curNode.next  ;

				      curNode.next = nil ;   -- ÕÒµ½µÄÉ¾³ý

                      curNode = nil ;
                  else
				     TmpNod.next = nil			;		
					
				  end
			      
			   
			  end 
	    
	
	end
    
	
end





function  LManagerBase:UnRegistMsgs(script,tmpMsg)

	if arg == nil then

	return;

	end
	

    for i  in  tmpMsg do
	
		self:UnRegistMsg(script,i)
		
	 end
	
end

