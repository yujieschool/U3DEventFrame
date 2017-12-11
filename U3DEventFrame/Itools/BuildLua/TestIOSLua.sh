#! /bin/bash
ls

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath

fullName=$tmpPath"/GenLuaIOSX86.py"

echo $fullName


cd $tmpPath

cd  "../../LuaEncoder/luajit_ios/x86"

chmod +x  luajit

cd $tmpPath


python  $fullName true
