using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Identity;
using CMS2.DataAccess;
using CMS2.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace CMS2.CentralWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ICmsUoW, CmsUoW>(new HierarchicalLifetimeManager(), new InjectionConstructor("Cms")); // specify the ConnectionString name
            container.RegisterType<IUserStore<IdentityUser, Guid>, UserStore>(new TransientLifetimeManager());
            container.RegisterType<RoleStore>(new TransientLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}