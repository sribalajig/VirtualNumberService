using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Telephony.VritualNumberService.ApplicationServices;
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

                var existingAssociation = _virtualNumberAssociationService.Get().FirstOrDefault(
                                        association => association.Caller.BabajobUserId 
                                                    == virtualNumberRequest.Caller.BabajobUserId
                                     && association.Callee.BabajobUserId 
                                                    == virtualNumberRequest.Callee.BabajobUserId
                                     && association.BabajobJobId 
                                                    == virtualNumberRequest.BabajobId);

                if (existingAssociation != null)
                {
                    return existingAssociation;
                }

                var newAssociation = _virtualNumberAssociationService.Generate(virtualNumberRequest);

                _virtualNumberAssociationService.Save(newAssociation);

                return newAssociation;
            };

            Get["/VirtualNumbers/States"] = _ => 
                Response.AsJson(_virtualNumberService.GetStates());

            Get["/VirtualNumbers/Purposes"] = _ =>
                Response.AsJson(_virtualNumberService.GetPurposes());

            Post["/VirtualNumbers/"] = _ =>
            {
                _virtualNumberService.Add(new VirtualNumber(
                    new PhoneNumber("9742244076"), 
                    new FreeJobApplication(), 
                    new Provider(1, "Exotel")));

                return Response.AsJson(HttpStatusCode.Created);
            };
        }
    }
}