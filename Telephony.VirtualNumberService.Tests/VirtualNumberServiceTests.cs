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
        [Test]
        [Description("When a virtual number is available for a purpose, generate returns that number")]
        public void GenerateReturnsANumberIfAvailable()
        {
            var virtualNumberRepo = MockVirtualNumberDataSource.GetRepository<VirtualNumber>();
            var virtualNumberAssociationRepo = MockVirtualNumberDataSource
                .GetRepository<VirtualNumberAssociation>();

            virtualNumberRepo.Setup(repo => repo.Get(
                It.IsAny<Func<VirtualNumber, bool>>()))
                .Returns(MockVirtualNumberDataSource.VirtualNumbersForFreeJobApplications);

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get(It.IsAny<Func<VirtualNumberAssociation, bool>>()))
                .Returns(Enumerable.Empty<VirtualNumberAssociation>());

            var virtualNumberService = new VritualNumberService.ApplicationServices.VirtualNumberService(
                virtualNumberRepo.Object,
                virtualNumberAssociationRepo.Object,
                new Mock<IRepository<Purpose>>().Object, 
                new Mock<IRepository<State>>().Object);

            var number = virtualNumberService
                .Generate(new Mock<IVirtualNumberRequest>().Object);

            Assert.AreEqual(
                number.VirtualNumber.VirtualPhoneNumber.Number, 
                MockVirtualNumberDataSource.ValidNumber);
        }

        [Test]
        [Description("If no virtual numbers are available for a given purpose, an exception will be thrown")]
        public void GenerateThrowsExceptionIfThereAreNoAvalaiableNumbers()
        {
            var virtualNumberRepo = MockVirtualNumberDataSource.GetRepository<VirtualNumber>();
            var virtualNumberAssociationRepo = MockVirtualNumberDataSource
                .GetRepository<VirtualNumberAssociation>();

            virtualNumberRepo.Setup(repo => repo.Get(
                It.IsAny<Func<VirtualNumber, bool>>()))
                .Returns(MockVirtualNumberDataSource.VirtualNumbersForFreeJobApplications);

            var mockAssociation = new Mock<VirtualNumberAssociation>();
            mockAssociation.Setup(a => a.VirtualNumber)
                .Returns(new VirtualNumber(
                    new PhoneNumber(MockVirtualNumberDataSource.ValidNumber),
                    new FreeJobApplication(),
                    MockVirtualNumberDataSource.GetProvider));

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get(It.IsAny<Func<VirtualNumberAssociation, bool>>()))
                .Returns(new List<VirtualNumberAssociation> { mockAssociation.Object });

            var virtualNumberService = new VritualNumberService.ApplicationServices.VirtualNumberService(
                virtualNumberRepo.Object,
                virtualNumberAssociationRepo.Object,
                new Mock<IRepository<Purpose>>().Object,
                new Mock<IRepository<State>>().Object);

            Assert.Throws<ApplicationException>(() =>
            {
                virtualNumberService.Generate(new Mock<IVirtualNumberRequest>().Object);
            });
        }
    }
}
