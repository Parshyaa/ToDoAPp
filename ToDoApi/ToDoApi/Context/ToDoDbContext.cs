using Microsoft.EntityFrameworkCore;
using ToDoApi.Entity;
using ToDoApi.Entity.Interface;

namespace ToDoApi.Context
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<ToDo> ToDoSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasKey(x => x.ToDoId);
        }
    }
}
