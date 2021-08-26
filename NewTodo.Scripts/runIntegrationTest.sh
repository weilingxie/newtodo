./NewTodo.Scripts/startup.sh &

cd ./NewTodo.IntegrationTest;
dotnet test;

pkill dotnet;
