using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CMS2.Entities;

namespace CMS2.DataAccess
{
    public class CmsContext : DbContext
    {
        public CmsContext()
            : base("name=Cms")
        {
        }

        public CmsContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<AccountStatus> AccountStatus { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AdjustmentReason> AdjustmentReasons { get; set; }
        public DbSet<ApplicableRate> ApplicableRates { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<ApprovingAuthority> ApprovingAuthorities { get; set; }
        public DbSet<AwbIssuance> AwbIssuances { get; set; }
        public DbSet<BillingPeriod> BillingPeriods { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRemark> BookingRemarks { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<BranchCorpOffice> BranchCorpOffices { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        //public DbSet<Bundle> Bundles { get; set; }
        //public DbSet<CargoReceived> CargoReceived { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeliveredPackage> DeliveredPackages { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryRemark> DeliveryRemarks { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatus { get; set; }
        public DbSet<DeliveryReceipt> DeliveryReceipts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePositionMapping> EmployeePositionMappings { get; set; }
        public DbSet<ExpressRate> ExpressRates { get; set; }
        //public DbSet<Function> Functions { get; set; }
        public DbSet<FlightInfo> FlightInfos { get; set; }
        public DbSet<FuelSurcharge> FuelSurcharges { get; set; }
        //public DbSet<Gateway> Gateways { get; set; }
        //public DbSet<GatewayType> GatewayTypes { get; set; }
        //public DbSet<GatewayTransmittal> GatewayTransmittals { get; set; }
        //public DbSet<GatewayTransmittalPackageNumber> GatewayTransmittalPackageNumbers { get; set; }
        public DbSet<GoodsDescription> GoodsDescriptions { get; set; }
        public DbSet<Group> Groups { get; set; }
        //public DbSet<Inbound> Inbounds { get; set; }
        public DbSet<Industry> Industries { get; set; }
        //public DbSet<ManifestAwb> ManifestAwbs { get; set; }
        //public DbSet<Module> Modules { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<PackageDimension> PackageDimensions { get; set; }
        public DbSet<PackageNumber> PackageNumbers { get; set; }
        public DbSet<PackageNumberAcceptance> PackageNumberAcceptances { get; set; }
        public DbSet<PackageTransfer> PackageTransfers { get; set; }
        public DbSet<PackageNumberTransfer> PackageNumberTransfers { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentTurnover> PaymentTurnovers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RateMatrix> RateMatrix { get; set; }
        public DbSet<RecordChange> RecordChanges { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RevenueUnit> RevenueUnits { get; set; }
        public DbSet<RevenueUnitType> RevenueUnitTypes { get; set; }
        //public DbSet<RoleMenuMapping> RoleMenuMappings { get; set; }
        public DbSet<ServiceMode> ServiceModes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipMode> ShipModes { get; set; }
        public DbSet<ShipmentAdjustment> ShipmentAdjustments { get; set; }
        public DbSet<ShipmentBasicFee> ShipmentBasicFees { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatus { get; set; }
        public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }
        public DbSet<StatementOfAccount> StatementOfAccounts { get; set; }
        public DbSet<StatementOfAccountNumber> StatementOfAccountNumbers { get; set; }
        public DbSet<StatementOfAccountPayment> StatementOfAccountPayments { get; set; }
        public DbSet<StatementOfAccountPrint> StatementOfAccountPrints { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<TerminalRevenueUnitMapping> TerminalRevenueUnitMappings { get; set; }
        public DbSet<TntMaint> TntMaints { get; set; }
        public DbSet<TransmittalStatus> TransmittalStatus { get; set; }
        public DbSet<TransferAcceptance> TransferAcceptances { get; set; }
        public DbSet<TransShipmentLeg> TransShipmentLegs { get; set; }
        public DbSet<TransShipmentRoute> TransShipmentRoutes { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckAreaMapping> TruckAreaMappings { get; set; }
        public DbSet<WeightBreak> WeigthBreaks { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        //public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // prevents the table names from being pluralized
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<AwbIssuance>().HasRequired(x => x.IssuedTo).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>().HasRequired(x => x.Consignee).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.Shipper).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.BookingStatus).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>().Property(x => x.CompanyId).IsOptional();
            modelBuilder.Entity<Client>().Property(x => x.AreaId).IsOptional();

            modelBuilder.Entity<Cluster>().HasRequired(x => x.BranchCorpOffice).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>().Property(x => x.PaymentTermId).IsOptional();
            modelBuilder.Entity<Company>().Property(x => x.Discount).HasPrecision(9, 2);
            modelBuilder.Entity<Company>().Property(x => x.CreditLimit).HasPrecision(9, 2);
            modelBuilder.Entity<Company>().HasRequired(x => x.BillingCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Company>().Property(x => x.AreaId).IsOptional();

            modelBuilder.Entity<Crating>().Property(x => x.Multiplier).HasPrecision(10, 10);

            modelBuilder.Entity<Delivery>().HasRequired(x => x.DeliveredBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ExpressRate>().Property(x => x.MinimumWeight).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().Property(x => x.MaximumWeight).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().Property(x => x.Cost).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ExpressRate>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<FlightInfo>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FlightInfo>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<FuelSurcharge>().HasRequired(x => x.OriginGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FuelSurcharge>().HasRequired(x => x.DestinationGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FuelSurcharge>().Property(x => x.Amount).HasPrecision(9, 2);
            
            modelBuilder.Entity<PackageTransfer>().HasRequired(x => x.ScannedBy).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PackageTransfer>().HasRequired(x => x.Driver).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>().Property(x => x.Amount).HasPrecision(9, 2);
            modelBuilder.Entity<Payment>().Property(x => x.TaxWithheld).HasPrecision(9, 2);
            modelBuilder.Entity<Payment>().HasRequired(x => x.ReceivedBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentTurnover>().Property(x => x.ReceivedCashAmount).HasPrecision(9, 2);
            modelBuilder.Entity<PaymentTurnover>().Property(x => x.ReceivedCheckAmount).HasPrecision(9, 2);
            modelBuilder.Entity<PaymentTurnover>().HasRequired(x => x.CollectedBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment>().Property(x => x.Weight).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.DeclaredValue).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.HandlingFee).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.QuarantineFee).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.Discount).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.AwbFeeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.FreightCollectChargeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.FuelSurchargeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.PeracFeeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.InsuranceId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.EvatId).IsOptional();
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Consignee).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Shipper).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.AcceptedBy).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Booking).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Commodity).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ShipmentAdjustment>().Property(x => x.AdjustmentAmount).HasPrecision(9, 2);

            modelBuilder.Entity<ShipmentBasicFee>().Property(x => x.Amount).HasPrecision(9, 2);

            modelBuilder.Entity<ShipmentTracking>().HasRequired(x => x.TrackedBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<StatementOfAccount>().HasRequired(x => x.Company).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TransferAcceptance>().HasRequired(x => x.ScannedBy).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<TransferAcceptance>().HasRequired(x => x.Driver).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TransShipmentRoute>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<TransShipmentRoute>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }


    }
}
