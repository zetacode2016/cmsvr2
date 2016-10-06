using CMS2.DataAccess;

namespace CMS2.Client
{
    public static class GlobalServices
    {
        //public static BookingBL BookingService { get; set; }
        //public static BranchCorpOfficeBL BranchCorpOfficeService { get; set; }
        //public static BranchSatOfficeBL BranchSatOfficeService { get; set; }
        //public static ClientBL ClientService { get; set; }
        //public static CityBL CityService { get; set; }
        //public static ClusterBL ClusterService { get; set; }
        //public static CommodityTypeBL CommodityTypeService { get; set; }
        //public static BookingStatusBL BookingStatusService { get; set; }
        //public static BookingRemarkBL BookingRemarkService { get; set; }
        //public static EmployeeBL EmployeeService { get; set; }
        //public static ExpressRateBL ExpressRateService { get; set; }
        //public static GatewaySatOfficeBL GatewaySatOfficeService { get; set; }
        //public static ServiceModeBL ServiceModeService { get; set; }
        //public static PaymentModeBL PaymentModeService { get; set; }
        //public static ShipmentBL ShipmentService { get; set; }
        //public static PaymentBL PaymentService { get; set; }
        //public static ShipmentBasicFeeBL ShipmentBasicFeeService { get; set; }
        //public static StatementOfAccountPaymentBL SoaPaymentService { get; set; }
        //public static PaymentTurnoverBL PaymentTurnoverService { get; set; }
        //public static StatementOfAccountBL SoaService { get; set; }
        //public static PaymentTypeBL PaymentTypeService { get; set; }
        //public static AreaBL AreaService { get; set; }
        //public static FuelSurchargeBL FuelSurchargeService { get; set; }
        //public static RevenueUnitBL RevenueUnitService { get; set; }

        public static void InstantiateServices()
        {
            //GlobalVars.UnitOfWork = new CmsUoW();
            //BranchCorpOfficeService = new BranchCorpOfficeBL(GlobalVars.UnitOfWork);
            //BranchSatOfficeService = new BranchSatOfficeBL(GlobalVars.UnitOfWork);
            //ClientService = new ClientBL(GlobalVars.UnitOfWork);
            //CityService = new CityBL(GlobalVars.UnitOfWork);
            //ClusterService = new ClusterBL(GlobalVars.UnitOfWork);
            //CommodityTypeService = new CommodityTypeBL(GlobalVars.UnitOfWork);
            //EmployeeService = new EmployeeBL(GlobalVars.UnitOfWork);
            //BookingStatusService = new BookingStatusBL(GlobalVars.UnitOfWork);
            //BookingRemarkService = new BookingRemarkBL(GlobalVars.UnitOfWork);
            //ExpressRateService = new ExpressRateBL(GlobalVars.UnitOfWork);
            //GatewaySatOfficeService = new GatewaySatOfficeBL(GlobalVars.UnitOfWork);
            //ServiceModeService = new ServiceModeBL(GlobalVars.UnitOfWork);
            //PaymentModeService = new PaymentModeBL(GlobalVars.UnitOfWork);
            //BookingService = new BookingBL(GlobalVars.UnitOfWork);
            //ShipmentService = new ShipmentBL(GlobalVars.UnitOfWork);
            //PaymentService = new PaymentBL(GlobalVars.UnitOfWork);
            //ShipmentBasicFeeService = new ShipmentBasicFeeBL(GlobalVars.UnitOfWork);
            //SoaPaymentService =new StatementOfAccountPaymentBL(GlobalVars.UnitOfWork);
            //PaymentTurnoverService = new PaymentTurnoverBL(GlobalVars.UnitOfWork);
            //SoaService = new StatementOfAccountBL(GlobalVars.UnitOfWork);
            //PaymentTypeService = new PaymentTypeBL(GlobalVars.UnitOfWork);
            //AreaService = new AreaBL(GlobalVars.UnitOfWork);
            //FuelSurchargeService = new FuelSurchargeBL(GlobalVars.UnitOfWork);
            //RevenueUnitService = new RevenueUnitBL(GlobalVars.UnitOfWork);
        }

        public static void GetData()
        {
            //RefreshBCOs();
            //RefreshBSOs();
            //RefreshClients();
            //RefreshCities();
            //RefreshClusters();
            //RefreshCommodityTypes();
            //RefreshEmployees();
            //RefreshFieldReps();
            //RefreshGatewaySatOffices();
            //RefreshBookingStatuses();
            //RefreshBookingRemarks();
            //RefreshExpressRates();
            //RefreshServiceModes();
            //RefreshPaymentModes();
            //RefreshPaymentTerms();
            //RefreshBookings();
            //RefreshShipments();
            //RefreshShipmentBasicFees();
            //RefreshPaymentTypes();
            //RefreshAreas();
            //RefreshFuelSurcharge();
            //RefreshRevenueUnits();
        }
        
