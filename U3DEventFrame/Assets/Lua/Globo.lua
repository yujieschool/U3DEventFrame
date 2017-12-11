--[[]]
object			= System.Object
Type			= System.Type
Object          = UnityEngine.Object
GameObject 		= UnityEngine.GameObject
Transform 		= UnityEngine.Transform
MonoBehaviour 	= UnityEngine.MonoBehaviour
Component		= UnityEngine.Component
Application		= UnityEngine.Application
SystemInfo		= UnityEngine.SystemInfo
Screen			= UnityEngine.Screen
Camera			= UnityEngine.Camera
Material 		= UnityEngine.Material
Renderer 		= UnityEngine.Renderer
AsyncOperation	= UnityEngine.AsyncOperation

CharacterController = UnityEngine.CharacterController
SkinnedMeshRenderer = UnityEngine.SkinnedMeshRenderer
Animation		= UnityEngine.Animation
AnimationClip	= UnityEngine.AnimationClip
AnimationEvent	= UnityEngine.AnimationEvent
AnimationState	= UnityEngine.AnimationState
Input			= UnityEngine.Input
KeyCode			= UnityEngine.KeyCode
AudioClip		= UnityEngine.AudioClip
AudioSource		= UnityEngine.AudioSource
Physics			= UnityEngine.Physics
Light			= UnityEngine.Light
LightType		= UnityEngine.LightType
ParticleEmitter	= UnityEngine.ParticleEmitter
Space			= UnityEngine.Space
CameraClearFlags= UnityEngine.CameraClearFlags
RenderSettings  = UnityEngine.RenderSettings
MeshRenderer	= UnityEngine.MeshRenderer
WrapMode		= UnityEngine.WrapMode
QueueMode		= UnityEngine.QueueMode
PlayMode		= UnityEngine.PlayMode
ParticleAnimator= UnityEngine.ParticleAnimator
TouchPhase 		= UnityEngine.TouchPhase
AnimationBlendMode = UnityEngine.AnimationBlendMode

SystemInfo = UnityEngine.SystemInfo


ParticleSystem  = UnityEngine.ParticleSystem


--UGUI
RectTransform = UnityEngine.RectTransform
Text          = UnityEngine.UI.Text
Button        = UnityEngine.UI.Button


--  Fish

MsgBase		 = U3DEventFrame.MsgBase
AssetManager = U3DEventFrame.AssetManager
UIManager	 = U3DEventFrame.UIManager
MsgCenter	 = U3DEventFrame.MsgCenter


print("==Canvas =========")
Canvas = GameObject.FindGameObjectWithTag("MainCanvas");

print(Canvas.name .."===========")
--[[


 print ( string.splitStr("12345678910111213",3,","))

  µÚÒ»¸ö²ÎÊý ±íÊ¾Òª·Ö¸îµÄ×Ö·û´®
  µÚ¶þ¸ö  ±íÊ¾ Ã¿¼¸Î» ·Ö¸îÒ»ÏÂ

  µÚÈý¸ö²ÎÊý    ±íÊ¾ ÓÃÊ²Ã´·Ö¸î

--]]
 function string.splitStr (tmpStr,number,split)

    tmpCount= string.len(tmpStr) / number  ;

    local   mutiCount = math.floor(tmpCount);

     local  result = "" ;

     print ("mutiCount=="..mutiCount)

   
      for i=0,mutiCount-1,1 do 

          result =split..string.sub(tmpStr,-i*number-3,-i*number-1)..result
      end 

    result = string.sub(tmpStr,-mutiCount*number-3,-mutiCount*number-1)..result ; 
    return  result ; 
       
end

function string.split(s, delim)
  if type(delim) ~= "string" or string.len(delim) <= 0 then
    return
  end
 
  local start = 1
  local t = {}
  while true do
  local pos = string.find (s, delim, start, true) -- plain find
    if not pos then
     break
    end
 
    table.insert (t, string.sub (s, start, pos - 1))
    start = pos + string.len (delim)
  end
  table.insert (t, string.sub (s, start))
 
  return t
end


function string.divide(str)
	
	num = tostring(str);
	
	forward = string.split(num, ".");
	
	target = forward[1];
	count = math.floor(string.len(target) / 3);
	

	local div = {}
	

	for i = 1, count do
		table.insert(div, string.sub(target, -3 * i, -3 * (i - 1) - 1));	
	end
	

	result = string.sub(target, 1, string.len(target) - count * 3);

	for	i = count, 1, -1 do

		result = result .. "," .. div[i];
	
	end
	
	

    return  result .. "." .. forward[1] ; 
	
