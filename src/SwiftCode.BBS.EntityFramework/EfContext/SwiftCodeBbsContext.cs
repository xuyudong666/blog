using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwiftCode.BBS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.EntityFramework.EfContext;

public class SwiftCodeBbsContext : DbContext
{
    public SwiftCodeBbsContext(DbContextOptions<SwiftCodeBbsContext> options) 
        : base(options)
    {
        
    }

    public SwiftCodeBbsContext()
    {
    }

    public DbSet<Article>  Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().Property(x => x.Title).HasMaxLength(128);
        modelBuilder.Entity<Article>().Property(x=>x.Submitter).HasMaxLength(64);
        modelBuilder.Entity<Article>().Property(x => x.Category).HasMaxLength(256);
        modelBuilder.Entity<Article>().Property(x => x.Remark).HasMaxLength(1024);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.,1433;Database=SwiftCodeBbs;Trusted_Connection=True;Connection Timeout=600;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;").LogTo(Console.WriteLine, LogLevel.Information);
    }
}
