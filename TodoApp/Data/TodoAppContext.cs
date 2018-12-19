using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext (DbContextOptions<TodoAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoApp.Models.TodoNote> TodoNotes { get; set; }
    }
}
