#coding=utf-8

import os 



currentPath = os.getcwd()+"\\"

addBinPath= currentPath +"bin\\"

addPythonPython= "C:\\Python27\\"

print ("old")
print (os.environ["PATH"] )

print ("old \n")
os.environ["PATH"] =addBinPath+";"+ os.environ["PATH"] 

os.environ["PATH"]= addPythonPython+";"+ os.environ["PATH"] 
print (os.environ["PATH"] )