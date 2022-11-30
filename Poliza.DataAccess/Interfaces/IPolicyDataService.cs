using Poliza.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Poliza.DataAccess.Interfaces
{
    public interface IPolicyDataService
    {
        public Task<List<PolicyEntity>> GetPolicies();
        public Task<PolicyEntity> GetPolicy(string placa);
        public Task<PolicyEntity> CreatePolicy(PolicyEntity model);
    }
}