end

function table.contains(table, element)
  if table==nil then
    printRed("table NIl")
    return false,-1
  end
  local index = -1;
  for i, v in pairs(table) do
    if v == element then
      index = i;
      return true, index;
    end
  end
  return false, index;
end

function string.contains(str, item)
	
	a = nil;
	a = string.find(str, item);
	return a ~= nil
	
end


function Invoke(func, duration, loop, scale)

newTimer = Timer.New();
newTimer:Reset(func, duration, loop, scale);
newTimer:Start();

end


math.randomseed(os.time())
function math.randomp(min, max)

  local ret = 0;

  n = max;
  while	n == max do
	  for i = 1, 3 do

		n = math.random(min, max)

		ret = n

	  end
	end
  return ret

end


function math.randomWeight(weights)
    
    totalWeight = 0;
    probability = {}

	for i = 1, #weights do
		totalWeight = totalWeight + weights[i];
		temp = i;
		table.insert(probability, 0);
		while temp > 0 do
            
			probability[i] = probability[i] + weights[temp];
			temp = temp - 1;
		end
	end

	ran = math.randomp(0, totalWeight);

	index = 1;

    for i = 1, #probability do
        if ran < probability[i] then
            break;
        end
        index = index + 1;
    end
    
		
	return index;

end

function table.removep(tab, obj)
	for i, v in pairs(tab) do
		if	v == obj then
			table.remove(tab, i);
			break;
		end
	end

end

function string.percent(num)
    return num * 100 .. "%";
end

function string.endwith(str, item)
    count = string.len(item);

    return string.sub(str, -count) == item;

end

function string.startwith(str, item)
    count = string.len(item);

    return string.sub(str, count) == item;
end

function string.replace(str, inStr, outStr)
    return (string.gsub(str, inStr, outStr));
end



function table.copy(st)

    local tab = {}
    for k, v in pairs(st or {}) do
        if type(v) ~= "table" then
            tab[k] = v
        else
            tab[k] = table.copy(v)
        end
    end
    return tab;

end


function math.GetProbabilityResult(prop)

    prop = prop * 100;
    local ran = math.randomp(0, 101);

    if ran <= prop then
        return true;
    else
        return false;
    end

end



function ResetTransform(trans)
    trans.localPosition = Vector3.zero;
    trans.localScale = Vector3.one;
    trans.localRotation = Quaternion.identity;
end

--ËÄÉáÎåÈë
function math.round(num, n)
    if n > 0 then
        local scale = math.pow(10, n-1)
        return math.floor(num / scale + 0.5) * scale
    elseif n < 0 then
        local scale = math.pow(10, n)
        return math.floor(num / scale + 0.5) * scale
    elseif n == 0 then
        return num
    end
end

--通过一个 101 返回一个头像的字段 1 man 2 woman
function GetPersonPicStr(picStr)
  --error("picStr = "..picStr)
  local tag =  string.sub(picStr,1,1)
  local strPic = "ManHead"
  --error("tag = "..tag)
  if tonumber(tag) == 2 then
    strPic = "WomanHead"
  end
  return strPic..string.sub(picStr,-2)
end


--将秒转换为 秒/分/时/天
function FormatTime(second, isFormat)
    if second == nil or second == 0 then
        if isFormat then
            return "00", "00", "00", "00";
        else
            return 0, 0, 0, 0;
        end
    end

    local s = second;
    local m = 0;
    local h = 0;
    local d = 0;
    s = math.floor(s);
    m = math.floor(second / 60);
    if m > 0 then
        s = s % 60;
        h = math.floor(m / 60);
        if h > 0 then
            m = m % 60;
            d = math.floor(h / 24);
            if d > 0 then
                h = h % 24;
            end
        end

    end

    if isFormat then
        return string.format("%02d", s), string.format("%02d", m), string.format("%02d", h), string.format("%02d", d);
    else
        return s, m, h, d;
    end
    

end


function table.random(tab)

    local result = {};
    local tempCount = #tab;
    for i = 1, tempCount do
        local ranSeed = math.randomp(1, #tab + 1);
        table.insert(result, tab[ranSeed]);
        table.remove(tab, ranSeed);
    end
    

    return result;

end

