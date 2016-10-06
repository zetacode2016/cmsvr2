using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Shipment.ViewModels;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    public class DeliveryController : ShipmentBaseController
    {
        public DeliveryBL service = new DeliveryBL();
        public ActionResult Index()
        {
            var deliveries = service.FilterActive().OrderByDescending(x => x.DateDelivered);
            List<DeliveryViewModel> vm = new List<DeliveryViewModel>();
            if (deliveries != null)
            {
                string awbno = "";
                foreach (var item in deliveries)
                {
                    DeliveryViewModel model = new DeliveryViewModel();
                    awbno = "";
                    if (item.ShipmentId != null)
                    {
                        awbno = item.Shipment.AirwayBillNo;
                    }

                    model.DeliveryId = item.DeliveryId;
                    model.AirwayBillNo = awbno;
                    model.DateDelivered = Convert.ToDateTime(item.DateDelivered).ToString("MMM dd, yyyy hh:mm tt");
                    model.DeliveredBy = item.DeliveredBy.FullName;
                    model.DeliveryStatus = item.DeliveryStatus.DeliveryStatusName;
                    vm.Add(model);
                }
            }
            return View(vm);
        }


        public ActionResult Details(Guid id)
        {
            DeliveryDetailsViewModel model = new DeliveryDetailsViewModel();
            model.PackageNos = new List<string>();
            var delivery = service.GetById(id);
            if (delivery != null)
            {
                string awbno = "";
                if (delivery.ShipmentId != null)
                {
                    awbno = delivery.Shipment.AirwayBillNo;
                }
                model.AirwayBillNo = awbno;
                model.DateDelivered = Convert.ToDateTime(delivery.DateDelivered).ToString("MMM dd, yyyy hh:mm tt"); ;
                model.DeliveredBy = delivery.DeliveredBy.FullName;
                model.DeliveryStatus = delivery.DeliveryStatus.DeliveryStatusName;
                if (delivery.DeliveryRemarkId != null)
                {
                    model.DeliveryRemark = delivery.DeliveryRemark.DeliveryRemarkName;
                }
                else
                {
                    model.DeliveryRemark = "";
                }
                model.Note = delivery.Note;
                if (delivery.DeliveryReceipts != null && delivery.DeliveryReceipts.Count>0)
                {
                    model.ReceivedBy = delivery.DeliveryReceipts[0].ReceivedBy;
                    model.Signature = delivery.DeliveryReceipts[0].Signature;
                }
                if (delivery.DeliveredPackages != null && delivery.DeliveredPackages.Count > 0)
                {
                    foreach (var item in delivery.DeliveredPackages)
                    {
                        if (item.PackageNumber != null)
                        {
                            model.PackageNos.Add(item.PackageNumber.PackageNo);
                        }
                    }
                }
            }
            
            return View(model);
        }
    }
}
