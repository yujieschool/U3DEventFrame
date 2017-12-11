#! /bin/bash

tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath

cd $tmpPath

cd "../../"
fullName="./Itools/doc2unix/Remove.py"
python  $fullName false


#mv  ./tmpXXX.xx  $1
