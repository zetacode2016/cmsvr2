using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.DataAccess;
using CMS2.DataAccess.Interfaces;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class MaintenanceController : AdminBaseController
    {
        private ICmsUoW uow;
        private GroupBL groupService;
        private RegionBL regionService;
        private BranchCorpOfficeBL bcoService;
        private ClusterBL clusterService;
        private CityBL cityService;
        private AreaBL areaService;
        private BranchSatOfficeBL bsoService;
        private GatewaySatOfficeBL gatewaySatService;
        private ApplicableRateBL applicableRateService;
        private CommodityTypeBL commodityTypeService;
        private CommodityBL commodityService;
        private RevenueUnitTypeBL revenueUnitTypeService;
        private GoodsDescriptionBL goodsDescService;
        private ShipmentBasicFeeBL shipmentBasicFeeService;
        private CratingBL cratingService;
        private PackagingBL packagingService;
        private TransShipmentRouteBL tranShipmentService;

        private ServiceTypeBL serviceTypeService;
        private ServiceModeBL serviceModeService;
        private PaymentModeBL paymentModeService;
        private PaymentTermBL paymentTermService;
        private ShipModeBL shipModeService;
        private BookingStatusBL bookingStatusService;
        private BookingRemarkBL bookingRemarkService;
        private DeliveryRemarkBL deliveryRemarkService;
        private DeliveryStatusBL deliveryStatusService;
        //private GatewayTypeBL gatewayTypeService;
        //private GatewayBL gatewayService;

        private AccountTypeBL accountTypeService;
        private AccountStatusBL accountStatusService;
        private BusinessTypeBL businessTypeService;
        private IndustryBL industryService;
        private OrganizationTypeBL organizationTypeService;
        private BillingPeriodBL billingPeriodService;

        public MaintenanceController()
        {
            uow = new CmsUoW();
            groupService = new GroupBL(uow);
            regionService = new RegionBL(uow);
            bcoService = new BranchCorpOfficeBL(uow);
            clusterService = new ClusterBL(uow);
            cityService = new CityBL(uow);
            areaService = new AreaBL(uow);
            bsoService = new BranchSatOfficeBL(uow);
            gatewaySatService = new GatewaySatOfficeBL(uow);
            applicableRateService = new ApplicableRateBL(uow);
            commodityTypeService = new CommodityTypeBL(uow);
            commodityService = new CommodityBL(uow);
            revenueUnitTypeService = new RevenueUnitTypeBL(uow);
            goodsDescService = new GoodsDescriptionBL(uow);
            shipmentBasicFeeService = new ShipmentBasicFeeBL(uow);
            cratingService = new CratingBL(uow);
            packagingService = new PackagingBL(uow);
            tranShipmentService = new TransShipmentRouteBL(uow);

             serviceModeService = new ServiceModeBL(uow);
            serviceTypeService = new ServiceTypeBL(uow);
            paymentModeService = new PaymentModeBL(uow);
            paymentTermService = new PaymentTermBL(uow);
            shipModeService = new ShipModeBL(uow);
            bookingRemarkService = new BookingRemarkBL(uow);
            bookingStatusService = new BookingStatusBL(uow);
            deliveryRemarkService = new DeliveryRemarkBL(uow);
            deliveryStatusService = new DeliveryStatusBL(uow);
            //gatewayTypeService = new GatewayTypeBL(uow);
            //gatewayService = new GatewayBL(uow);
            accountStatusService = new AccountStatusBL(uow);
            accountTypeService = new AccountTypeBL(uow);
            businessTypeService = new BusinessTypeBL(uow);
            industryService = new IndustryBL(uow);
            organizationTypeService = new OrganizationTypeBL(uow);
            billingPeriodService = new BillingPeriodBL(uow);
        }

        public ActionResult Index()
        {
            ViewBag.Groups = new SelectList(groupService.FilterActive().OrderBy(x => x.GroupName).ToList(), "GroupId", "GroupName");
            ViewBag.Regions = new SelectList(regionService.FilterActive().OrderBy(x => x.RegionName).ToList(), "RegionId", "RegionName");
            ViewBag.Bcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            ViewBag.Clusters = new SelectList(clusterService.FilterActive().OrderBy(x => x.ClusterName).ToList(), "ClusterId", "ClusterName");
            ViewBag.Cities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Areas = new SelectList(areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName");
            ViewBag.Bsos = new SelectList(bsoService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName");
            ViewBag.GatewaySats = new SelectList(gatewaySatService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName");
            ViewBag.ApplicableRates = new SelectList(applicableRateService.FilterActive().OrderBy(x => x.ApplicableRateName).ToList(), "ApplicableRateId", "ApplicableRateName");
            ViewBag.CommodityTypes = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeId", "CommodityTypeName");
            ViewBag.Commodities = new SelectList(commodityService.FilterActive().OrderBy(x => x.CommodityName).ToList(), "CommodityId", "CommodityName");
            ViewBag.RevenueUnitTypes =new SelectList(revenueUnitTypeService.FilterActive().OrderBy(x => x.RevenueUnitTypeName).ToList(),"RevenueUnitTypeId", "RevenueUnitTypeName");
            ViewBag.GoodDescs =new SelectList(goodsDescService.FilterActive().OrderBy(x => x.GoodsDescriptionName).ToList(),"GoodsDescriptionId", "GoodsDescriptionName");
            ViewBag.Cratings = new SelectList(cratingService.FilterActive().OrderBy(x => x.CratingName).ToList(), "CratingId", "CratingName");
            ViewBag.Packagings = new SelectList(packagingService.FilterActive().OrderBy(x => x.PackagingName).ToList(), "PackagingId", "PackagingName");
            ViewBag.TranShipments = new SelectList(tranShipmentService.FilterActive().OrderBy(x => x.TransShipmentRouteName).ToList(), "TransShipmentRouteId", "TransShipmentRouteName");
            ViewBag.BasicFees = new SelectList(shipmentBasicFeeService.FilterActive().OrderBy(x => x.ShipmentFeeName).ToList(), "ShipmentBasicFeeId", "ShipmentFeeName");
            ViewBag.ServiceModes = new SelectList(serviceModeService.FilterActive().OrderBy(x => x.ServiceModeName).ToList(), "ServiceModeId", "ServiceModeName");
            ViewBag.ServiceTypes = new SelectList(serviceTypeService.FilterActive().OrderBy(x => x.ServiceTypeName).ToList(), "ServiceTypeId", "serviceTypeName");
            ViewBag.PaymentTerms = new SelectList(paymentTermService.FilterActive().OrderBy(x => x.PaymentTermName).ToList(), "PaymentTermId", "PaymentTermName");
            ViewBag.PaymentMode = new SelectList(paymentModeService.FilterActive().OrderBy(x => x.PaymentModeName).ToList(), "PaymentModeId", "PaymentModeName");
            ViewBag.ShipModes = new SelectList(shipModeService.FilterActive().OrderBy(x => x.ShipModeName).ToList(), "ShipModeId", "ShipModeName");
            ViewBag.BookingStatus = new SelectList(bookingStatusService.FilterActive().OrderBy(x => x.BookingStatusName).ToList(), "BookingStatusId", "BookingStatusName");
            ViewBag.BookingRemarks = new SelectList(bookingRemarkService.FilterActive().OrderBy(x => x.BookingRemarkName).ToList(), "BookingRemarkId", "BookingRemarkName");
            ViewBag.DeliveryStatus = new SelectList(deliveryStatusService.FilterActive().OrderBy(x => x.DeliveryStatusName).ToList(), "DeliveryStatusId", "DeliveryStatusName");
            ViewBag.DeliveryRemarks = new SelectList(deliveryRemarkService.FilterActive().OrderBy(x => x.DeliveryRemarkName).ToList(), "DeliveryremarkId", "DeliveryRemarkName");
            //ViewBag.GatewayTypes = new SelectList(gatewayTypeService.FilterActive().OrderBy(x => x.GatewayTypeName).ToList(), "GatewayTypeId", "GatewayTypeName");
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActive().OrderBy(x => x.GatewayName).ToList(), "GatewayId", "GatewayName");
            ViewBag.AccountStatus = new SelectList(accountStatusService.FilterActive().OrderBy(x => x.AccountStatusName).ToList(), "AccountStatusId", "AccountStatusName");
            ViewBag.AccountType = new SelectList(accountTypeService.FilterActive().OrderBy(x => x.AccountTypeName).ToList(), "AccountTypeId", "AccountTypeName");
            ViewBag.BusinessType = new SelectList(businessTypeService.FilterActive().OrderBy(x => x.BusinessTypeName).ToList(), "BusinessTypeId", "BusinessTypename");
            ViewBag.Industries = new SelectList(industryService.FilterActive().OrderBy(x => x.IndustryName).ToList(), "IndustryId", "IndustryName");
            ViewBag.OrganizationType = new SelectList(organizationTypeService.FilterActive().OrderBy(x => x.OrganizationTypeName).ToList(), "OrganizationTypeId", "OrganizationTypeName");
            ViewBag.BillingPeriods = new SelectList(billingPeriodService.FilterActive().OrderBy(x => x.BillingPeriodName).ToList(), "BillingPeriodId", "BillingPeriodName");


            return View();
        }
        
    }
}