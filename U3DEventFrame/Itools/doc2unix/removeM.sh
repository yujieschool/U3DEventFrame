#! /bin/bash



ListDir()
{


rootPath=`pwd`
echo  $rootPath
for  file in `ls $1`
do
   tmpFile=$1"/"$file

   if  [ -d  $tmpFile ]
   then
       echo  "path =="$tmpFile
       ListDir   $tmpFile
   else

       tmpName=${file##*.}
       if [[ $tmpName = "lua"  ||   $tmpName = "Lua" ]]
       then

            `cat $tmpFile | tr -d "\r" > ./tmpXXX.lua`
             `mv  ./tmpXXX.lua  $tmpFile`


             `cat $tmpFile | sed -e '/^*$/d'`

              `cat $tmpFile | sed -n '/./p'`


          fi

#echo  " file="$file

   fi
done

  return

}





tmpPath="$( cd "$(dirname "$0")" &&pwd)"
echo $tmpPath



cd $tmpPath



luaPath='../../Assets/Lua'




echo "begin $luaPath"

cd $luaPath

ListDir   $luaPath


cd $tmpPath


toLuaPath="../../Assets/ToLua"

cd $toLuaPath
echo "begin $toLuaPath"

ListDir   $toLuaPath
