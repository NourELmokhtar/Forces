using Forces.Application.Features.House.Commands.AddEdit;
using Forces.Application.Features.House.Commands.Delete;
using Forces.Application.Features.House.Queries.GetAll;
using Forces.Application.Features.House.Queries.GetBySpecifics;
using Forces.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Threading.Tasks;

namespace Forces.Server.Controllers.v1.House
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : BaseApiController<HouseController>
    {
        [Authorize(Policy = Permissions.House.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditHouseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]

        public async Task<IActionResult> GetALl()
        {
            var Persons = await _mediator.Send(new GetAllHouseQuery());
            return Ok(Persons);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetByCondition(GetHouseByQuery command)
        {
            var Persons = await _mediator.Send(command);
            return Ok(Persons);
        }

        [Authorize(Policy = Permissions.Person.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteHouseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
