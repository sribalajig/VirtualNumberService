using System.Dynamic;
using System.Linq;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Modules.VirtualNumbers;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public interface IVirtualNumberAssociationService
    {
        void Save(VirtualNumberAssociation virtualNumberAssociation);

        IQueryable<VirtualNumberAssociation> Get();

        VirtualNumberAssociation Generate(IVirtualNumberRequest virtualNumberRequest);

        VirtualNumberAssociation Get(IVirtualNumberRequest virtualNumberRequest);
    }
}