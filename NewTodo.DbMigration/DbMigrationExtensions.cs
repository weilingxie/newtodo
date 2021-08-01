using FluentMigrator.Builders.Create.Table;

namespace NewTodo.DbMigration
{
    public static class DbMigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(
            this ICreateTableWithColumnOrSchemaSyntax createTable)
        {
            return createTable.WithColumn("Id")
                .AsGuid()
                .NotNullable()
                .PrimaryKey();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithStringNotNullableColumn(
            this ICreateTableColumnOptionOrWithColumnSyntax createTable, string name, int size)
        {
            return createTable.WithColumn(name)
                .AsString(size)
                .NotNullable();
        }
        
        public static ICreateTableColumnOptionOrWithColumnSyntax WithDateTimeNotNullableColumn(
            this ICreateTableColumnOptionOrWithColumnSyntax createTable, string name)
        {
            return createTable.WithColumn(name)
                .AsDateTime2()
                .NotNullable();
        }
    }
}