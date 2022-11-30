using Microsoft.EntityFrameworkCore;
using Poliza.Application.Entities;
using Poliza.DataAccess.Interfaces;
using Poliza.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poliza.DataAccess.DataService
{
    public class PolicyDataService : IPolicyDataService
    {
        private readonly PolizaContext polizaContext;

        public PolicyDataService(PolizaContext polizaContext)
        {
            this.polizaContext = polizaContext;
        }

        public async Task<PolicyEntity> CreatePolicy(PolicyEntity model)
        {
            var policy = new Policy
            {
                DateEnd = model.DateEnd.Value,
                DateExpired = model.DateExpired.Value,
                DateInit = model.DateInit.Value,
                CityId = model.CityId,
                Placa = model.Placa,
            };

            polizaContext.Policy.Add(policy);

            await polizaContext.SaveChangesAsync();

            model.Id = policy.Id;

            return model;
        }

        public async Task<List<PolicyEntity>> GetPolicies()
        {
            return await (from policy in polizaContext.Policy
                   select new PolicyEntity
                   {
                       DateEnd = policy.DateEnd,
                       DateExpired = policy.DateExpired,
                       DateInit = policy.DateInit,
                       CityId = policy.CityId,
                       Placa = policy.Placa,
                   }).ToListAsync();
        }

        public async Task<PolicyEntity> GetPolicy(string placa)
        {
            return await (from policy in polizaContext.Policy
                    where policy.Placa.Equals(placa)
                    select new PolicyEntity
                    {
                        Id = policy.Id,
                        DateEnd = policy.DateEnd,
                        DateExpired = policy.DateExpired,
                        DateInit = policy.DateInit,
                        CityId = policy.CityId,
                        Placa = policy.Placa,
                    }).FirstOrDefaultAsync();
        }
    }
}
