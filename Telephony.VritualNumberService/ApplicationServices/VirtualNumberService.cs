using System.Collections.Generic;
using System.Data.Entity;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.ApplicationServices
{
    public class VirtualNumberService : IVirtualNumberService
    {
        private readonly IRepository<VirtualNumber, VirtualNumberContext> _virtualNumberRepository;
        
        private readonly IRepository<Purpose, VirtualNumberContext> _purposeRespository;

        private readonly IRepository<State, VirtualNumberContext> _virtualNumberStateRepostiory;

        public VirtualNumberService(
            IRepository<VirtualNumber, VirtualNumberContext> virtualNumberRepository,
            IRepository<Purpose, VirtualNumberContext> purposeRespository,
            IRepository<State, VirtualNumberContext> virtualNumberStateRepostiory)
        {
            _virtualNumberRepository = virtualNumberRepository;
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

        public void Save(VirtualNumber virtualNumber)
        {
            _virtualNumberRepository.Save(virtualNumber);
        }

        public IEnumerable<Purpose> GetPurposes()
        {
            return _purposeRespository.Get();
        }

        public IEnumerable<State> GetStates()
        {
            return _virtualNumberStateRepostiory.Get();
        }
    }
}