using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeWork.Model.Models
{
    public partial class FAQDBContext : DbContext
    {
        public FAQDBContext()
        {
        }

        public FAQDBContext(DbContextOptions<FAQDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QaData> QaData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-3J4IUMQ9\\ZIVTESTSQL;Database=FAQDB;user id=sa;password=1qaz!QAZ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QaData>(entity =>
            {
                entity.ToTable("QA_data");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Conten)
                    .HasColumnName("CONTEN")
                    .HasMaxLength(50);

                entity.Property(e => e.CrtBy)
                    .IsRequired()
                    .HasColumnName("CRT_BY")
                    .HasMaxLength(50);

                entity.Property(e => e.CrtTime)
                    .HasColumnName("CRT_TIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndOn)
                    .HasColumnName("END_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remove).HasColumnName("REMOVE");

                entity.Property(e => e.Sort).HasColumnName("SORT");

                entity.Property(e => e.StartOn)
                    .HasColumnName("START_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("TITLE")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                    .HasColumnName("UPDATE_BY")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("UPDATE_TIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpperAllid)
                    .HasColumnName("UPPER_ALLID")
                    .HasMaxLength(50);

                entity.Property(e => e.UpperId).HasColumnName("UPPER_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
