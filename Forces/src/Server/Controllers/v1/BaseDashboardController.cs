using Forces.Application.Features.BaseDashboards.Queries.GetData;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forces.Server.Controllers.v1
{
    [ApiController]
    public class BaseDashboardController : BaseApiController<BaseDashboardController>
    {
        private readonly ICurrentUserService _currentUser;

        public BaseDashboardController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        /// <summary>
        /// Get BaseDashboard Data
        /// </summary>
        /// <returns>Status 200 OK </returns>
        [Authorize(Policy = Permissions.BaseDashboards.View)]
        [HttpGet("BaseId")]
        public async Task<IActionResult> GetDataAsync(int BaseId)
        {

            var result = await _mediator.Send(new GetBaseDashboardDataQuery() { BaseId = BaseId });
            return Ok(result);
        }
    }
}