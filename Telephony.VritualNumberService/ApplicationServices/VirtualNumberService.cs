using System;
using System.Collections.Generic;
using System.Linq;
using Telephony.VritualNumberService.DataAccess;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.Utilities;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public class VirtualNumberService : IVirtualNumberService
    {
        private readonly IRepository<VirtualNumber> _virtualNumberRepository;

        private readonly IRepository<VirtualNumberAssociation> _virtualNumberAssociationRepository;

        private readonly IRepository<Purpose> _purposeRespository;

        private readonly IRepository<State> _virtualNumberStateRepostiory;

        public VirtualNumberService(
            IRepository<VirtualNumber> virtualNumberRepository,
            IRepository<VirtualNumberAssociation> virtualNumberAssociationRepository,
            IRepository<Purpose> purposeRespository,
            IRepository<State> virtualNumberStateRepostiory)
        {
            _virtualNumberRepository = virtualNumberRepository;
            _virtualNumberAssociationRepository = virtualNumberAssociationRepository;
            _purposeRespository = purposeRespository;
            _virtualNumberStateRepostiory = virtualNumberStateRepostiory;
        }

        public IEnumerable<VirtualNumber> Get(Func<VirtualNumber, bool> predicate)
        {
            return _virtualNumberRepository.Get(predicate);
        }
   
        public void Add(VirtualNumber virtualNumber)
        {
            _virtualNumberRepository.Add(virtualNumber);
        }

        public VirtualNumberAssociation Generate(IVirtualNumberRequest virtualNumberRequest)
        {
            var availableNumbers = _virtualNumberRepository.Get(
                number => number.Purpose.Name.Equals(virtualNumberRequest.Purpose.Name));

            var virtualNumbersUsedBySeeker = _virtualNumberAssociationRepository.Get(
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

        public IEnumerable<Purpose> GetPurposes(Func<Purpose, bool> predicate = null)
        {
            return _purposeRespository.Get();
        }

        public IEnumerable<State> GetStates(Func<State, bool> predicate = null)
        {
            return _virtualNumberStateRepostiory.Get();
        }
    }
}