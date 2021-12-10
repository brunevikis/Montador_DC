#!/bin/bash

path="$1"

if [ -d "$path" ]
then

echo "executando"
date

cd "$path"

touch lock

if [[ $( ls *.inp | wc -l ) > 0 ]]
then

	echo "ALGHAO234PGJAGAENCAD" > ENCAD.DAT
	wineconsole /home/marco/PrevisaoPLD/shared/previvaz/previvaz.exe
	
    chmod 775 *
	rm lock

	echo "executado"

fi

fi



echo "fim"
date
