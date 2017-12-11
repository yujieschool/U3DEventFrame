#! /bin/bash
ls

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath


cd "../../"
fullName="./Itools/BuildLua/WithOutBuild.py"
python  $fullName false
