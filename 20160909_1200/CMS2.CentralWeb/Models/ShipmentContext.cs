using System.Data.Entity;
using CMS2.CentralWeb.Models.CMS1;

namespace CMS2.CentralWeb.Models
{
    public partial class ShipmentContext : DbContext
    {
        public ShipmentContext()
            : base("name=Shipment")
        {
        }

        public virtual DbSet<cargo> cargoes { get; set; }
        public virtual DbSet<destination> destinations { get; set; }
        public virtual DbSet<payment> payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cargo>()
                .Property(e => e.issuedby)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.pickedupby)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.origincode)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.destcode)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.consigneename)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.consigneecompany)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.consigneeaddress)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.consigneetels)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.consigneeemail)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.shippername)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.shippercompany)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.shipperaddress)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.shippertels)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.shipperemail)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.descriptionofgoods)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.servicecode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.paymentcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.declaredvalue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.otherchargesdesc)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.othersamount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.actualweight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<cargo>()
                .Property(e => e.chargeableweight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<cargo>()
                .Property(e => e.weightcharge)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.awbfee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.valuation)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.deliveryfee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.peracfee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.freightcollect)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.fuelsurcharge)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.cratingfee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.insurance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.subtotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.evat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.grandtotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.encodedby)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.statusby)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.reportid)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.Others2amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cargo>()
                .Property(e => e.consigneecode)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.shippercode)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.plateno)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.paymentterms)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.lastupdateby)
                .IsUnicode(false);

            modelBuilder.Entity<cargo>()
                .Property(e => e.isTranshipment)
                .IsFixedLength();

            modelBuilder.Entity<cargo>()
                .Property(e => e.dangerousfee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<payment>()
                .Property(e => e.airwaybill)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.clientname)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.paymentcode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.iscash)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<payment>()
                .Property(e => e.orpr)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.branch)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<payment>()
                .Property(e => e.postedby)
                .IsUnicode(false);
        }
    }
}
