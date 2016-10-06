using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMS2.CentralWeb.Models;
using CMS2.CentralWeb.Models.CMS1;
using CMS2.CentralWeb.ViewModels;

namespace CMS2.CentralWeb.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpGet]
        [ActionName("GetPayments")]
        public HttpResponseMessage GetPayments(int page = 0, string start = "", string end = "")
        {
            // sample access from browser
            //http://localhost/apcargoweb/api/payment/getpayments?start=4/26/2013&end=4/26/2013
            DateTime startDate;
            DateTime endDate;
            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                List<Payments> payments = PaymentToPayments(GetShipmentPayments(null, null, page));
                return Request.CreateResponse(HttpStatusCode.OK, payments);
            }
            else if (DateTime.TryParse(start, out startDate) && DateTime.TryParse(end, out endDate))
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 1);
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                List<Payments> payments = PaymentToPayments(GetShipmentPayments(startDate, endDate, page));
                return Request.CreateResponse(HttpStatusCode.OK, payments);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// Gets and returns record count
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentsSummary")]
        public HttpResponseMessage GetPaymentsSummary(string start = "", string end = "")
        {
            DateTime startDate;
            DateTime endDate;
            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                PaymentsSummary paymentSummary = new PaymentsSummary();
                paymentSummary.PaymentsCount = GetShipmentPayments(null, null, 0).Count.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, paymentSummary);
            }
            else if (DateTime.TryParse(start, out startDate) && DateTime.TryParse(end, out endDate))
            {
                PaymentsSummary paymentSummary = new PaymentsSummary();
                paymentSummary.PaymentsCount = GetShipmentPayments(startDate, endDate, 0).Count.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, paymentSummary);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        private List<payment> GetShipmentPayments(DateTime? startDate, DateTime? endDate, int page)
        {
            int pageRecords = 200;
            ShipmentContext context = new ShipmentContext();
            if (startDate == null && endDate == null)
            {
                DateTime paymentDate = DateTime.Now.AddDays(-1).Date;
                startDate = new DateTime(paymentDate.Year, paymentDate.Month, paymentDate.Day, 0, 0, 0);
                endDate = new DateTime(paymentDate.Year, paymentDate.Month, paymentDate.Day, 23, 59, 59);
            }
            else
            {
                startDate = new DateTime(startDate.GetValueOrDefault().Year, startDate.GetValueOrDefault().Month, startDate.GetValueOrDefault().Day, 0, 0, 0);
                endDate = new DateTime(endDate.GetValueOrDefault().Year, endDate.GetValueOrDefault().Month, endDate.GetValueOrDefault().Day, 23, 59, 59);
            }

            List<payment> models = new List<payment>();
            if (page == 0)
            {
                models = context.payments.Where(x => x.paymentdate >= startDate && x.paymentdate <= endDate).ToList();
            }
            else
            {
                models = context.payments
                    .Where(x => x.paymentdate >= startDate && x.paymentdate <= endDate)
                    .OrderBy(x => x.paymentdate)
                    .Skip(pageRecords*(page - 1))
                    .Take(pageRecords).ToList();
            }
            return models.OrderBy(x => x.paymentdate).ThenBy(x => x.orpr).ThenBy(x => x.clientname).ToList(); ;
        }

        private List<Payments> PaymentToPayments(List<payment> payments)
        {
            string client = "";
            DateTime paymentDate = new DateTime();
            List<Payments> _payments = new List<Payments>();
            foreach (var item in payments)
            {
                if (String.IsNullOrEmpty(item.orpr))
                {
                    if (client.Equals(item.clientname) && paymentDate==item.paymentdate)
                    {
                        var model = _payments.Last();
                        model.AwbPayments.Add(new AwbPayment()
                        {
                            Id = item.id.ToString(),
                            AirwayBillNo = item.airwaybill,
                            Amount = item.amount.ToString("N"),
                            TaxWithheld = "0.00"
                        });
                    }
                    else
                    {
                        Payments model = new Payments();
                        model.PaymentDate = item.paymentdate.ToString("MM/dd/yyyy");
                        model.ORNo = item.orpr;
                        model.PaymentType = item.iscash;
                        model.CheckBankName = "NA";
                        model.CheckNo = "NA";
                        model.CheckDate = "NA";
                        model.AwbPayments = new List<AwbPayment>()
                            {
                                new AwbPayment() {
                                    Id = item.id.ToString(),
                                    AirwayBillNo = item.airwaybill,
                                    Amount = item.amount.ToString("N"),
                                    TaxWithheld = "0.00"}
                            };

                        _payments.Add(model);
                        client = item.clientname;
                        paymentDate = item.paymentdate;
                    }
                }
                else
                {
                    if (_payments.Exists(x => x.ORNo.Equals(item.orpr)))
                    {
                        var model = _payments.FirstOrDefault(x => x.ORNo.Equals(item.orpr));
                        model.AwbPayments.Add(new AwbPayment()
                        {
                            Id = item.id.ToString(),
                            AirwayBillNo = item.airwaybill,
                            Amount = item.amount.ToString("N"),
                            TaxWithheld = "0.00"
                        });
                    }
                    else
                    {
                        Payments model = new Payments();
                        model.PaymentDate = item.paymentdate.ToString("MM/dd/yyyy");
                        model.ORNo = item.orpr;
                        model.PaymentType = item.iscash;
                        model.CheckBankName = "NA";
                        model.CheckNo = "NA";
                        model.CheckDate = "NA";
                        model.AwbPayments = new List<AwbPayment>()
                    {
                        new AwbPayment() {
                            Id = item.id.ToString(),
                            AirwayBillNo = item.airwaybill,
                            Amount = item.amount.ToString("N"),
                            TaxWithheld = "0.00"}
                    };

                        _payments.Add(model);
                    }
                }
            }
            return _payments;
        }
    }
}
