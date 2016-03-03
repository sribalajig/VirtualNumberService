using System.Data.Entity;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Telephony.VritualNumberService.ApplicationServices;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.Modules.VirtualNumbers
{
    public class VirtualNumberModule : NancyModule
    {
        private readonly IVirtualNumberService _virtualNumberService;

        private readonly IVirtualNumberAssociationService _virtualNumberAssociationService;

        public VirtualNumberModule(
            IVirtualNumberService virtualNumberService,
            IVirtualNumberAssociationService virtualNumberAssociationService)
        {
            _virtualNumberService = virtualNumberService;
            _virtualNumberAssociationService = virtualNumberAssociationService;

            Get["/VirtualNumbers"] = _ =>
                Response.AsJson(_virtualNumberService.Get());

            Post["/VirtualNumbers/Generate"] = _ =>
            {
                var virtualNumberRequest = this.Bind<VirtualNumberRequest>();

                var existingAssociation = _virtualNumberAssociationService.Get()
                                        .Include(a => a.VirtualNumber.VirtualPhoneNumber)
                                        .FirstOrDefault(
                                                    association => association.Caller.Id 
                                                                == virtualNumberRequest.Caller.Id
                                                    && association.Callee.Id 
                                                                == virtualNumberRequest.Callee.Id
                                                    && association.BabajobJobId 
                                                                == virtualNumberRequest.BabajobId);

                if (existingAssociation != null)
                {
                    return Response.AsJson(existingAssociation.VirtualNumber.VirtualPhoneNumber);
                }

                var newAssociation = _virtualNumberAssociationService.Generate(virtualNumberRequest);

                _virtualNumberAssociationService.Save(newAssociation);

                return newAssociation.VirtualNumber;
            };

            Get["/VirtualNumbers/States"] = _ => 
                Response.AsJson(_virtualNumberService.GetStates());

            Get["/VirtualNumbers/Purposes"] = _ =>
                Response.AsJson(_virtualNumberService.GetPurposes());

            Post["/VirtualNumbers/"] = _ =>
            {
                var purpose = new FreeJobApplication();
                var provider = new Provider(2, "Ozonetel");
                var number = new PhoneNumber("9742244076");

                var virtualNumber = new VirtualNumber
                {
                    PurposeId = purpose.Id,
                    ProviderId = provider.Id,
                    VirtualPhoneNumber = number,
                };

                _virtualNumberService.Save(virtualNumber);

                return Response.AsJson(HttpStatusCode.Created);
            };
        }
    }
}