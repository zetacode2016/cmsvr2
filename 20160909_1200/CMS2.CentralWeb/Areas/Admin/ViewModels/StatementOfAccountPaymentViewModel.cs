using CMS2.Entities.Models;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class StatementOfAccountPaymentViewModel
    {
        public StatementOfAccountModel StatementOfAccountModel { get; set; }
        public PaymentViewModel PaymentViewModel { get; set; }
    }
}