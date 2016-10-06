using System.Collections.Generic;
using APCargo.Entities;

namespace APCargo.Web.Areas.Admin.ViewModels
{
    public class SoaViewModel
    {

        public StatementOfAccount StatementOfAccount { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
        //public List<StatementOfAccount> UnpaidSoas { get; set; }
        public List<Shipment> UnpaidShipments { get; set; } 
        public PaymentViewModel PaymentViewModel { get; set; }
    }
}