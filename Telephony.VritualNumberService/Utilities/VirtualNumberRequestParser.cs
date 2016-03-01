using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Modules.VirtualNumbers;

namespace Telephony.VritualNumberService.Utilities
{
    public class VirtualNumberRequestParser : IVirtualNumberRequestParser
    {
        public VirtualNumberRequest ParseRequest(JObject jsonObject)
        {
            JToken caller;
            JToken callee;
            JToken purpose;

            if (!jsonObject.TryGetValue("caller", out caller) || 
                !jsonObject.TryGetValue("callee", out callee) ||
                !jsonObject.TryGetValue("purpose", out purpose))
            {
                throw new ArgumentException("Ivalid json in the body.");
            }

            return new VirtualNumberRequest(
                caller.ToObject<User>(),
                callee.ToObject<User>(),
                new FreeJobApplication(), 
                new int());
        }
    }
}