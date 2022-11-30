using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poliza.Application.Entities;
using Poliza.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace Poliza.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyDataService _policyService;
        private readonly ILogger<PolicyController> _logger;

        public PolicyController(IPolicyDataService policyService, ILogger<PolicyController> logger)
        {
            _policyService = policyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Getting policies");

                var policies = await _policyService.GetPolicies();

                _logger.LogInformation($"{policies.Count} were gotten");

                return Ok(policies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");

                return new ContentResult
                {
                    Content = "Error Internal Server",
                    ContentType = "application/json",
                    StatusCode = 500
                };
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] string placa)
        {
            try
            {
                _logger.LogInformation($"Getting policy {placa}");

                var policy = await _policyService.GetPolicy(placa);

                if (policy == null)
                {
                    _logger.LogInformation($"policy {placa} was not found");
                    NotFound();
                }

                _logger.LogInformation($"policy {placa} was found");

                return Ok(policy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");

                return InternalError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PolicyEntity model)
        {
            try
            {
                if (DateTime.UtcNow < model.DateExpired)
                {
                    var msg = "It is not possible creating a policy right now";

                    _logger.LogInformation(msg);

                    return BadRequest(msg);
                }

                var policy = await _policyService.CreatePolicy(model);

                return Ok(policy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");

                return InternalError();
            }
        }

        private IActionResult InternalError()
        {
            return new ContentResult
            {
                Content = "Error Internal Server",
                ContentType = "application/json",
                StatusCode = 500
            };
        }
    }
}
