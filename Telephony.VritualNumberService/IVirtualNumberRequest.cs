using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;

namespace Telephony.VritualNumberService
{
    public interface IVirtualNumberRequest
    {
        User Caller { get; }

        User Callee { get; }

        Purpose Purpose { get; }

        int BabajobId { get; }
    }
}
