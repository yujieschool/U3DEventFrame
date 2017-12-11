LInvalidMsgManager = {}
local this = LInvalidMsgManager
this.InvalidMsgList = {} --作废的网络回调ID

this.lastEventMsgId = nil

--创建一个 无效ID 的信息
function LInvalidMsgManager.CreateInvalidMsgData(msgId,count)--当count == 0 时 说明是无效
	local invalidMsgData = {}
	invalidMsgData.msgId = msgId
	invalidMsgData.count = count
	return invalidMsgData
end
--添加一个信息
function LInvalidMsgManager.AddInvalidMsgData(msgId,count) --当count == 0 时 说明是无效
	for k,v in pairs(this.InvalidMsgList) do
		if v.msgId == msgId then
			v.count = count
			return
		end
	end
	this.InvalidMsgList[#this.InvalidMsgList + 1] = this.CreateInvalidMsgData(msgId,count)
end
--自动监测信息
function LInvalidMsgManager.CheckIsInvalidMsgId(msgId)  
	local index = 1
	for k,v in pairs(this.InvalidMsgList) do
		if v.msgId == msgId then
			v.count = v.count - 1
			error("LInvalidMsgManager.CheckIsInvalidMsgId v.count = "..v.count)
			if v.count == 0 then --说明要废弃这个msgId
				table.remove(this.InvalidMsgList,index)
				return true
			end
			return false
		end
		index = index + 1
	end
	return false
end
--使最后一次发出去的回调消息无效
function LInvalidMsgManager.LastEventMsgDisable()
	if this.lastEventMsgId == nil then
		return
	end
	this.AddInvalidMsgData(this.lastEventMsgId,1)
end
