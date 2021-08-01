using System.Diagnostics.CodeAnalysis;
using FluentMigrator;

namespace NewTodo.DbMigration
{
    [ExcludeFromCodeCoverage]
    [Migration(202108012141)]
    public class _202108012141_AddTodoItemTable : Migration
    {
        private const string TableName = "TodoItems";

        public override void Up()
        {
            Create.Table(TableName)
                .WithIdColumn()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithStringNotNullableColumn("Title", 100)
                .WithStringNotNullableColumn("State", 5)
                .WithDateTimeNotNullableColumn("CreateTime")
                .WithDateTimeNotNullableColumn("LastUpdatedTime");
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}