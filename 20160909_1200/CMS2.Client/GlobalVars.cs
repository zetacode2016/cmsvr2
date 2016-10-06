using System;
using CMS2.Client.SyncHelper;
using CMS2.DataAccess.Interfaces;

namespace CMS2.Client
{
    public static class GlobalVars
    {
        public static ICmsUoW UnitOfWork { get; set; }
        public static Guid DeviceRevenueUnitId { get; set; }
        public static Guid DeviceCityId { get; set; }
        public static Guid DeviceBcoId { get; set; }
        public static bool IsSubserver { get; set; }
        public static AutoSync autoSync;

        //public static List<BookingStatus> BookingStatuses { get; set; }
        //public static List<BookingRemark> BookingRemarks { get; set; }
        //public static List<BranchCorpOffice> BranchCorpOffices { get; set; }
        //public static List<RevenueUnit> BranchSatOffices { get; set; }
        //public static List<Entities.Client> Clients { get; set; }
        //public static List<City> Cities { get; set; }
        //public static List<Booking> Bookings { get; set; }
        //public static List<Cluster> Clusters { get; set; }
        //public static CommodityType CommodityType { get; set; }
        //public static List<CommodityType> CommodityTypes { get; set; }
        //public static List<Employee> Employees { get; set; }
        //public static List<Employee> FieldReps { get; set; }
        //public static List<RevenueUnit> GatewaySatOffices { get; set; }
        //public static List<ExpressRate> ExpressRates { get; set; }
        //public static List<ServiceMode> ServiceModes { get; set; }
        //public static List<PaymentMode> PaymentModes { get; set; }
        //public static List<PaymentTerm> PaymentTerms { get; set; }
        //public static List<PaymentType> PaymentTypes { get; set; } 
        //public static List<Shipment> Shipments { get; set; }
        //public static List<ShipmentBasicFee> ShipmentBasicFees { get; set; }
        //public static List<RevenueUnit> Areas { get; set; }
        //public static List<FuelSurcharge> FuelSurcharges { get; set; }
        //public static List<RevenueUnit> RevenueUnits { get; set; }
    }
}
