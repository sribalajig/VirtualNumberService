using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Telephony.VritualNumberService.ApplicationServices;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.Modules.VirtualNumbers;

namespace Telephony.VirtualNumberService.Tests
{
    [TestFixture]
    public class VirtualNumberServiceTests
    {
        [Test]
        [Description("When a virtual number is available for a purpose, generate returns that number")]
        public void GenerateReturnsANumberIfAvailable()
        {
            var virtualNumberRepo = MockVirtualNumberDataSource.GetRepository<VirtualNumber, VirtualNumberContext>();
            var virtualNumberAssociationRepo = MockVirtualNumberDataSource
                .GetRepository<VirtualNumberAssociation, VirtualNumberContext>();

            virtualNumberRepo.Setup(repo => repo.Get())
                .Returns(MockVirtualNumberDataSource.VirtualNumbers.AsQueryable());

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get())
                .Returns(Enumerable.Empty<VirtualNumberAssociation>().AsQueryable());

            var virtualNumberService = new VirtualNumberAssociationService(
                virtualNumberAssociationRepo.Object, 
                virtualNumberRepo.Object);

            var virtualNumberRequest = new Mock<IVirtualNumberRequest>();
            virtualNumberRequest.Setup(x => x.Purpose).Returns(new FreeJobApplication());

            var number = virtualNumberService
                .Generate(virtualNumberRequest.Object);

            Assert.AreEqual(
                number.VirtualNumber.VirtualPhoneNumber.Number,
                MockVirtualNumberDataSource.FreeJobApplicationNumber1);
        }

        [Test]
        [Description("If no virtual numbers are available for a given purpose, an exception will be thrown")]
        public void GenerateThrowsExceptionIfThereAreNoAvalaiableNumbers()
        {
            var virtualNumberAssociationRepo = MockVirtualNumberDataSource
                .GetRepository<VirtualNumberAssociation, VirtualNumberContext>();

            var virtualNumberRepo = MockVirtualNumberDataSource.GetRepository<VirtualNumber, VirtualNumberContext>();
            virtualNumberRepo.Setup(repo => repo.Get())
                .Returns(MockVirtualNumberDataSource.VirtualNumbers.Where(
                    x => x.Purpose is FreeJobApplication).AsQueryable());

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get())
                .Returns(MockVirtualNumberDataSource.GetFreeJobAssociations.Select(j => j.Object).AsQueryable());

            var virtualNumberService = new VirtualNumberAssociationService(
                virtualNumberAssociationRepo.Object,
                virtualNumberRepo.Object);

            Assert.Throws<ApplicationException>(() =>
            {
                var virtualNumberRequest = new Mock<IVirtualNumberRequest>();

                virtualNumberRequest.Setup(x => x.Purpose).Returns(new FreeJobApplication());
                virtualNumberRequest.Setup(v => v.Caller).Returns(new User("seeker", 1));


                virtualNumberService.Generate(virtualNumberRequest.Object);
            });
        }
    }
}
