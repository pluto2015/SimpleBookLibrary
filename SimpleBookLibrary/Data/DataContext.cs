using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBookLibrary.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    public class DataContext:DbContext
    {
        #region entities
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<BorrowerEntity> Borrowers { get; set; }
        public DbSet<BorrowHistoryEntity> BorrowHistory { get; set; }
        public DbSet<DepartmentEntity> Department { get; set; }
        #endregion
        public DataContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>(buildAction =>
            {
                buildAction.ToTable("TB_Books");
                buildAction.Property(x=>x.Code).IsRequired(false);
                buildAction.Property(x => x.Publisher).IsRequired(false);
                buildAction.Property(x => x.Author).IsRequired(false);
                buildAction.Property(x => x.DepartmentId).IsRequired(false);
                buildAction.Property(x => x.Remark).IsRequired(false);
            });
            modelBuilder.Entity<BorrowerEntity>(buildAction =>
            {
                buildAction.ToTable("TB_Borrowers");
            });
            modelBuilder.Entity<BorrowHistoryEntity>(buildAction =>
            {
                buildAction.ToTable("TB_BorrowHistories");
                buildAction.Property(x => x.BookId).IsRequired(false);
            });
            modelBuilder.Entity<DepartmentEntity>(buildAction =>
            {
                buildAction.ToTable("TB_Departments");
            });
        }
    }
}
