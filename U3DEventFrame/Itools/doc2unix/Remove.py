#coding=utf-8
import  sys
reload(sys)
sys.setdefaultencoding('utf8')


import struct
import  binascii
import  os
currentPath = os.getcwd()+"/"
print (len (sys.argv))

print (sys.argv[1])
print (sys.argv[0])
if len(sys.argv) >1 and sys.argv[1] == 'true' :
      nPos =  currentPath.index("Itools")

      currentPath = currentPath[:nPos]


luaPath= currentPath +"Assets/Lua"


toLuaPath = currentPath +"Assets/ToLua/Lua"


def   ExcuseCommond( arg):
    tmpOut=os.system(arg)
    if  tmpOut!=0:
        print u" ------执行命令失败 ==== %s =error=%s"%(arg,tmpOut)
        return -1 ;
            #print u"------执行命令成功 ==== %s "%(arg)
    else:
       print u" ------执行命令成功 ==== %s "%(arg)
       return 0 ;
def ReadFile(file) :

    fileHand = open (file,"r")
    line= fileHand.readline()


    tmpObj="efbbbf"
    tmpLine=binascii.b2a_hex(line).encode("utf8")

    if tmpObj in  tmpLine :
        otherLines= fileHand.readlines()
        fileHand.close()
        print("readone line="+ line )
        print ("all line==="+otherLines[0])
        writeHand = open (file,"w")
        writeHand.writelines(otherLines)
        writeHand.close()

    else:
       fileHand.close()
    #print ("read file=="+file)
    
#    tmpLine=binascii.b2a_hex(line).encode("utf8")
#
#    #print ('read=='+tmpLine)
#    if tmpObj in  tmpLine :
#       print('is equal== %s'%(file))
#
#
#       comond="cat %s | sed '1d'> ./Test.xxx  "%(file)
#       if ExcuseCommond(comond) == 0 :
#          comond="mv -f ./Test.xxx  %s "%(file)
#          ExcuseCommond(comond)

    

def   ListFile(src):
    
    print ("process=="+src)
    for item in os.listdir(src):
        s = os.path.join(src, item)

        if os.path.isdir(s):
            
            ListFile(s)
        elif ( s.endswith(".lua") or s.endswith(".Lua")) :
            
            ReadFile(s)
pass



def  Main() :

     print("begin=="+luaPath)
     ListFile(luaPath)
     print("begin=="+toLuaPath)
     ListFile(toLuaPath)



Main()

     
