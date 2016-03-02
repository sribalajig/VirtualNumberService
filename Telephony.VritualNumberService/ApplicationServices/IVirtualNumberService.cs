using System.Collections.Generic;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public interface IVirtualNumberService
    {
        IEnumerable<VirtualNumber> Get();

        void Save(VirtualNumber virtualNumber);

        IEnumerable<Purpose> GetPurposes();

        IEnumerable<State> GetStates();
    }
}
