﻿using System.Collections.Generic;
using Moq;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VirtualNumberService.Tests
{
    public static class MockVirtualNumberDataSource
    {
        public const string FreeJobApplicationNumber1 = "9791011355";

        public const string PaidJobApplicationNumber1 = "9742244076";

        public const string FreeJobApplicationNumber2 = "9860223424";

        public const string PaidJobApplicationNumber2 = "9860223430";

        public static IEnumerable<VirtualNumber> VirtualNumbers
        {
            get
            {
                return new List<VirtualNumber>
                {
                    new VirtualNumber(new PhoneNumber(FreeJobApplicationNumber1), new FreeJobApplication(), new Provider(1, "Exotel")),
                    new VirtualNumber(new PhoneNumber(FreeJobApplicationNumber2), new FreeJobApplication(), new Provider(1, "Exotel")),
                    new VirtualNumber(new PhoneNumber(PaidJobApplicationNumber1), new PaidJobApplication(), new Provider(1, "Exotel")),
                    new VirtualNumber(new PhoneNumber(PaidJobApplicationNumber2), new PaidJobApplication(), new Provider(1, "Exotel"))
                };
            }
        } 

        public static Mock<IRepository<T, VirtualNumberContext>> GetRepository<T, TU>() where T : class where TU : VirtualNumberContext
        {
            return new Mock<IRepository<T, VirtualNumberContext>>();
        }

        public static Provider GetProvider
        {
            get { return new Provider(1, "Exotel"); }
        }

        public static List<Mock<VirtualNumberAssociation>> GetFreeJobAssociations
        {
            get
            {
                var mockAssociationOne = new Mock<VirtualNumberAssociation>();
                mockAssociationOne.Setup(a => a.VirtualNumber)
                    .Returns(new VirtualNumber(
                        new PhoneNumber(FreeJobApplicationNumber1),
                        new FreeJobApplication(),
                        GetProvider));

                mockAssociationOne.Setup(a => a.Caller).Returns(new User(new BabajobUserType(), 1));

                var mockAssociationTwo = new Mock<VirtualNumberAssociation>();
                mockAssociationTwo.Setup(a => a.VirtualNumber)
                    .Returns(new VirtualNumber(
                        new PhoneNumber(FreeJobApplicationNumber2),
                        new FreeJobApplication(),
                        GetProvider));

                mockAssociationTwo.Setup(a => a.Caller).Returns(new User(new BabajobUserType(), 1));

                return new List<Mock<VirtualNumberAssociation>>
                {
                    mockAssociationOne,
                    mockAssociationTwo
                };
            }
        }
    }
}
