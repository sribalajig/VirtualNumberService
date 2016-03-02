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

        public VirtualNumberModule(IVirtualNumberService virtualNumberService)
        {
            _virtualNumberService = virtualNumberService;

            Get["/VirtualNumbers"] = _ =>
                Response.AsJson(_virtualNumberService.Get());

            Post["/VirtualNumbers/Generate"] = _ =>
            {
                var virtualNumberRequest = this.Bind<VirtualNumberRequest>();

                return Response.AsJson(
                    _virtualNumberService.Generate(virtualNumberRequest), 
                    HttpStatusCode.Created);
            };

            Get["/VirtualNumbers/States"] = _ => 
                Response.AsJson(_virtualNumberService.GetStates());

            Get["/VirtualNumbers/Purposes"] = _ =>
                Response.AsJson(_virtualNumberService.GetPurposes());

            Post["/VirtualNumbers/"] = _ =>
            {
                var virtualNumber = this.Bind<VirtualNumber>();

                _virtualNumberService.Add(new VirtualNumber(
                    new PhoneNumber("9742244076"), 
                    new FreeJobApplication(), 
                    new Provider(1, "Exotel")));

                return Response.AsJson(HttpStatusCode.Created);
            };
        }
    }
}