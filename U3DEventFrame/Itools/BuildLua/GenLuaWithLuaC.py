#coding=utf-8

import os
import  sys

import  shutil

reload(sys)
sys.setdefaultencoding('utf8')
currentPath = os.getcwd()+"/"


print (len (sys.argv))

print (sys.argv[1])
print (sys.argv[0])
if len(sys.argv) >1 and sys.argv[1] == 'true' :
    nPos =  currentPath.index("Itools")
        
    currentPath = currentPath[:nPos]



#os.system("pause")

luaPath= currentPath +"Assets/Lua"


toLuaPath = currentPath +"Assets/ToLua/Lua"

print ("luaPath=22="+luaPath)




JITPath= currentPath +"LuaEncoder/luavm"



winStreamAssetPath = currentPath+ "Assets/StreamingAssets/Lua/Mac"





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

def copytree(src, dst, symlinks=False, ignore=None):
    for item in os.listdir(src):
        s = os.path.join(src, item)
        #print s
        d = os.path.join(dst, item)
        #print d
        if os.path.isdir(s):
            
            #print "copy dis =="+d
            if not os.path.exists(d) :
                os.makedirs(d)
            
            copytree(s, d, symlinks, ignore)
        elif os.path.isfile(s) and os.path.exists(s):
            if not os.path.exists(dst) :
                os.makedirs(d)
            shutil.copy2(s, d)


def   ExcuseCommond( arg):
    if os.system(arg) !=0:
        print u" ------执行命令失败 ==== %s "%(arg)
            #print u"------执行命令成功 ==== %s "%(arg)
            else:
                print u" ------执行命令成功 ==== %s "%(arg)

def  RemovePath (rootdir):
    
    if os.path.exists(rootdir):
    filelist=os.listdir(rootdir)
    for file in filelist:
        filepath = os.path.join( rootdir, file )
            if os.path.isfile(filepath) and os.path.exists(filepath):
                os.remove(filepath)
                    print filepath+" removed!"
                        elif os.path.isdir(filepath):
                            if  len(filepath) >2 :
                                shutil.rmtree(filepath,True)
                            #print "dir "+filepath+" removed!"
shutil.rmtree(rootdir,True)

def   JitCompile(filePath,file,frontPath):
    outPath =  tmpOutPath+"/"+file[len(frontPath):]
        nPos = len(filePath) - len(frontPath)
            delatPath = tmpOutPath +"/"+ filePath[len(frontPath):len(frontPath)+nPos]
                #print("delatPath =="+delatPath)
                #tmpPos = delatPath.find("Data")
                #if(tmpPos != -1):
                #print(filePath)
                
                
                if not os.path.exists(delatPath) :
                    
                    os.makedirs(delatPath)
                        
                        
                        curPath = os.getcwd()
                            os.chdir(JITPath)
                                #print ("JITPath=="+JITPath)
                                #print ("curPath=="+curPath)
                                #print ("outPath=="+outPath)
                                #commond ="%sluajit.exe -b -g  %s  %s.bytes"%(JITPath,file,outPath)
                                commond ="./luac -o   %s  %s"%(file,outPath)
                                    ExcuseCommond(commond)
                                        os.chdir(curPath)
#os.system("pause")




def   ListFile(src,frontPath):
    
    for item in os.listdir(src):
        s = os.path.join(src, item)
        #print s
        if os.path.isdir(s):
            
            ListFile(s,frontPath)
        elif ( s.endswith(".lua") or s.endswith(".Lua")) :
            
            JitCompile(src,s,frontPath)
        pass

def   Main():
    
    #RemovePath(androidStreamAssetPath)
    RemovePath(winStreamAssetPath)
        os.chdir(toLuaPath)
            
            #outPath= toLuaPath +"out\\"
            if os.path.exists(tmpOutPath) :
                RemovePath(tmpOutPath)
                    else :
                        os.mkdir(tmpOutPath)
                            
                            ListFile(toLuaPath,toLuaPath)
                                
                                
                                #CopyLua(tmpOutPath,androidStreamAssetPath)
                                #CopyLua(tmpOutPath,winStreamAssetPath)
                                #RemovePath(outPath)
                                os.chdir(luaPath)
                                    
                                    
                                    
                                    ListFile(luaPath,luaPath)
                                        #CopyLua(tmpOutPath,androidStreamAssetPath)
                                        CopyLua(tmpOutPath,winStreamAssetPath)
                                            #RemovePath(outPath)
                                            RemovePath(tmpOutPath)
#tmpOutPath = androidStreamAssetPath +"\\out"
#RemovePath(tmpOutPath)

#tmpOutPath = winStreamAssetPath +"\\out"
#RemovePath(tmpOutPath)


def  CopyLua(src,platform):
    
    
    if not os.path.exists(platform) :
        os.makedirs(platform)
            
            
            copytree(src,platform)



#tmpPath= "c:\\tmp\\tmp\\tmp"
#os.makedirs(tmpPath)
Main()
#os.system("pause")




