for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password@12345 -d master -i CreateDB.sql
    if [ $? -eq 0 ]
    then
        echo "CreateDB.sql completed"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done