using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class AirportDatabaseContext : DbContext
    {
        public AirportDatabaseContext()
        {
        }

        public AirportDatabaseContext(DbContextOptions<AirportDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airplane> Airplane { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<ExpertiseIn> ExpertiseIn { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Technician> Technician { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestInfo> TestInfo { get; set; }
        public virtual DbSet<TrafficController> TrafficController { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AirportDatabase;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>(entity =>
            {
                entity.HasKey(e => e.RegistrationNo)
                    .HasName("PK__Airplane__22A2336DA78D12A8");

                entity.Property(e => e.RegistrationNo)
                    .HasColumnName("registration_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.ModelNo).HasColumnName("model_no");

                entity.HasOne(d => d.ModelNoNavigation)
                    .WithMany(p => p.Airplane)
                    .HasForeignKey(d => d.ModelNo)
                    .HasConstraintName("FK__Airplane__model___2D27B809");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("PK__Employee__DDDF0AE753CB1BA3");

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo).HasColumnName("phone_no");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UnionMemNo).HasColumnName("union_mem_no");

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            modelBuilder.Entity<ExpertiseIn>(entity =>
            {
                entity.HasKey(e => new { e.Ssn, e.ModelNo })
                    .HasName("PK__Expertis__C01C9ADBBAA20A8E");

                entity.ToTable("Expertise_In");

                entity.Property(e => e.Ssn).HasColumnName("ssn");

                entity.Property(e => e.ModelNo).HasColumnName("model_no");

                entity.HasOne(d => d.ModelNoNavigation)
                    .WithMany(p => p.ExpertiseIn)
                    .HasForeignKey(d => d.ModelNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expertise__model__440B1D61");

                entity.HasOne(d => d.SsnNavigation)
                    .WithMany(p => p.ExpertiseIn)
                    .HasForeignKey(d => d.Ssn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expertise_I__ssn__4316F928");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.ModelNo)
                    .HasName("PK__Model__DC3903C043D89DD8");

                entity.Property(e => e.ModelNo)
                    .HasColumnName("model_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Technician>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("PK__Technici__DDDF0AE71B3FC6C1");

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.SsnNavigation)
                    .WithOne(p => p.Technician)
                    .HasForeignKey<Technician>(d => d.Ssn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Technician__ssn__286302EC");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => new { e.TestNo, e.RegistrationNo, e.Date })
                    .HasName("PK__Test__CC0B8FC621A16600");

                entity.Property(e => e.TestNo).HasColumnName("test_no");

                entity.Property(e => e.RegistrationNo).HasColumnName("registration_no");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.NoHours).HasColumnName("no_hours");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Ssn).HasColumnName("ssn");

                entity.HasOne(d => d.RegistrationNoNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.RegistrationNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test__registrati__4CA06362");

                entity.HasOne(d => d.SsnNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.Ssn)
                    .HasConstraintName("FK__Test__ssn__37A5467C");

                entity.HasOne(d => d.TestNoNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.TestNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test__test_no__4D94879B");
            });

            modelBuilder.Entity<TestInfo>(entity =>
            {
                entity.HasKey(e => e.TestNo)
                    .HasName("PK__Test_Inf__F3F872D11F94AEF5");

                entity.ToTable("Test_Info");

                entity.Property(e => e.TestNo)
                    .HasColumnName("test_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.MaxScore).HasColumnName("max_score");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrafficController>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("PK__Traffic___DDDF0AE7F1793830");

                entity.ToTable("Traffic_Controller");

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .ValueGeneratedNever();

                entity.Property(e => e.MostRecentExamDate)
                    .HasColumnName("most_recent_exam_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.SsnNavigation)
                    .WithOne(p => p.TrafficController)
                    .HasForeignKey<TrafficController>(d => d.Ssn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Traffic_Con__ssn__25869641");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
