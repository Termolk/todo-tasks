using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO_TASKS.Models;

namespace TODO_TASKS.Services
{
    public class DB : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public DB()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Todoo.db");
        }
    }
}
