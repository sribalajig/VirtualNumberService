using Newtonsoft.Json.Linq;
using Telephony.VritualNumberService.Modules.VirtualNumbers;

namespace Telephony.VritualNumberService.Utilities
{
    public interface IVirtualNumberRequestParser
    {
        VirtualNumberRequest ParseRequest(JObject jsonObject);
    }
}
