FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

COPY NewTodo.DbMigration/CreateDB.sql CreateDB.sql
COPY dbInitial.sh dbInitial.sh
COPY entryPoint.sh entryPoint.sh

RUN chmod +x dbInitial.sh entryPoint.sh

CMD /bin/bash ./entryPoint.sh