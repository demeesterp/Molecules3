﻿using Microsoft.EntityFrameworkCore;
using Molecules.Core.Data.DbEntities;

namespace Molecules.Core.Data
{
    public class MoleculesDbContext(DbContextOptions<MoleculesDbContext> options) : DbContext(options)
    {
        public DbSet<CalcOrderDbEntity>         CalcOrders      { get; set; }
        public DbSet<CalcOrderItemDbEntity>     CalcOrderItems  { get; set; }
        public DbSet<MoleculeDbEntity>          Molecule        { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("moleculesapp");

            // CalcOrderDbEntity

            modelBuilder.Entity<CalcOrderDbEntity>()
                    .ToTable("CalcOrder")
                    .HasKey(o => o.Id);

            modelBuilder.Entity<CalcOrderDbEntity>()
                    .Property(o => o.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<CalcOrderDbEntity>()
                    .HasMany(o => o.CalcOrderItems)
                    .WithOne(i => i.CalcOrder)
                    .HasForeignKey(i => i.CalcOrderId)
                    .HasPrincipalKey(o => o.Id);

            modelBuilder.Entity<CalcOrderDbEntity>()
                   .Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            modelBuilder.Entity<CalcOrderDbEntity>()
                   .Property(o => o.CustomerName)
                   .IsRequired();


            // CalcOrderItemDbEntity

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .ToTable("CalcOrderItem")
                    .HasKey(o => o.Id);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.CalcOrderId)
                    .IsRequired();

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.CalcType)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.BasissetCode)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.MoleculeName)
                    .IsRequired()
                    .HasMaxLength(250);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.XYZ)
                    .IsRequired();

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .Property(o => o.Charge)
                    .IsRequired();


            // MoleculeDbEntity
            modelBuilder.Entity<MoleculeDbEntity>()
                    .ToTable("Molecule")
                    .HasKey(m => m.Id);

            modelBuilder.Entity<MoleculeDbEntity>()
                .HasIndex(m => new {
                    m.MoleculeName,
                    m.OrderName,
                    m.BasisSet
                }).IsUnique();

            modelBuilder.Entity<MoleculeDbEntity>()
                    .Property(m => m.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<MoleculeDbEntity>()
                    .Property(m => m.BasisSet)
                    .IsRequired();

            modelBuilder.Entity<MoleculeDbEntity>()
                    .Property(m => m.MoleculeName)
                    .IsRequired()
                    .HasMaxLength(250);

            modelBuilder.Entity<MoleculeDbEntity>()
                    .Property(m => m.Molecule)
                    .IsRequired();

        }
    }
}
