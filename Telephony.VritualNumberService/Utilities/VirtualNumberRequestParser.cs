using System;
using Newtonsoft.Json.Linq;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Modules.VirtualNumbers;

namespace Telephony.VritualNumberService.Utilities
{
    public class VirtualNumberRequestParser : IVirtualNumberRequestParser
    {
        public VirtualNumberRequest ParseRequest(JObject jsonObject)
        {
            JToken caller;
            JToken callee;
            JToken purposeId;
            JToken babajobJobId;

            if (!jsonObject.TryGetValue("caller", out caller) || 
                !jsonObject.TryGetValue("callee", out callee) ||
                !jsonObject.TryGetValue("purposeId", out purposeId) ||
                !jsonObject.TryGetValue("babajobJobId", out babajobJobId))
            {
                throw new ArgumentException("Ivalid json in the body.");
            }

            return new VirtualNumberRequest(
                caller.ToObject<User>(),
                callee.ToObject<User>(),
                purposeId.ToObject<int>(), 
                babajobJobId.ToObject<int>());
        }
    }
}