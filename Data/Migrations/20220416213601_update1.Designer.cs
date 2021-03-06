// <auto-generated />
using System;
using Airport_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Airport_Database.Migrations
{
    [DbContext(typeof(AirportDatabaseContext))]
    [Migration("20220416213601_update1")]
    partial class update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Airport_Database.Models.Airplane", b =>
                {
                    b.Property<long>("RegistrationNo")
                        .HasColumnName("registration_no")
                        .HasColumnType("bigint");

                    b.Property<int?>("ModelNo")
                        .HasColumnName("model_no")
                        .HasColumnType("int");

                    b.HasKey("RegistrationNo")
                        .HasName("PK__Airplane__22A2336DA78D12A8");

                    b.HasIndex("ModelNo");

                    b.ToTable("Airplane");
                });

            modelBuilder.Entity("Airport_Database.Models.Employee", b =>
                {
                    b.Property<int>("Ssn")
                        .HasColumnName("ssn")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int?>("PhoneNo")
                        .HasColumnName("phone_no")
                        .HasColumnType("int");

                    b.Property<decimal?>("Salary")
                        .HasColumnName("salary")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Street")
                        .HasColumnName("street")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int?>("UnionMemNo")
                        .HasColumnName("union_mem_no")
                        .HasColumnType("int");

                    b.Property<int?>("Zip")
                        .HasColumnName("zip")
                        .HasColumnType("int");

                    b.HasKey("Ssn")
                        .HasName("PK__Employee__DDDF0AE753CB1BA3");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Airport_Database.Models.ExpertiseIn", b =>
                {
                    b.Property<int>("Ssn")
                        .HasColumnName("ssn")
                        .HasColumnType("int");

                    b.Property<int>("ModelNo")
                        .HasColumnName("model_no")
                        .HasColumnType("int");

                    b.HasKey("Ssn", "ModelNo")
                        .HasName("PK__Expertis__C01C9ADBBAA20A8E");

                    b.HasIndex("ModelNo");

                    b.ToTable("Expertise_In");
                });

            modelBuilder.Entity("Airport_Database.Models.Model", b =>
                {
                    b.Property<int>("ModelNo")
                        .HasColumnName("model_no")
                        .HasColumnType("int");

                    b.Property<int?>("Capacity")
                        .HasColumnName("capacity")
                        .HasColumnType("int");

                    b.Property<int?>("Weight")
                        .HasColumnName("weight")
                        .HasColumnType("int");

                    b.HasKey("ModelNo")
                        .HasName("PK__Model__DC3903C043D89DD8");

                    b.ToTable("Model");
                });

            modelBuilder.Entity("Airport_Database.Models.Technician", b =>
                {
                    b.Property<int>("Ssn")
                        .HasColumnName("ssn")
                        .HasColumnType("int");

                    b.HasKey("Ssn")
                        .HasName("PK__Technici__DDDF0AE71B3FC6C1");

                    b.ToTable("Technician");
                });

            modelBuilder.Entity("Airport_Database.Models.Test", b =>
                {
                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("date");

                    b.Property<int?>("NoHours")
                        .HasColumnName("no_hours")
                        .HasColumnType("int");

                    b.Property<long?>("RegistrationNo")
                        .HasColumnName("registration_no")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Score")
                        .HasColumnName("score")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("Ssn")
                        .HasColumnName("ssn")
                        .HasColumnType("int");

                    b.Property<int?>("TestNo")
                        .HasColumnName("test_no")
                        .HasColumnType("int");

                    b.HasIndex("RegistrationNo");

                    b.HasIndex("Ssn");

                    b.HasIndex("TestNo");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Airport_Database.Models.TestInfo", b =>
                {
                    b.Property<int>("TestNo")
                        .HasColumnName("test_no")
                        .HasColumnType("int");

                    b.Property<int?>("MaxScore")
                        .HasColumnName("max_score")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("TestNo")
                        .HasName("PK__Test_Inf__F3F872D11F94AEF5");

                    b.ToTable("Test_Info");
                });

            modelBuilder.Entity("Airport_Database.Models.TrafficController", b =>
                {
                    b.Property<int>("Ssn")
                        .HasColumnName("ssn")
                        .HasColumnType("int");

                    b.Property<DateTime?>("MostRecentExamDate")
                        .HasColumnName("most_recent_exam_date")
                        .HasColumnType("date");

                    b.HasKey("Ssn")
                        .HasName("PK__Traffic___DDDF0AE7F1793830");

                    b.ToTable("Traffic_Controller");
                });

            modelBuilder.Entity("Airport_Database.Models.Airplane", b =>
                {
                    b.HasOne("Airport_Database.Models.Model", "ModelNoNavigation")
                        .WithMany("Airplane")
                        .HasForeignKey("ModelNo")
                        .HasConstraintName("FK__Airplane__model___2D27B809");
                });

            modelBuilder.Entity("Airport_Database.Models.ExpertiseIn", b =>
                {
                    b.HasOne("Airport_Database.Models.Model", "ModelNoNavigation")
                        .WithMany("ExpertiseIn")
                        .HasForeignKey("ModelNo")
                        .HasConstraintName("FK__Expertise__model__440B1D61")
                        .IsRequired();

                    b.HasOne("Airport_Database.Models.Employee", "SsnNavigation")
                        .WithMany("ExpertiseIn")
                        .HasForeignKey("Ssn")
                        .HasConstraintName("FK__Expertise_I__ssn__4316F928")
                        .IsRequired();
                });

            modelBuilder.Entity("Airport_Database.Models.Technician", b =>
                {
                    b.HasOne("Airport_Database.Models.Employee", "SsnNavigation")
                        .WithOne("Technician")
                        .HasForeignKey("Airport_Database.Models.Technician", "Ssn")
                        .HasConstraintName("FK__Technician__ssn__286302EC")
                        .IsRequired();
                });

            modelBuilder.Entity("Airport_Database.Models.Test", b =>
                {
                    b.HasOne("Airport_Database.Models.Airplane", "RegistrationNoNavigation")
                        .WithMany()
                        .HasForeignKey("RegistrationNo")
                        .HasConstraintName("FK__Test__registrati__36B12243");

                    b.HasOne("Airport_Database.Models.Technician", "SsnNavigation")
                        .WithMany()
                        .HasForeignKey("Ssn")
                        .HasConstraintName("FK__Test__ssn__37A5467C");

                    b.HasOne("Airport_Database.Models.TestInfo", "TestNoNavigation")
                        .WithMany()
                        .HasForeignKey("TestNo")
                        .HasConstraintName("FK__Test__test_no__35BCFE0A");
                });

            modelBuilder.Entity("Airport_Database.Models.TrafficController", b =>
                {
                    b.HasOne("Airport_Database.Models.Employee", "SsnNavigation")
                        .WithOne("TrafficController")
                        .HasForeignKey("Airport_Database.Models.TrafficController", "Ssn")
                        .HasConstraintName("FK__Traffic_Con__ssn__25869641")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
