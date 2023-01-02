#!/bin/bash

if [ -z "$1" ]
  then
    echo "No argument supplied. Migration name was expected"
else
    dotnet ef migrations add "$1" --project ./src/SoliSYSTEMS.ServiceTemplate.Migrations/ 
fi
