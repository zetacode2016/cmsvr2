﻿using System.Security.Principal;
using CMS2.Common.Identity;
using CMS2.Entities;

namespace CMS2.Client
{
    public static class AppUser
    {
        public static IPrincipal Principal { get; set; }
        public static IdentityUser User { get; set; }
        public static Employee Employee { get; set; }
        public static EmployeePositionMapping EmployeeAssignment { get; set; }
    }
}
