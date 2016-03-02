﻿using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;
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

            var virtualNumberService = new VritualNumberService.ApplicationServices.VirtualNumberService(
                virtualNumberRepo.Object,
                virtualNumberAssociationRepo.Object,
                new Mock<IRepository<Purpose, VirtualNumberContext>>().Object,
                new Mock<IRepository<State, VirtualNumberContext>>().Object);

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
                .GetRepository<VirtualNumberAssociation, VirtualNumberContext>();

            var virtualNumberRepo = MockVirtualNumberDataSource.GetRepository<VirtualNumber, VirtualNumberContext>();
            virtualNumberRepo.Setup(repo => repo.Get())
                .Returns(MockVirtualNumberDataSource.VirtualNumbers.Where(
                    x => x.Purpose is FreeJobApplication).AsQueryable());

            virtualNumberAssociationRepo.Setup(
                repo => repo.Get())
                .Returns(MockVirtualNumberDataSource.GetFreeJobAssociations.Select(j => j.Object).AsQueryable());

            var virtualNumberService = new VritualNumberService.ApplicationServices.VirtualNumberService(
                virtualNumberRepo.Object,
                virtualNumberAssociationRepo.Object,
                new Mock<IRepository<Purpose, VirtualNumberContext>>().Object,
                new Mock<IRepository<State, VirtualNumberContext>>().Object);

            Assert.Throws<ApplicationException>(() =>
            {
                var virtualNumberRequest = new Mock<IVirtualNumberRequest>();

                virtualNumberRequest.Setup(x => x.Purpose).Returns(new FreeJobApplication());

                virtualNumberService.Generate(virtualNumberRequest.Object);
            });
        }
    }
}
