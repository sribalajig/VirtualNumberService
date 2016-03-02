using System;
using System.Linq;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.Modules.VirtualNumbers;
using Telephony.VritualNumberService.Utilities;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public class VirtualNumberAssociationService : IVirtualNumberAssociationService
    {
        private readonly IRepository<VirtualNumberAssociation, VirtualNumberContext> _virtualNumberAssociationRepository;

        private readonly IRepository<VirtualNumber, VirtualNumberContext> _virtualNumberRepository; 

        public VirtualNumberAssociationService(
            IRepository<VirtualNumberAssociation, VirtualNumberContext> virtualNumberAssociationRepository,
            IRepository<VirtualNumber, VirtualNumberContext> virtualNumberRepository)
        {
            _virtualNumberAssociationRepository = virtualNumberAssociationRepository;
            _virtualNumberRepository = virtualNumberRepository;
        }

        public void Save(VirtualNumberAssociation virtualNumberAssociation)
        {
            _virtualNumberAssociationRepository.Add(virtualNumberAssociation);
        }

        public IQueryable<VirtualNumberAssociation> Get()
        {
            return _virtualNumberAssociationRepository.Get();
        }

        public VirtualNumberAssociation Generate(IVirtualNumberRequest virtualNumberRequest)
        {
            var availableNumbers = _virtualNumberRepository.Get()
                .Where(number => number.Purpose.Name.Equals(virtualNumberRequest.Purpose.Name));

            var virtualNumbersUsedBySeeker = _virtualNumberAssociationRepository.Get()
                .Where(
                association => association.Caller.BabajobUserId == virtualNumberRequest.Caller.BabajobUserId
                && association.VirtualNumber.Purpose.Name == virtualNumberRequest.Purpose.Name);

            var availableNumber = availableNumbers.Except(virtualNumbersUsedBySeeker.Select(
                number => number.VirtualNumber), new VirtualNumberComparer()).FirstOrDefault();

            if (availableNumber == null)
                throw new ApplicationException("No more numbers available");

            return new VirtualNumberAssociation
            {
                Caller = virtualNumberRequest.Caller,
                Callee = virtualNumberRequest.Callee,
                State = new InUse(),
                VirtualNumber = availableNumber
            };
        }
    }
}