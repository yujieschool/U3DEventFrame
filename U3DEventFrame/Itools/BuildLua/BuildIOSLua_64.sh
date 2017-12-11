#! /bin/bash
ls

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath

fullName=$tmpPath"/GenLuaIOSX64.py"

echo $fullName


cd $tmpPath

cd  "../../LuaEncoder/luajit_ios/x86_64"

chmod +x  luajit

cd $tmpPath

cd "../../"
fullName="./Itools/BuildLua/GenLuaIOSX64.py"
python  $fullName false
