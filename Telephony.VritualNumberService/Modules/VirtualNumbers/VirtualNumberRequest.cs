using Telephony.VritualNumberService.Entities;

namespace Telephony.VritualNumberService.Modules.VirtualNumbers
{
    public class VirtualNumberRequest : IVirtualNumberRequest
    {
        public VirtualNumberRequest(
            User caller, 
            User callee, 
            int purposeId,
            int babajobJobJobId)
        {
            Caller = caller;
            Callee = callee;
            PurposeId = purposeId;
            BabajobJobId = babajobJobJobId;
        }

        public User Caller { get; protected set; }

        public User Callee { get; protected set; }

        public int PurposeId { get; protected set; }

        public int BabajobJobId { get; protected set; }
    }
}