using Castle.MicroKernel.Registration;
using ClosestAddress.Cache;
using ClosestAddress.WebApi.Interfaces;
using ClosestAddress.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosestAddress.WebApi.IoC
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container,
        Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<ICustomCache>()
                    .ImplementedBy<CustomCache>()
                    .LifestyleTransient());
            container.Register(
               Component
                   .For<IAddresses>()
                   .ImplementedBy<Addresses>()
                   .LifestyleTransient());
        }
    }

}