using System.Collections.Generic;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.Utilities
{
    public class VirtualNumberComparer : IEqualityComparer<VirtualNumber>
    {
        public bool Equals(VirtualNumber x, VirtualNumber y)
        {
            return x.VirtualPhoneNumber.Number == y.VirtualPhoneNumber.Number;
        }

        public int GetHashCode(VirtualNumber obj)
        {
            return obj.VirtualPhoneNumber.Number.GetHashCode();
        }
    }
}