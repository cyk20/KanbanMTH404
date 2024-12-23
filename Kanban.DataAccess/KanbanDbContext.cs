using Kanban.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;

using System.Reflection.Emit;

namespace KanbanBoard.DataAccess
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Board Configuration
            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
            });

            // Column Configuration
            modelBuilder.Entity<Column>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(c => c.Board)
                      .WithMany(b => b.Columns)
                      .HasForeignKey(c => c.BoardId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Card Configuration
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.Property(c => c.CreatedAt).IsRequired();
                entity.HasOne(c => c.Column)
                      .WithMany(c => c.Cards)
                      .HasForeignKey(c => c.ColumnId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
