
--输出日志--
function log(str)
   Util.Log(str);
end

--打印字符串--
function print(str) 
	Util.Log(str);
end
--蓝色打印
function printBlue(str) 
	Util.Log("<color=blue>"..str.."</color>");
end
--绿色打印
function printGreen(str) 
	Util.Log("<color=green>"..str.."</color>");
end
--红色打印
function printRed(str) 
	Util.Log("<color=red>"..str.."</color>");
end
--错误日志--
function error(str) 
	Util.LogError(str);
end

--警告日志--
function warn(str) 
	Util.LogWarning(str);
end

--查找对象--
function find(str)
	return GameObject.Find(str);
end

function destroy(obj)
	GameObject.Destroy(obj);
end
function destroyWait(obj,tim)
	GameObject.Destroy(obj,tim);
end
function newobject(prefab)
	return GameObject.Instantiate(prefab);
end



function child(str)
	return transform:FindChild(str);
end

function subGet(childNode, typeName)		
	return child(childNode):GetComponent(typeName);
end


   --[[
     --      beginValue  表的初始值
--      tmpTable     要赋值表
           这个方法出来的 值  按照写的数组顺序累加   
     TestSix = { "test1","test5","test3","test4","test6"}   ;

     ResetTableKeyValue(1000,TestSix);

   --]]

  function  ResetTableKeyValue ( beginValue, tmpTable)


     for i=1 , table.getn(tmpTable) do

       tmpTable[tmpTable[i]] =  beginValue+ i ;
       
    end 

  end 

--      beginValue  表的初始值
--      tmpTable     要赋值表

--[[
            这个方法出来的 值  不是按照写的顺序累加   
     TestSix = { test1 =1, test2=1,test3=1}   ;

     ResetTableValue(1000,TestSix);

--]]
function   ResetTableValue( beginValue, tmpTable)

      local  tmpCount = 0 ;

      --[[
      for i =1 , table.getn(tmpTable) do

       table[i]=        beginValue +   tmpCount ;
       error(' table[i] =='.. table[i]);
       tmpCount = tmpCount+1 ;

       end 
           --]]

     for  key in pairs(tmpTable)   do 
        
           tmpTable[key] = beginValue +   tmpCount ;

           tmpCount = tmpCount+1 ;

     end 
      

end 



    -- 计算包含汉字字符串长度
function string.lenth(inputstr)

   local lenInByte = #inputstr
   local width = 0
   local i = 1
    while (i<=lenInByte) 
    do
        local curByte = string.byte(inputstr, i)
        local byteCount = 1;
        if curByte>0 and curByte<=127 then
            byteCount = 1                                              
        elseif curByte>=192 and curByte<223 then
            byteCount = 2                                              
        elseif curByte>=224 and curByte<239 then
            byteCount = 3                                               
        elseif curByte>=240 and curByte<=247 then
            byteCount = 4                                             
        end
         
        i = i + byteCount                                            
        width = width + 1                                            
    end
    return width
end

-- 截取包含汉字字符串
function   string.subStr(inputstr,startnub,endnub)

   local lenInByte = #inputstr
   local width = 0
   local i = 1
   local  tmpStart  = 0 
   local  tmpEnd  =0
    while (i<=lenInByte) 
    do
        local curByte = string.byte(inputstr, i)
        local byteCount = 1;
        if curByte>0 and curByte<=127 then
            byteCount = 1                                             
        elseif curByte>=192 and curByte<223 then
            byteCount = 2                                              
        elseif curByte>=224 and curByte<239 then
            byteCount = 3                                               
        elseif curByte>=240 and curByte<=247 then
            byteCount = 4                                               
        end
         
        local char = string.sub(inputstr, i, i+byteCount-1)
                                                                       
        i = i + byteCount                                            


        width = width + 1                                            

        if(width == startnub)  then

            tmpStart = i 
        end 

         if(width == endnub)  then

            tmpEnd = i -1
        end 

    end

     local  result = string.sub(inputstr, tmpStart, tmpEnd)
     return result

end 



function string.convert(str, con)

   local len = string.len(str);
   local strStart = "";
   local strEnd = "";

   for i = 1, len do
        if string.byte(str, i) == 91 then
            strStart = string.sub(str, 1, i - 1);
        elseif string.byte(str, i) == 93 then
            strEnd = string.sub(str, i + 1, len);
        end
   end
   
   return strStart .. con .. strEnd;

end

