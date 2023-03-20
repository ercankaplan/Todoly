using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTodo.Data.Models.Ef.EfContext
{
    public class TODOLYDbContext: DbContext
    {
        public readonly string Schema = "public";
        public TODOLYDbContext(DbContextOptions<TODOLYDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<TodoItem> TodoItem { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Person>(entity =>
            {

                entity.ToTable(nameof(Person), Schema);
                entity.Property(e => e.Id).ValueGeneratedNever().HasColumnType("uuid").IsRequired();
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Surname).HasMaxLength(100);
                entity.Property(p => p.Email).HasMaxLength(100);


            });

            modelBuilder.Entity<TodoItem>(entity =>
            {

                entity.ToTable(nameof(TodoItem), Schema);
                entity.Property(e => e.Id).ValueGeneratedNever().HasColumnType("uuid").IsRequired();
                entity.Property(p => p.Title).HasMaxLength(150);
                entity.Property(p => p.DueTo).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.ProgressState).IsRequired();


                entity.HasOne(d => d.Person).WithMany(p => p.Todos)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Todo_Person")
                .OnDelete(DeleteBehavior.Cascade);



            });

           




            base.OnModelCreating(modelBuilder);
        }
    }
}
