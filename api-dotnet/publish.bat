call now ls
dotnet publish --configuration Release --output ./publish
cd ./publish
call now deploy --public
call now alias
cd ..
call now ls > publish.log
pause