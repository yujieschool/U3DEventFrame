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
        
        d = os.path.join(dst, item)

        if os.path.isdir(s):
            print "copying =="+s
            print d
            if  not os.path.exists(d):
                os.makedirs(d)
            copytree(s,d) 
            #shutil.copytree(s, d, symlinks, ignore)
        else:
            if  not os.path.exists(dst):
                os.makedirs(dst)
            shutil.copy2(s, d)


def  backUp(target,source):
      tmpPath =os.getcwd();
      os.chdir(source)
      print source
      zip_commond = "zip -qr %s ./" %(target)
      ExcuseCommond(zip_commond)
      os.chdir(tmpPath);


#递归删除一个文件夹
def  RemovePath (rootdir):
    filelist=os.listdir(rootdir)
    for f in filelist:
       filepath = os.path.join( rootdir, f )
       if os.path.isfile(filepath) and  f.endswith(".meta"):
          os.remove(filepath)
          print filepath+" removed!"
       elif os.path.isdir(filepath):
          RemovePath(filepath)



def   ListFile(src,frontPath,toPath):

    for item in os.listdir(src):
        s = os.path.join(src, item)
        
        if os.path.isdir(s):
            
            ListFile(s,frontPath,toPath)
        elif ( s.endswith(".txt") ) :
                print ("new path 11 =="+s)
                print ("topath =="+toPath)
                tmpCount = len(frontPath)
                toNewPath= os.path.join(toPath,src[tmpCount:])  
                if not os.path.exists(toNewPath):
                    os.makedirs(toNewPath)

                print ("new path 22=="+toNewPath)
                shutil.copy2(s, toNewPath)




def  CopyUseAsset(rootdir,toPath):
     filelist=os.listdir(rootdir)
     for file in filelist:
          filepath = os.path.join( rootdir, file )
          if not file.endswith("AssetBundles") :
               if os.path.isfile(filepath) :
                  shutil.copy2(filepath, toPath)
               else:
                  
                  #shutil.copytree(filepath, toPath, False, None)
                  copytree(filepath,os.path.join(toPath,file))
          else:
              
               ListFile(filepath,rootdir,toPath)




def   Main():
     
 
      appDataPath= frontPath+"Assets\\StreamingAssets\\"
      target = "StreamingAssets"

      RemovePath(appDataPath)

      disPath = "c:\\tmp\\tmp"
      if not os.path.exists(disPath) :
         os.makedirs(disPath)
      RemovePath(disPath)

      CopyUseAsset(appDataPath,disPath)
      backUp(target,disPath)

      source = disPath + "\\"+target+".zip"
      dist = appDataPath
      shutil.copy2(source, dist)

   

               





currentPath = os.getcwd()+"\\"


print (len (sys.argv))

print (sys.argv[1])
print (sys.argv[0])

if len(sys.argv) >1 and sys.argv[1] == 'true' :
      nPos =  currentPath.index("Itools")

      currentPath = currentPath[:nPos]




# 直接回到 assets 上一层目录

frontPath= currentPath





Main()


