using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
namespace TestApp.Models
{
    public class TestContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<err> Errors { get; set; }
        public DbSet<ErrorHistory> ErHstr { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Criticality> Crites { get; set; }
        public DbSet<Urgency> Urg { get; set; }
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

    }

     
}
