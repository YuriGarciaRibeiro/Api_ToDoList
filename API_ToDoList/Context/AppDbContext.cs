using DefaultNamespace;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base (options)
        {

        }

        public DbSet<ToDoItem>? ToDoItens { get; set; }
        public DbSet<User>? Users { get; set; }

    }
}
