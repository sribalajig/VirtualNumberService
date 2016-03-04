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
            int babajobJobJobId)
        {
            Caller = caller;
            Callee = callee;
            Purpose = purpose;
            BabajobJobId = babajobJobJobId;
        }

        public User Caller { get; protected set; }

        public User Callee { get; protected set; }

        public Purpose Purpose { get; protected set; }

        public int BabajobJobId { get; protected set; }
    }
}