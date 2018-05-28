using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TKPMGD_NET.Data.Mapper;
using TKPMGD_NET.Models;

namespace TKPMGD_NET.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            new SentenceMap(builder.Entity<Sentence>());
            new TestCaseMap(builder.Entity<TestCase>());
        }

        public virtual DbSet<Sentence> Sentences { get; set; }
        public virtual DbSet<TestCase> TestCases { get; set; }
    }
}
