using System;
using System.Collections.Generic;
using Nancy;
using Nancy.TinyIoc;
using Telephony.VritualNumberService.ApplicationServices;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.ModelBinder;

namespace Telephony.VritualNumberService.Bootstrap
{
    public class VirtualNumberServiceBootstrapper : DefaultNancyBootstrapper
    {
        protected override IEnumerable<Type> ModelBinders
        {
            get
            {
                return new[] { typeof(GenerateVirtualNumberRequestModelBinder) };
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IVirtualNumberService, VirtualNumberService>();
            container.Register<
                IRepository<VirtualNumber, VirtualNumberContext>, 
                Repository<VirtualNumber, VirtualNumberContext>>();
            container.Register<
                IRepository<VirtualNumberAssociation, VirtualNumberContext>, 
                Repository<VirtualNumberAssociation, VirtualNumberContext>>();
            container.Register<
                IRepository<Purpose, VirtualNumberContext>, 
                Repository<Purpose, VirtualNumberContext>>();
            container.Register<
                IRepository<State, VirtualNumberContext>, 
                Repository<State, VirtualNumberContext>>();
        }
    }
}