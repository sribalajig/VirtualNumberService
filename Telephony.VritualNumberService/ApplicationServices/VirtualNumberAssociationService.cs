using System;
using System.Data.Entity;
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

        private readonly IRepository<User, VirtualNumberContext> _userRepository;

        public VirtualNumberAssociationService(
            IRepository<VirtualNumberAssociation, VirtualNumberContext> virtualNumberAssociationRepository,
            IRepository<VirtualNumber, VirtualNumberContext> virtualNumberRepository,
            IRepository<User, VirtualNumberContext> userRepository)
        {
            _virtualNumberAssociationRepository = virtualNumberAssociationRepository;
            _virtualNumberRepository = virtualNumberRepository;
            _userRepository = userRepository;
        }

        public void Save(VirtualNumberAssociation virtualNumberAssociation)
        {
            var caller = _userRepository.Get()
                .FirstOrDefault(
                user => user.Id 
                    == virtualNumberAssociation.Caller.Id);

            if (caller == null)
            {
                _userRepository.Save(virtualNumberAssociation.Caller);
            }

            var callee = _userRepository.Get()
                .FirstOrDefault(user => user.Id
                    == virtualNumberAssociation.Callee.Id);

            if (callee == null)
            {
                _userRepository.Save(virtualNumberAssociation.Callee);
            }

            var newAssociation = new VirtualNumberAssociation
            {
                CallerId = virtualNumberAssociation.Caller.Id,
                CalleeId = virtualNumberAssociation.Callee.Id,
                StateId = virtualNumberAssociation.State.Id,
                VirtualNumberId = virtualNumberAssociation.VirtualNumber.Id,
                BabajobJobId = virtualNumberAssociation.BabajobJobId
            };

            _virtualNumberAssociationRepository.Save(newAssociation);
        }

        public IQueryable<VirtualNumberAssociation> Get()
        {
            return _virtualNumberAssociationRepository.Get();
        }

        public VirtualNumberAssociation Generate(IVirtualNumberRequest virtualNumberRequest)
        {
            var availableNumbers = _virtualNumberRepository.Get()
                .Where(number => number.Purpose.Name.Equals(virtualNumberRequest.Purpose.Name))
                .Include(number => number.VirtualPhoneNumber);

            var virtualNumbersUsedBySeeker = _virtualNumberAssociationRepository.Get()
                .Where(
                association => association.Caller.Id == virtualNumberRequest.Caller.Id
                && association.VirtualNumber.Purpose.Name == virtualNumberRequest.Purpose.Name);

            var numbersUsedBySeeker = virtualNumbersUsedBySeeker.Select(
                number => number.VirtualNumber).Include(number => number.VirtualPhoneNumber).ToList();

            var availableNumber = availableNumbers.ToList()
                .Except((numbersUsedBySeeker), new VirtualNumberComparer()).FirstOrDefault();

            if (availableNumber == null)
                throw new ApplicationException("No more numbers available!");

            return new VirtualNumberAssociation
            {
                Caller = virtualNumberRequest.Caller,
                Callee = virtualNumberRequest.Callee,
                State = new InUse(),
                VirtualNumber = availableNumber,
                BabajobJobId = virtualNumberRequest.BabajobJobId
            };
        }
    }
}