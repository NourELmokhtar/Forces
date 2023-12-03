using Microsoft.AspNetCore.Authorization;
using Forces.Shared.Constants.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Forces.Application.Features.BaseDashboards.Queries.GetData;

namespace Forces.Server.Controllers.v1
{
    [ApiController]
    public class InventoryDashboard : BaseApiController<InventoryDashboard>
    {
/*        [Authorize(Policy = Permissions.InventoryDashboards.View)]*/
        [HttpGet("BaseId")]
        public async Task<IActionResult> GetDataAsync(int BaseId)
        {

            var result = await _mediator.Send(new GetBaseDashboardDataQuery() { BaseId = BaseId });
            return Ok(result);
        }
    }
}
