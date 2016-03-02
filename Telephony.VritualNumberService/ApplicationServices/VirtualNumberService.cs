using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.Modules.VirtualNumbers;
using Telephony.VritualNumberService.Utilities;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public class VirtualNumberService : IVirtualNumberService
    {
        private readonly IRepository<VirtualNumber, VirtualNumberContext> _virtualNumberRepository;

        private readonly IRepository<VirtualNumberAssociation, VirtualNumberContext> _virtualNumberAssociationRepository;

        private readonly IRepository<Purpose, VirtualNumberContext> _purposeRespository;

        private readonly IRepository<State, VirtualNumberContext> _virtualNumberStateRepostiory;

        public VirtualNumberService(
            IRepository<VirtualNumber, VirtualNumberContext> virtualNumberRepository,
            IRepository<VirtualNumberAssociation, VirtualNumberContext> virtualNumberAssociationRepository,
            IRepository<Purpose, VirtualNumberContext> purposeRespository,
            IRepository<State, VirtualNumberContext> virtualNumberStateRepostiory)
        {
            _virtualNumberRepository = virtualNumberRepository;
            _virtualNumberAssociationRepository = virtualNumberAssociationRepository;
            _purposeRespository = purposeRespository;
            _virtualNumberStateRepostiory = virtualNumberStateRepostiory;
        }

        public IEnumerable<VirtualNumber> Get()
        {
            var numbers = _virtualNumberRepository.Get()
                .Include(number => number.Purpose)
                .Include(number => number.Provider)
                .Include(number => number.VirtualPhoneNumber);

            return numbers;
        }
   
        public void Add(VirtualNumber virtualNumber)
        {
            _virtualNumberRepository.Add(virtualNumber);
        }

        public VirtualNumberAssociation Generate(IVirtualNumberRequest virtualNumberRequest)
        {
            var availableNumbers = _virtualNumberRepository.Get()
                .Where(number => number.Purpose.Name.Equals(virtualNumberRequest.Purpose.Name));

            var virtualNumbersUsedBySeeker = _virtualNumberAssociationRepository.Get()
                .Where(association => association.Caller.BabajobUserId == virtualNumberRequest.Caller.BabajobUserId
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