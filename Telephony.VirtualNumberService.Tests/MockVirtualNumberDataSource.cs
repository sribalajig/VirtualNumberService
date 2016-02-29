using System.Collections.Generic;
using Moq;
using Telephony.VritualNumberService.DataAccess;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VirtualNumberService.Tests
{
    public static class MockVirtualNumberDataSource
    {
        public const string ValidNumber = "9791011355";

        public static IEnumerable<VirtualNumber> VirtualNumbersForFreeJobApplications
        {
            get
            {
                return new List<VirtualNumber>
                {
                    new VirtualNumber(new PhoneNumber(ValidNumber), new FreeJobApplication(), new Provider(1, "Exotel"))
                };
            }
        } 

        public static Mock<IRepository<T>> GetRepository<T>() where T : class 
        {
            return new Mock<IRepository<T>>();
        }

        public static Provider GetProvider
        {
            get { return new Provider(1, "Exotel"); }
        }
    }
}
