using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;

namespace Telephony.VritualNumberService.Modules.VirtualNumbers
{
    public class VirtualNumberRequest : IVirtualNumberRequest
    {
        public VirtualNumberRequest(
            User caller, 
            User callee, 
            Purpose purpose,
            int babajobJobId)
        {
            Caller = caller;
            Callee = callee;
            Purpose = purpose;
            BabajobId = babajobJobId;
        }

        public User Caller { get; protected set; }

        public User Callee { get; protected set; }

        public Purpose Purpose { get; protected set; }

        public int BabajobId { get; protected set; }
    }
}