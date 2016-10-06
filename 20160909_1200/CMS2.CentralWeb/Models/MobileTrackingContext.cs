using System.Data.Entity;

namespace CMS2.CentralWeb.Models
{
    public partial class MobileTrackingContext : DbContext
    {
        public MobileTrackingContext()
            : base("name=MobileTracking")
        {
        }

        public virtual DbSet<CMS2_Acceptance> CMS2_Acceptance { get; set; }
        public virtual DbSet<CMS2_Area> CMS2_Area { get; set; }
        public virtual DbSet<CMS2_Booking> CMS2_Booking { get; set; }
        public virtual DbSet<CMS2_Branch> CMS2_Branch { get; set; }
        public virtual DbSet<CMS2_Consignee> CMS2_Consignee { get; set; }
        public virtual DbSet<CMS2_Deliver> CMS2_Deliver { get; set; }
        public virtual DbSet<CMS2_Dimension> CMS2_Dimension { get; set; }
        public virtual DbSet<CMS2_ErrorLogs> CMS2_ErrorLogs { get; set; }
        public virtual DbSet<CMS2_Franchise> CMS2_Franchise { get; set; }
        public virtual DbSet<CMS2_Group> CMS2_Group { get; set; }
        public virtual DbSet<CMS2_Payment> CMS2_Payment { get; set; }
        public virtual DbSet<CMS2_Shipper> CMS2_Shipper { get; set; }
        public virtual DbSet<CMS2_Track> CMS2_Track { get; set; }
        public virtual DbSet<CMS2_TruckInfo> CMS2_TruckInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CMS2_Acceptance>()
                .Property(e => e.id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CMS2_Area>()
                .Property(e => e.locationcode)
                .IsFixedLength();

            modelBuilder.Entity<CMS2_Booking>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_Branch>()
                .Property(e => e.desc)
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_Branch>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_Branch>()
                .Property(e => e.tels)
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_Branch>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_Branch>()
                .Property(e => e.locationcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_Dimension>()
                .Property(e => e.qty)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CMS2_Dimension>()
                .Property(e => e.agw)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CMS2_Dimension>()
                .Property(e => e.l)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CMS2_Dimension>()
                .Property(e => e.w)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CMS2_Dimension>()
                .Property(e => e.h)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CMS2_Franchise>()
                .Property(e => e.locationcode)
                .IsFixedLength();

            modelBuilder.Entity<CMS2_Group>()
                .Property(e => e.locationcode)
                .IsFixedLength();

            modelBuilder.Entity<CMS2_TruckInfo>()
                .Property(e => e.plate_no)
                .IsUnicode(false);

            modelBuilder.Entity<CMS2_TruckInfo>()
                .Property(e => e.contact_no)
                .IsUnicode(false);
        }
    }
}
