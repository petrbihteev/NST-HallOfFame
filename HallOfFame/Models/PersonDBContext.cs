using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HallOfFame
{
    public partial class PersonDBContext : DbContext
    {
        public PersonDBContext()
        {
        }

        public PersonDBContext(DbContextOptions<PersonDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConPersonSkill> ConPersonSkills { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PersonDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConPersonSkill>(entity =>
            {
                entity.HasKey(e => e.IdPersonSkill);

                entity.HasIndex(e => e.PersonId, "IX_ConPersonSkills_PersonId");

                entity.HasIndex(e => e.SkillId, "IX_ConPersonSkills_SkillId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ConPersonSkills)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ConPersonSkills_Persons");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.ConPersonSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ConPersonSkills_Skills");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
