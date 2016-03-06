using System;
using System.Collections.Generic;
using NUnit.Framework;
using Telephony.VritualNumberService.ApplicationServices;
using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Data.Repositories;
using Telephony.VritualNumberService.Entities;
using Telephony.VritualNumberService.Entities.VirtualNumber;
using Telephony.VritualNumberService.Modules.VirtualNumbers;

namespace Telephony.VirtualNumberService.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private IVirtualNumberAssociationService _virtualNumberAssociationService;

        [Test]
        [TestCaseSource(nameof(GetPurposeIds))]
        public void NotMoreThanTwoNumbersAreAllowedForAGivenPurpose(int purposeId)
        {
            var firstNumber = _virtualNumberAssociationService.Generate(
                GetVirtualNumberRequest(1, 2, 1, purposeId));

            Assert.IsNotNull(firstNumber);

            var secondNumber = _virtualNumberAssociationService
                .Generate(GetVirtualNumberRequest(1, 3, 2, purposeId));

            Assert.IsNotNull(secondNumber);

            Assert.Throws<ApplicationException>(() =>
            {
                _virtualNumberAssociationService.Generate(
                    GetVirtualNumberRequest(1, 4, 3, purposeId));

                Assert.AreNotEqual(
                    firstNumber.VirtualNumber.VirtualPhoneNumber.Number,
                    secondNumber.VirtualNumber.VirtualPhoneNumber.Number);
            });
        }

#region setup and teardown
        [TestFixtureSetUp]
        public void Setup()
        {
            _virtualNumberAssociationService = new VirtualNumberAssociationService(
                new Repository<VirtualNumberAssociation, VirtualNumberContext>(),
                new Repository<VirtualNumber, VirtualNumberContext>(),
                new Repository<User, VirtualNumberContext>());

            using (var context = new VirtualNumberContext())
            {
                context.VirtualNumbers.Add(new VirtualNumber
                {
                    PurposeId = 1,
                    ProviderId = 1,
                    VirtualPhoneNumber = new PhoneNumber("9722244076")
                });

                context.VirtualNumbers.Add(new VirtualNumber
                {
                    PurposeId = 1,
                    ProviderId = 1,
                    VirtualPhoneNumber = new PhoneNumber("9722244077")
                });

                context.VirtualNumbers.Add(new VirtualNumber
                {
                    PurposeId = 2,
                    ProviderId = 1,
                    VirtualPhoneNumber = new PhoneNumber("9722244078")
                });

                context.VirtualNumbers.Add(new VirtualNumber
                {
                    PurposeId = 2,
                    ProviderId = 1,
                    VirtualPhoneNumber = new PhoneNumber("9722244079")
                });

                context.SaveChanges();
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            using (var context = new VirtualNumberContext())
            {
                context.Database.ExecuteSqlCommand(
                    "delete from virtualnumbers.virtualnumberassociations");

                context.Database.ExecuteSqlCommand(
                    "delete from virtualnumbers.virtualnumbers");

                context.Database.ExecuteSqlCommand(
                    "delete from virtualnumbers.phonenumbers");

                context.Database.ExecuteSqlCommand(
                    "delete from virtualnumbers.users");
            }
        }
        #endregion

#region helpers
        private static VirtualNumberRequest GetVirtualNumberRequest(
            int seekerId,
            int employerId,
            int jobId, 
            int purposeId)
        {
            return new VirtualNumberRequest(
                new User
                {
                    BabaJobUserTypeId = 2,
                    Id = seekerId
                },
                new User
                {
                    BabaJobUserTypeId = 1,
                    Id = employerId
                },
                purposeId,
                jobId);
        }

        public IEnumerable<int> GetPurposeIds()
        {
            return new List<int> {1, 2};
        }
#endregion
    }
}
