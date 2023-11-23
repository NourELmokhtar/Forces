using Forces.Application.Features.InventoryDashboard.GetData;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forces.Server.Controllers.v1
{
    [ApiController]
    public class InventoryDashboardController : BaseApiController<InventoryDashboardController>
    {
        private readonly ICurrentUserService _currentUser;

        public InventoryDashboardController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        /// <summary>
        /// Get InventoryDashboard Data
        /// </summary>
        /// <returns>Status 200 OK </returns>
        [Authorize(Policy = Permissions.InventoryDashboards.View)]
        [HttpGet("InventoryId")]
        public async Task<IActionResult> GetDataAsync(int InventoryId)
        {

            var result = await _mediator.Send(new InventoryDashboarDataQuery() { InventoryId = InventoryId });
            return Ok(result);
        }
    }
}