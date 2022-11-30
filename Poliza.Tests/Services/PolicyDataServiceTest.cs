using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poliza.Application.Entities;
using Poliza.DataAccess.DataService;
using Poliza.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poliza.Tests.Services
{
    [TestClass]
    public class PolicyDataServiceTest
    {
        [TestMethod]
        public async Task GetPolicies_Successfull()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = CreateContext(nameDB);

            SeedPolicies(context);

            var contextTest = CreateContext(nameDB);

            var policyService = new PolicyDataService(contextTest);

            var policies = await policyService.GetPolicies();

            Assert.AreEqual(2, policies.Count);
            Assert.AreEqual("jf4", policies[0].Placa);
            Assert.AreEqual(1, policies[0].CityId);
            Assert.AreEqual(DateTime.Parse("2022-12-29 13:26"), policies[0].DateEnd);
            Assert.AreEqual(DateTime.Parse("2022-12-30 13:26"), policies[0].DateExpired);
            Assert.AreEqual(DateTime.Parse("2022-12-31 13:26"), policies[0].DateInit);
            Assert.AreEqual("jf", policies[1].Placa);
            Assert.AreEqual(1, policies[1].CityId);
            Assert.AreEqual(DateTime.Parse("2022-11-29 13:26"), policies[1].DateEnd);
            Assert.AreEqual(DateTime.Parse("2022-11-30 13:26"), policies[1].DateExpired);
            Assert.AreEqual(DateTime.Parse("2022-11-01 13:26"), policies[1].DateInit);
        }

        [TestMethod]
        public async Task GetPolicy_Successfull()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = CreateContext(nameDB);

            SeedPolicies(context);

            var contextTest = CreateContext(nameDB);

            var policyService = new PolicyDataService(contextTest);

            var placa = "jf4";
            var policy = await policyService.GetPolicy(placa);

            Assert.AreEqual("jf4", policy.Placa);
            Assert.AreEqual(1, policy.CityId);
            Assert.AreEqual(DateTime.Parse("2022-12-29 13:26"), policy.DateEnd);
            Assert.AreEqual(DateTime.Parse("2022-12-30 13:26"), policy.DateExpired);
            Assert.AreEqual(DateTime.Parse("2022-12-31 13:26"), policy.DateInit);
        }

        [TestMethod]
        public async Task GetPolicy_NotFound()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = CreateContext(nameDB);

            SeedPolicies(context);

            var contextTest = CreateContext(nameDB);

            var policyService = new PolicyDataService(contextTest);

            var placa = "noexiste";
            var policy = await policyService.GetPolicy(placa);

            Assert.IsNull(policy);
        }

        [TestMethod]
        public async Task CreatePolicy_Successfull()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = CreateContext(nameDB);

            SeedPolicies(context);

            var contextTest = CreateContext(nameDB);

            var policyService = new PolicyDataService(contextTest);

            var policy = new PolicyEntity
            {
                DateEnd = DateTime.Parse("2022-12-29 13:26"),
                DateExpired = DateTime.Parse("2022-12-30 13:26"),
                DateInit = DateTime.Parse("2022-12-31 13:26"),
                Placa = "312412",
                CityId = 1
            };

            var policyNew = await policyService.CreatePolicy(policy);

            Assert.AreEqual("312412", policyNew.Placa);
            Assert.AreEqual(1, policyNew.CityId);
            Assert.AreEqual(DateTime.Parse("2022-12-29 13:26"), policyNew.DateEnd);
            Assert.AreEqual(DateTime.Parse("2022-12-30 13:26"), policyNew.DateExpired);
            Assert.AreEqual(DateTime.Parse("2022-12-31 13:26"), policyNew.DateInit);
        }

        private PolizaContext CreateContext(string nameDB)
        {
            var options = new DbContextOptionsBuilder<PolizaContext>()
                .UseInMemoryDatabase(nameDB).Options;
            var context = new PolizaContext(options);

            return context;
        }

        private void SeedPolicies(PolizaContext context)
        {
            var policies = new List<Policy>
            {
                new Policy
                {
                    DateEnd = DateTime.Parse("2022-12-29 13:26"),
                    DateExpired = DateTime.Parse("2022-12-30 13:26"),
                    DateInit = DateTime.Parse("2022-12-31 13:26"),
                    Placa = "jf4",
                    CityId = 1
                },

                new Policy
                {
                    DateEnd = DateTime.Parse("2022-11-29 13:26"),
                    DateExpired = DateTime.Parse("2022-11-30 13:26"),
                    DateInit = DateTime.Parse("2022-11-01 13:26"),
                    Placa = "jf",
                    CityId = 1
                }
            };

            var city = new City
            {
                Name = "Ibagué"
            };

            context.Add(city);
            context.AddRange(policies);

            context.SaveChanges();
        }
    }
}
