using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Molecules.Core.Data.Migrations
{
    [DbContext(typeof(MoleculesDbContext))]
    partial class MoleculesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("moleculesapp")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Molecules.Core.Data.DbEntities.CalcOrderDbEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("CustomerName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("character varying(250)");

                b.HasKey("Id");

                b.ToTable("CalcOrder", "moleculesapp");
            });

            modelBuilder.Entity("Molecules.Core.Data.DbEntities.CalcOrderItemDbEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("BasissetCode")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<int>("CalcOrderId")
                    .HasColumnType("integer");

                b.Property<string>("CalcType")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<int>("Charge")
                    .HasColumnType("integer");

                b.Property<string>("MoleculeName")
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("character varying(250)");

                b.Property<string>("XYZ")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("CalcOrderId");

                b.ToTable("CalcOrderItem", "moleculesapp");
            });

            modelBuilder.Entity("Molecules.Core.Data.DbEntities.MoleculeDbEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("BasisSet")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Molecule")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("MoleculeName")
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("character varying(250)");

                b.Property<string>("OrderName")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("MoleculeName", "OrderName", "BasisSet")
                    .IsUnique();

                b.ToTable("Molecule", "moleculesapp");
            });

            modelBuilder.Entity("Molecules.Core.Data.DbEntities.CalcOrderItemDbEntity", b =>
            {
                b.HasOne("Molecules.Core.Data.DbEntities.CalcOrderDbEntity", "CalcOrder")
                    .WithMany("CalcOrderItems")
                    .HasForeignKey("CalcOrderId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("CalcOrder");
            });

            modelBuilder.Entity("Molecules.Core.Data.DbEntities.CalcOrderDbEntity", b =>
            {
                b.Navigation("CalcOrderItems");
            });
#pragma warning restore 612, 618
        }
    }
}
