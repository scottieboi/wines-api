using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WinesApi.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Box> Boxes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Vineyard> Vineyards { get; set; }
        public virtual DbSet<Winelist> Winelists { get; set; }
        public virtual DbSet<Winetype> Winetypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "C.UTF-8");

            modelBuilder.Entity<Box>(entity =>
            {
                entity.HasKey(e => e.Boxno)
                    .HasName("box_pkey");

                entity.ToTable("box");

                entity.Property(e => e.Boxno)
                    .ValueGeneratedNever()
                    .HasColumnName("boxno");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasIndex(e => e.Box, "location_box_fk");

                entity.HasIndex(e => e.Wineid, "location_wineid_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Box).HasColumnName("box");

                entity.Property(e => e.Cellarversion).HasColumnName("cellarversion");

                entity.Property(e => e.No).HasColumnName("no");

                entity.Property(e => e.Wineid).HasColumnName("wineid");

                entity.HasOne(d => d.BoxNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.Box)
                    .HasConstraintName("location_box_fkey");

                entity.HasOne(d => d.Wine)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.Wineid)
                    .HasConstraintName("location_wineid_fkey");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Region1).HasColumnName("region");
            });

            modelBuilder.Entity<Vineyard>(entity =>
            {
                entity.ToTable("vineyard");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Vineyard1).HasColumnName("vineyard");
            });

            modelBuilder.Entity<Winelist>(entity =>
            {
                entity.ToTable("winelist");

                entity.HasIndex(e => e.Regionid, "winelist_regionid_fk");

                entity.HasIndex(e => e.Vineyardid, "winelist_vineyardid_fk");

                entity.HasIndex(e => e.Winetypeid, "winelist_winetypeid_fkey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bottlesize).HasColumnName("bottlesize");

                entity.Property(e => e.Drinkrangefrom).HasColumnName("drinkrangefrom");

                entity.Property(e => e.Drinkrangeto).HasColumnName("drinkrangeto");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Percentalcohol).HasColumnName("percentalcohol");

                entity.Property(e => e.Pricepaid)
                    .HasPrecision(7, 2)
                    .HasColumnName("pricepaid");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Regionid).HasColumnName("regionid");

                entity.Property(e => e.Vineyardid).HasColumnName("vineyardid");

                entity.Property(e => e.Vintage).HasColumnName("vintage");

                entity.Property(e => e.Winename).HasColumnName("winename");

                entity.Property(e => e.Winetypeid).HasColumnName("winetypeid");

                entity.Property(e => e.Yearbought).HasColumnName("yearbought");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Winelists)
                    .HasForeignKey(d => d.Regionid)
                    .HasConstraintName("winelist_regionid_fkey");

                entity.HasOne(d => d.Vineyard)
                    .WithMany(p => p.Winelists)
                    .HasForeignKey(d => d.Vineyardid)
                    .HasConstraintName("winelist_vineyardid_fkey");

                entity.HasOne(d => d.Winetype)
                    .WithMany(p => p.Winelists)
                    .HasForeignKey(d => d.Winetypeid)
                    .HasConstraintName("winelist_winetypeid_fkey");
            });

            modelBuilder.Entity<Winetype>(entity =>
            {
                entity.ToTable("winetype");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('location_id_seq'::regclass)");

                entity.Property(e => e.Winetype1).HasColumnName("winetype");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}