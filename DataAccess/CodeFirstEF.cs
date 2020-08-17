namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CodeFirstEF : DbContext
    {
        public CodeFirstEF()
            : base("name=CodeFirstEFConn")
        {
        }

        public virtual DbSet<Actors> Actors { get; set; }
        public virtual DbSet<MovieActors> MovieActors { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movies>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Movies>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Movies>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Designation)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TodoItem>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
