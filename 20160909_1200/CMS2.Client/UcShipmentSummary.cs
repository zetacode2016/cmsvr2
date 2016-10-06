using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities.Models;

namespace CMS2.Client
{
    public partial class UcShipmentSummary : UserControl
    {
        private ShipmentBL shipmentService;
        public UcShipmentSummary()
        {
            InitializeComponent();
        
        }

        private void UcShipmentSummary_Load(object sender, EventArgs e)
        {
            shipmentService = new ShipmentBL(GlobalVars.UnitOfWork);
        }

        private void PopulateGrid()
        {
            DateTime currentDate = DateTime.Now;
            var entities = shipmentService.FilterActiveBy(x => x.DateAccepted.Year == currentDate.Year && x.DateAccepted.Month==currentDate.Month && x.DateAccepted.Day == currentDate.Day);
            var shipments = shipmentService.EntitiesToModels(entities);

            gridShipmentSummary.Rows.Clear();
            int index = 0;
            foreach (var item in shipments)
            {
                gridShipmentSummary.Rows.Add();
                gridShipmentSummary.Rows[index].Cells["colAwbNo"].Value = item.AirwayBillNo;
                gridShipmentSummary.Rows[index].Cells["colDateAccepted"].Value = item.DateAcceptedString;
                gridShipmentSummary.Rows[index].Cells["colAccountNo"].Value = item.Shipper.AccountNo;
                gridShipmentSummary.Rows[index].Cells["colShipper"].Value = item.Shipper.FullName;
                gridShipmentSummary.Rows[index].Cells["colOriginCity"].Value = item.OriginCity.CityName;
                gridShipmentSummary.Rows[index].Cells["colConsignee"].Value = item.Consignee.FullName;
                gridShipmentSummary.Rows[index].Cells["colDestinationCity"].Value = item.DestinationCity.CityName;
                
                gridShipmentSummary.Rows[index].Cells["colQuantity"].Value = item.Quantity.ToString();
                gridShipmentSummary.Rows[index].Cells["colChargeableWeight"].Value = item.ChargeableWeightString;
                gridShipmentSummary.Rows[index].Cells["colSubTotal"].Value = item.ShipmentSubTotalString;
                gridShipmentSummary.Rows[index].Cells["colVatAmount"].Value = item.ShipmentVatAmountString;
                gridShipmentSummary.Rows[index].Cells["colTotal"].Value = item.ShipmentTotalString;
                gridShipmentSummary.Rows[index].Cells["colServiceMode"].Value = item.ServiceMode.ServiceModeName;
                gridShipmentSummary.Rows[index].Cells["colPaymentMode"].Value = item.PaymentMode.PaymentModeCode;
                gridShipmentSummary.Rows[index].Cells["colAcceptedBy"].Value = item.AcceptedBy.FullName;
                index++;
            }
        }

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}
