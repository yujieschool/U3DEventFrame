#! /bin/bash



cat $1 | tr -d "\r" > ./tmpXXX.txt



cat ./tmpXXX.txt | sed -e '/^*$/d'

cat ./tmpXXX.txt | sed -n '/./p'


#mv  ./tmpXXX.xx  $1
