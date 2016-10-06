using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Models;
using CMS2.CentralWeb.Models.CMS1;
using CMS2.CentralWeb.ViewModels;
using CMS2.Entities.Models;

namespace CMS2.CentralWeb.Controllers
{
    public class ShipmentController : ApiController
    {
        [HttpGet]
        [ActionName("GetInvoices")]
        public HttpResponseMessage GetInvoices(string start = "", string end = "")
        {
            // sample access from browser
            //http://localhost/apcargoweb/api/shipment/getinvoices?start=3/26/2015&end=3/29/2015

            DateTime startDate;
            DateTime endDate;
            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                List<Invoice> invoices = CargoToInvoice(GetShipment(null, null));
                return Request.CreateResponse(HttpStatusCode.OK, invoices);
            }
            else if (DateTime.TryParse(start, out startDate) && DateTime.TryParse(end, out endDate))
            {
                List<Invoice> invoices = CargoToInvoice(GetShipment(startDate, endDate));
                return Request.CreateResponse(HttpStatusCode.OK, invoices);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        [HttpGet]
        [ActionName("GetInvoicesSummary")]
        public HttpResponseMessage GetInvoicesSummary(string start = "", string end = "")
        {
            DateTime startDate;
            DateTime endDate;
            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                InvoiceSummary invoiceSummary = new InvoiceSummary();
                invoiceSummary.InvoiceCount = GetShipment(null, null).Count().ToString();
                return Request.CreateResponse(HttpStatusCode.OK, invoiceSummary);
            }
            else if (DateTime.TryParse(start, out startDate) && DateTime.TryParse(end, out endDate))
            {
                InvoiceSummary invoiceSummary = new InvoiceSummary();
                invoiceSummary.InvoiceCount = GetShipment(startDate, endDate).Count().ToString();
                return Request.CreateResponse(HttpStatusCode.OK, invoiceSummary);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        private List<cargo> GetShipment(DateTime? startDate, DateTime? endDate)
        {
            ShipmentContext context = new ShipmentContext();
            if (startDate == null && endDate == null)
            {
                DateTime shipmentDate = DateTime.Now.AddDays(-1).Date;
                startDate = new DateTime(shipmentDate.Year, shipmentDate.Month, shipmentDate.Day, 0, 0, 0);
                endDate = new DateTime(shipmentDate.Year, shipmentDate.Month, shipmentDate.Day, 23, 59, 59);
            }
            else
            {
                startDate = new DateTime(startDate.GetValueOrDefault().Year, startDate.GetValueOrDefault().Month, startDate.GetValueOrDefault().Day, 0, 0, 0);
                endDate = new DateTime(endDate.GetValueOrDefault().Year, endDate.GetValueOrDefault().Month, endDate.GetValueOrDefault().Day, 23, 59, 59);
            }
            List<cargo> models = context.cargoes.Where(x =>
                         x.processingdate >= startDate && x.processingdate <= endDate)
                     .ToList();
            return models;
        }

        private List<Invoice> CargoToInvoice(List<cargo> cargos)
        {
            ShipmentContext context = new ShipmentContext();
            List<Invoice> invoices = new List<Invoice>();

            var models = cargos.Join(context.destinations, c => c.origincode, d => d.code, (c, d) => new { c, d });
            foreach (var item in models)
            {
                Invoice model = new Invoice();
                model.AirwayBillNo = item.c.airwaybill.ToString();
                model.OriginArea = item.c.origincode;
                model.DestinationArea = item.c.destcode;
                model.Consignee = item.c.consigneename;
                if (string.IsNullOrEmpty(model.Consignee))
                    model.Consignee = item.c.consigneecompany;
                model.Shipper = item.c.shippername;
                if (string.IsNullOrEmpty(model.Shipper))
                    model.Shipper = item.c.shippercompany;
                model.AcceptedBy = "NA";
                model.AcceptedArea = "NA";
                model.DateAccepted = item.c.processingdate.ToString();
                model.Quantity = "NA";
                model.ActualGrossWeight = item.c.actualweight.ToString();
                model.ChargeableWeight = item.c.chargeableweight.ToString();
                model.WeightCharge = item.c.weightcharge.ToString();
                model.Discount = item.c.discount.ToString();
                model.ServiceMode = item.c.servicecode;
                model.PaymentMode = item.c.paymentcode;
                model.PaymentTerm = item.c.paymentterms;
                model.HasPerishableGoods = "NA";
                model.HasAwbfee = item.c.noawb.ToString();
                model.HasFreightCollectCharge = item.c.nofcc.ToString();
                model.IsNonVatable = item.c.novat.ToString();
                model.IsNonVatValuation = "NA";
                model.HasNonVatInsurance = "NA";
                model.HasNonVatWeightCharge = item.c.nowc.ToString();
                model.DeclaredValue = item.c.declaredvalue.ToString();
                model.OtherChargesDesc = item.c.otherchargesdesc;
                model.OtherChargesAmount = item.c.othersamount.ToString();
                model.AwbFee = item.c.awbfee.ToString();
                model.FreigtCollectCharge = item.c.freightcollect.ToString();
                model.FuelSurcharge = item.c.fuelsurcharge.ToString();
                model.PeracFee = item.c.peracfee.ToString();
                model.Insurance = item.c.insurance.ToString();
                model.CratingFee = item.c.cratingfee.ToString();
                model.DangerousFee = item.c.dangerousfee.ToString();
                model.DeliveryFee = item.c.deliveryfee.ToString();
                model.ValuationAmount = item.c.valuation.ToString();
                model.SubTotal = item.c.subtotal.ToString();
                model.VatAmount = item.c.evat.ToString();
                model.TotalAmount = item.c.grandtotal.ToString();
                model.Branch = item.d.desc;
                invoices.Add(model);
            }
            return invoices;
        }

        public HttpResponseMessage ComputeCharges([FromBody]ShipmentModel model)
        {
            if (model != null)
            {
                ShipmentBL shipmentService = new ShipmentBL();
                model = shipmentService.ComputeCharges(model);
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }
    }
}
