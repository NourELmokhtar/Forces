using Forces.Application.Features.Office.Queries.GetAll;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Inventory.Queries.GetAll
{
    public class GetAllInventoryQuery : IRequest<IResult<List<GetAllInventoriesResponse>>>
    {
        public GetAllInventoryQuery()
        {
        }
    }

    internal class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, IResult<List<GetAllInventoriesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IVoteCodeService _voteCodeService;

        public GetAllInventoryQueryHandler(
            IUnitOfWork<int> unitOfWork,
            IVoteCodeService voteCodeService)
        {
            _unitOfWork = unitOfWork;
            _voteCodeService = voteCodeService;
        }

        public async Task<IResult<List<GetAllInventoriesResponse>>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {
            var Inventory = await _unitOfWork.Repository<Models.Inventory>().GetAllAsync();
            var MappedInventory = Inventory.Select(x => new GetAllInventoriesResponse()
            {
                Name = x.Name,
                Id = x.Id,
                BaseSectionId = x.BaseSectionId,
                HouseId = x.HouseId,
                RoomId = x.RoomId,

            }).ToList();
            return await Result<List<GetAllInventoriesResponse>>.SuccessAsync(MappedInventory);
        }
    }


}