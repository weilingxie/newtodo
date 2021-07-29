using Microsoft.EntityFrameworkCore;

namespace NewTodo.Infrastructure
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}