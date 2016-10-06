using System;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class UserRoleViewModel
    {
        [DisplayName("Username")]
        public Guid UserId { get; set; }
        [DisplayName("Role")]
        public Guid RoleId { get; set; }
    }
}