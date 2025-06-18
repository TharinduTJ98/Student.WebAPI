using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.Entities.Models;

namespace WebAPI.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses{ get; set; }
    }
}
