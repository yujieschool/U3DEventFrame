#coding=utf-8

import os

import  shutil
import sys
reload(sys)

sys.setdefaultencoding('gbk')


def getFileList( p):

        p = str( p )

        if p=="":

              return [ ]

        p = p.replace( "/","\\")

        if p[ -1] != "\\":

             p = p+"\\"

        a = os.listdir( p )

        b = [ x   for x in a if os.path.isfile( p + x ) ]

        return b

def   ExcuseCommond( arg):
      if os.system(arg) ==0:
          print u"------执行命令成功 ==== %s "%(arg)
      else:
         print u" ------执行命令失败 ==== %s "%(arg)

def copytree(src, dst, symlinks=False, ignore=None):
    for item in os.listdir(src):
        s = os.path.join(src, item)
        print s
        d = os.path.join(dst, item)
        print d
        if os.path.isdir(s):
            print "copying"
            shutil.copytree(s, d, symlinks, ignore)
        else:
            shutil.copy2(s, d)




def   Main():
     
      print ("main coming")
   
      for file in  getFileList(pbPath):

           if file.endswith(".proto") :
               os.chdir(toolsPath)
               print (toolsPath)
               commond = 'protoc.exe --plugin=protoc-gen-lua="..\\plugin\\build.bat" --lua_out=".\\Protol" %s  '%(file)

           
               ExcuseCommond(commond)

               srcPath=  " %s\\Protol\\%s_pb.lua"%(toolsPath,file[:-6])

               print ("srcPath=="+srcPath)
               disPath = "%s\\%s_pb.lua"%(pbPath,file[:-6])

               print ("dist =="+disPath)

      srcPATH ="%s\\Protol"%(toolsPath)
      disPATH= pbPath
      copytree(srcPATH,pbPath)

#print ("curWorkPath==")

#curWorkPath= os.getcwd() +"\\Assets\\Scripts\\Editor\\ToLuaTools\\"
#print ("curWorkPath=22="+curWorkPath)


currentPath = os.getcwd()+"\\"


print (len (sys.argv))

print (sys.argv[1])
print (sys.argv[0])

if len(sys.argv) >1 and sys.argv[1] == 'true' :
      nPos =  currentPath.index("Itools")

      currentPath = currentPath[:nPos]




# 直接回到 assets 上一层目录

frontPath= currentPath


toolsPath = frontPath +"Itools\\protoc-gen-lua-master\\example"

appDataPath= frontPath+"Assets\\"

pbPath = appDataPath+"Lua\\PB"


print ("pbPath=="+pbPath)


                

Main()

print ("all Finish")