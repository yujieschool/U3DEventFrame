#! /bin/bash
ls

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath

fullName=$tmpPath"/WithOutBuild.py"

echo $fullName


cd $tmpPath

cd  "../../LuaEncoder/luavm"

chmod +x  luac

cd $tmpPath


python  $fullName true ios
