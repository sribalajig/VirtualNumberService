using Newtonsoft.Json.Linq;

namespace Telephony.VritualNumberService.Utilities
{
    public interface IVirtualNumberRequestParser
    {
        VirtualNumberRequest ParseRequest(JObject jsonObject);
    }
}
