using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Telephony.VritualNumberService;
using Telephony.VritualNumberService.DataAccess;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VirtualNumberService.Tests
{
    [TestFixture]
    public class VirtualNumberServiceTests
    {
        private const string ValidNumber = "9791011355";

        [Test]
        [Description("When a virtual number is available for a purpose, generate returns that number")]
        public void GenerateGivesNumberIfOneIsAvailable()
        {
            var virtualNumberRepo = new Mock<IRepository<VirtualNumber>>();
            var virtualNumberAssociationRepo = new Mock<IRepository<VirtualNumberAssociation>>();

            virtualNumberRepo.Setup(repo => repo.Get(
                It.IsAny<Func<VirtualNumber, bool>>()))
                .Returns(new List<VirtualNumber>
                {
                    new VirtualNumber(new PhoneNumber(ValidNumber), new FreeJobApplication(), new Provider(1, "Exotel"))
                });

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get(It.IsAny<Func<VirtualNumberAssociation, bool>>()))
                .Returns(Enumerable.Empty<VirtualNumberAssociation>());

            var virtualNumberService = new VritualNumberService.ApplicationServices.VirtualNumberService(
                virtualNumberRepo.Object,
                virtualNumberAssociationRepo.Object,
                new Mock<IRepository<Purpose>>().Object, 
                new Mock<IRepository<State>>().Object);

            var number = virtualNumberService.Generate(new Mock<IVirtualNumberRequest>().Object);

            Assert.AreEqual(number.VirtualNumber.VirtualPhoneNumber.Number, ValidNumber);
        }
    }
}
