using System;
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
                .Returns(MockVirtualNumberDataSource.VirtualNumbers);

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
                MockVirtualNumberDataSource.FreeJobApplicationNumber1);
        }

        [Test]
        [Description("If no virtual numbers are available for a given purpose, an exception will be thrown")]
        public void GenerateThrowsExceptionIfThereAreNoAvalaiableNumbers()
        {
            var virtualNumberAssociationRepo = MockVirtualNumberDataSource
                .GetRepository<VirtualNumberAssociation>();

            var virtualNumberRepo = MockVirtualNumberDataSource.GetRepository<VirtualNumber>();
            virtualNumberRepo.Setup(repo => repo.Get(
                It.IsAny<Func<VirtualNumber, bool>>()))
                .Returns(MockVirtualNumberDataSource.VirtualNumbers.Where(
                    x => x.Purpose is FreeJobApplication));

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get(It.IsAny<Func<VirtualNumberAssociation, bool>>()))
                .Returns(MockVirtualNumberDataSource.GetFreeJobAssociations.Select(j => j.Object));

            var virtualNumberService = new VritualNumberService.ApplicationServices.VirtualNumberService(
                virtualNumberRepo.Object,
                virtualNumberAssociationRepo.Object,
                new Mock<IRepository<Purpose>>().Object,
                new Mock<IRepository<State>>().Object);

            Assert.Throws<ApplicationException>(() =>
            {
                var virtualNumberRequest = new Mock<IVirtualNumberRequest>();

                virtualNumberRequest.Setup(x => x.Purpose).Returns(new FreeJobApplication());

                virtualNumberService.Generate(virtualNumberRequest.Object);
            });
        }
    }
}
