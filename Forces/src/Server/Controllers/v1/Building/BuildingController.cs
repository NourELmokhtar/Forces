using Forces.Application.Features.Building.Commands.AddEdit;
using Forces.Application.Features.Building.Commands.Delete;
using Forces.Application.Features.Building.Queries.GetAll;
using Forces.Application.Features.Building.Queries.GetBySpecifics;
using Forces.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Threading.Tasks;

namespace Forces.Server.Controllers.v1.Building
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : BaseApiController<BuildingController>
    {
        [Authorize(Policy = Permissions.Building.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditBuildingCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]

        public async Task<IActionResult> GetALl()
        {
            var Persons = await _mediator.Send(new GetAllBuildingsQuery());
            return Ok(Persons);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetByCondition(GetBuildingsByQuery command)
        {
            var Persons = await _mediator.Send(command);
            return Ok(Persons);
        }

        [Authorize(Policy = Permissions.Person.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteBuildingCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