        //public static void GetClientsBy(Expression<Func<Entities.Client, bool>> filter)
        //{
        //    GlobalVars.Clients = ClientService.FilterActiveBy(filter);
        //}

        #region Get_RefreshData
        //public static void RefreshBCOs()
        //{
        //    GlobalVars.BranchCorpOffices = BranchCorpOfficeService.FilterActive().OrderBy(x=>x.BranchCorpOfficeName).ToList();
        //}

        //public static void RefreshBSOs()
        //{
        //    GlobalVars.BranchSatOffices = BranchSatOfficeService.FilterActive().OrderBy(x=>x.RevenueUnitName).ToList();
        //}
        //public static void RefreshClients()
        //{
        //    GlobalVars.Clients = ClientService.FilterActive();
        //}
        
        //public static void RefreshCities()
        //{
        //    GlobalVars.Cities = CityService.FilterActive().OrderBy(x => x.CityName).ToList();
        //}

        //public static void RefreshClusters()
        //{
        //    GlobalVars.Clusters = ClusterService.FilterActive().OrderBy(x => x.ClusterName).ToList();
        //}
        
        //public static void RefreshEmployees()
        //{
        //    GlobalVars.Employees = EmployeeService.FilterActive().OrderBy(x => x.FullName).ToList();
        //}

        //public static void RefreshFieldReps()
        //{
        //    var fieldRepIds = EmployeeService.GetByPosition("Field Rep").Select(x => x.Value).ToList();
        //    if (fieldRepIds == null || fieldRepIds.Count == 0)
        //    { }
        //    else
        //    {
        //        GlobalVars.FieldReps = EmployeeService.FilterActiveBy(x => fieldRepIds.Contains(x.EmployeeId)).OrderBy(x => x.FullName).ToList();
        //    }
        //}

        //public static void RefreshGatewaySatOffices()
        //{
        //    GlobalVars.GatewaySatOffices = GatewaySatOfficeService.FilterActive().OrderBy(x=>x.RevenueUnitName).ToList();
        //}

        //public static void RefreshBookingStatuses()
        //{
        //    GlobalVars.BookingStatuses = BookingStatusService.FilterActive().OrderBy(x => x.ListOrder).ToList();
        //}

        //public static void RefreshBookingRemarks()
        //{
        //    var remarks = BookingRemarkService.FilterActive();
        //    remarks.Add(new BookingRemark(){BookingRemarkName = ""});
        //    remarks.Sort((x, y) => x.BookingRemarkName.CompareTo(y.BookingRemarkName));
        //    GlobalVars.BookingRemarks = remarks;
        //}

        //public static void RefreshCommodityTypes()
        //{
        //    GlobalVars.CommodityTypes = CommodityTypeService.FilterActive().OrderBy(x=>x.CommodityTypeName).ToList();
        //}

        //public static void RefreshExpressRates()
        //{
        //    GlobalVars.ExpressRates = ExpressRateService.FilterActive();
        //}

        //public static void RefreshServiceModes()
        //{
        //    GlobalVars.ServiceModes = ServiceModeService.FilterActive();
        //}

        //public static void RefreshPaymentModes()
        //{
        //    GlobalVars.PaymentModes = PaymentModeService.FilterActive();
        //}

        //public static void RefreshPaymentTerms()
        //{
        //    GlobalVars.PaymentTerms = PaymentTermService.FilterActive();
        //}

        //public static void RefreshBookings()
        //{
        //    GlobalVars.Bookings = BookingService.FilterActive();
        //}

        //public static void RefreshShipments()
        //{
        //    GlobalVars.Shipments = ShipmentService.FilterActive();
        //}

        //public static void RefreshShipmentBasicFees()
        //{
        //    GlobalVars.ShipmentBasicFees = ShipmentBasicFeeService.FilterActive();
        //}

        //public static void RefreshPaymentTypes()
        //{
        //    GlobalVars.PaymentTypes = PaymentTypeService.FilterActive();
        //}

        //public static void RefreshAreas()
        //{
        //    GlobalVars.Areas = AreaService.FilterActive().OrderBy(x=>x.RevenueUnitName).ToList();
        //}

        //public static void RefreshFuelSurcharge()
        //{
        //    GlobalVars.FuelSurcharges = FuelSurchargeService.FilterActive();
        //}

        //public static void RefreshRevenueUnits()
        //{
        //    GlobalVars.RevenueUnits = RevenueUnitService.FilterActive().OrderBy(x=>x.RevenueUnitName).ToList();
        //}
        #endregion


    }
}
