using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class NewContractorFindingContext : DbContext
{
    public NewContractorFindingContext()
    {
    }

    public NewContractorFindingContext(DbContextOptions<NewContractorFindingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContractorDetail> ContractorDetails { get; set; }

    public virtual DbSet<ContractorView> ContractorViews { get; set; }

    public virtual DbSet<CustomerView> CustomerViews { get; set; }

    public virtual DbSet<ServiceProviding> ServiceProvidings { get; set; }

    public virtual DbSet<TbBuilding> TbBuildings { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbGender> TbGenders { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Userview> Userviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MOBACK\\MSSQLSERVER01;Database=New_Contractor_Finding;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractorDetail>(entity =>
        {
            entity.HasKey(e => e.License).HasName("PK__Contract__A4E54DE5F908475B");

            entity.ToTable("Contractor_details");

            entity.Property(e => e.License)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("license");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

            entity.HasOne(d => d.Contractor).WithMany(p => p.ContractorDetails)
                .HasForeignKey(d => d.ContractorId)
                .HasConstraintName("FK__Contracto__Contr__2F10007B");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.ContractorDetails)
                .HasForeignKey(d => d.Gender)
                .HasConstraintName("FK__Contracto__Gende__300424B4");

            entity.HasOne(d => d.ServicesNavigation).WithMany(p => p.ContractorDetails)
                .HasForeignKey(d => d.Services)
                .HasConstraintName("FK__Contracto__Servi__30F848ED");
        });

        modelBuilder.Entity<ContractorView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ContractorView");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.GenderType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.License)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("license");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomerView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CustomerView");

            entity.Property(e => e.Building)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LandSqft).HasColumnName("Land_sqft");
            entity.Property(e => e.RegistrationNo)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceProviding>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service___C51BB00A61F0FC39");

            entity.ToTable("Service_providing");

            entity.HasIndex(e => e.ServiceName, "UQ__Service___A42B5F99262304B2").IsUnique();

            entity.Property(e => e.ServiceId).ValueGeneratedNever();
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbBuilding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tb_Build__3214EC27EDB742BF");

            entity.ToTable("Tb_Building");

            entity.HasIndex(e => e.Building, "UQ__Tb_Build__553663717B7DF165").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Building)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.RegistrationNo).HasName("PK__Tb_Custo__6EF5E04355CF4CD4");

            entity.ToTable("Tb_Customer");

            entity.Property(e => e.RegistrationNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BuildingType).HasColumnName("Building_Type");
            entity.Property(e => e.LandSqft).HasColumnName("Land_sqft");

            entity.HasOne(d => d.BuildingTypeNavigation).WithMany(p => p.TbCustomers)
                .HasForeignKey(d => d.BuildingType)
                .HasConstraintName("FK__Tb_Custom__Build__3B75D760");
        });

        modelBuilder.Entity<TbGender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Tb_Gende__4E24E9F770A419C4");

            entity.ToTable("Tb_Gender");

            entity.Property(e => e.GenderId).ValueGeneratedNever();
            entity.Property(e => e.GenderType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tb_User__1788CC4CF6330C22");

            entity.ToTable("Tb_User");

            entity.HasIndex(e => e.EmailId, "UQ__Tb_User__7ED91ACE9F1CC951").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.TypeUserNavigation).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.TypeUser)
                .HasConstraintName("FK__Tb_User__TypeUse__276EDEB3");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__User_Typ__F04DF13AC1D2593D");

            entity.ToTable("User_Type");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("typeId");
            entity.Property(e => e.Usertype1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("usertype");
        });

        modelBuilder.Entity<Userview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Userview");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Usertype)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("usertype");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
