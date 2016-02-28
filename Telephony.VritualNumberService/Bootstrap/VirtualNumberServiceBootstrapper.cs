using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Telephony.VritualNumberService.ApplicationServices;
using Telephony.VritualNumberService.DataAccess;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.ModelBinder;
using Telephony.VritualNumberService.Persistence;

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
            container.Register<IRepository<VirtualNumber>, Repository<VirtualNumber>>();
            container.Register<IRepository<VirtualNumberAssociation>, Repository<VirtualNumberAssociation>>();
            container.Register<IRepository<Purpose>, Repository<Purpose>>();
            container.Register<IRepository<State>, Repository<State>>();
        }

        protected override void ApplicationStartup(
            TinyIoCContainer container, 
            IPipelines pipelines)
        {
            using (var context = new VirtualNumberContext())
            {
                context.States.Add(new Free());
                context.States.Add(new InUse());
                context.States.Add(new Expired());

                context.SaveChanges();
            }
        }
    }
}