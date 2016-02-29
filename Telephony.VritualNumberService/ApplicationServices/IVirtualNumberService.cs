using System;
using System.Collections.Generic;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public interface IVirtualNumberService
    {
        IEnumerable<VirtualNumber> Get(Func<VirtualNumber, bool> predicate = null);

        void Add(VirtualNumber virtualNumber);

        VirtualNumberAssociation Generate(IVirtualNumberRequest virtualNumberRequest);

        IEnumerable<Purpose> GetPurposes(Func<Purpose, bool> predicate = null);

        IEnumerable<State> GetStates(Func<State, bool> predicate = null);

    }
}
