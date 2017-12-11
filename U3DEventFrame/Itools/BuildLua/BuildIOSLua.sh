#! /bin/bash
ls

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath



echo $fullName


cd $tmpPath

cd  "../../LuaEncoder/luajit_ios/x86"

chmod +x  luajit

cd $tmpPath

cd "../../"
fullName="./Itools/BuildLua/GenLuaIOSX86.py"
python  $fullName false
