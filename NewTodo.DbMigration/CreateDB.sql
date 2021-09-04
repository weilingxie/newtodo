IF DB_ID('TodoDb') IS NOT NULL
BEGIN
SELECT 'Database Name already Exist' AS Message
END
ELSE
BEGIN
    CREATE DATABASE [TodoDb]
SELECT 'Database is Created'
END