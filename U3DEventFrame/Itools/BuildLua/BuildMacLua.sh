#! /bin/bash
ls

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath



echo $fullName


cd $tmpPath

cd  "../../LuaEncoder/luavm"

chmod +x  luac

cd $tmpPath

cd "../../"
fullName="./Itools/BuildLua/GenLuaIMacOX.py"

python  $fullName false
