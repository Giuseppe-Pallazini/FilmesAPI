using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace apifilmes.Models;

public partial class ApiDbContext : DbContext
{
    public ApiDbContext()
    {
    }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAtor> TbAtors { get; set; }

    public virtual DbSet<TbDiretor> TbDiretors { get; set; }

    public virtual DbSet<TbFilme> TbFilmes { get; set; }

    public virtual DbSet<TbFilmeAtor> TbFilmeAtors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user id=root;password=1234;database=apiDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TbAtor>(entity =>
        {
            entity.HasKey(e => e.IdAtor).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbDiretor>(entity =>
        {
            entity.HasKey(e => e.IdDiretor).HasName("PRIMARY");

            entity.HasOne(d => d.IdFilmeNavigation).WithMany(p => p.TbDiretors).HasConstraintName("tb_diretor_ibfk_1");
        });

        modelBuilder.Entity<TbFilme>(entity =>
        {
            entity.HasKey(e => e.IdFilme).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbFilmeAtor>(entity =>
        {
            entity.HasKey(e => e.IdFilmeAtor).HasName("PRIMARY");

            entity.HasOne(d => d.IdAtorNavigation).WithMany(p => p.TbFilmeAtors).HasConstraintName("tb_filme_ator_ibfk_2");

            entity.HasOne(d => d.IdFilmeNavigation).WithMany(p => p.TbFilmeAtors).HasConstraintName("tb_filme_ator_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
