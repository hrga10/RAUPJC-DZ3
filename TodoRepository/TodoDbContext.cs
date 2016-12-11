using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<TodoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(s => s.Id);
            modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.UserId).IsRequired();
        }

    }
}
