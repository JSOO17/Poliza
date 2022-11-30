using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Poliza.Application.Entities;
using Poliza.Controllers;
using Poliza.DataAccess.Interfaces;
using Poliza.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Poliza.Tests
{
    [TestClass]
    public class PolicyControllerTest
    {
        [TestMethod]
        public async Task GetPolicies_Successfull()
        {
            var policyMock = new Mock<IPolicyDataService>();
            var loggerMock = new Mock<ILogger<PolicyController>>();

            policyMock.Setup(x => x.GetPolicies()).Returns(Task.FromResult(new List<PolicyEntity>
            {
                new PolicyEntity
                {
                    DateEnd = DateTime.Parse("2022-12-29 13:26"),
                    DateExpired = DateTime.Parse("2022-12-30 13:26"),
                    DateInit = DateTime.Parse("2022-12-31 13:26"),
                    Placa = "jf4",
                    CityId = 1
                },

                new PolicyEntity
                {
                    DateEnd = DateTime.Parse("2022-11-29 13:26"),
                    DateExpired = DateTime.Parse("2022-11-30 13:26"),
                    DateInit = DateTime.Parse("2022-11-01 13:26"),
                    Placa = "jf",
                    CityId = 1
                }
            }));

            var policyController = new PolicyController(policyMock.Object, loggerMock.Object);

            var response = (OkObjectResult)await policyController.Get();

            Assert.AreEqual(200, response.StatusCode);
        }
    }
}
