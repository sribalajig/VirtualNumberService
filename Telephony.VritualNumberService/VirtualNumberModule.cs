using Nancy;
using Nancy.ModelBinding;
using Telephony.VritualNumberService.ApplicationServices;

namespace Telephony.VritualNumberService
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
        }
    }
}