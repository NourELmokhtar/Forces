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
                BaseSectionName = x.BaseSectionId != null
                ? _unitOfWork.Repository<Models.BasesSections>().GetAllAsync().Result
                    .Where(y => y.Id == x.BaseSectionId)
                    .FirstOrDefault()?.SectionName
                : null,
                HouseName = x.HouseId != null
                ? _unitOfWork.Repository<Models.House>().GetAllAsync().Result
                    .Where(y => y.Id == x.HouseId)
                    .FirstOrDefault()?.HouseName
                : null,
                RoomName = x.RoomId != null
                ? _unitOfWork.Repository<Models.Room>().GetAllAsync().Result
                    .Where(y => y.Id == x.RoomId)
                    .FirstOrDefault()?.RoomNumber
                : null,
                BaseName = x.BaseSectionId != null
                ? _unitOfWork.Repository<Models.Bases>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.BasesSections>().GetAllAsync().Result
                    .Where(y => y.Id == x.BaseSectionId)
                    .FirstOrDefault()?.BaseId)
                    .FirstOrDefault()?.BaseName
                : x.HouseId != null
                ? _unitOfWork.Repository<Models.Bases>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.House>().GetAllAsync().Result
                    .Where(y => y.Id == x.HouseId)
                    .FirstOrDefault()?.BaseId)
                    .FirstOrDefault()?.BaseName:
                    x.RoomId != null
                ? _unitOfWork.Repository<Models.Bases>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.Building>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.Room>().GetAllAsync().Result
                    .Where(y => y.Id == x.RoomId)
                    .FirstOrDefault()?.BuildingId)
                    .FirstOrDefault()?.BaseId)
                    .FirstOrDefault()?.BaseName:null,


            }).ToList();
            return await Result<List<GetAllInventoriesResponse>>.SuccessAsync(MappedInventory);
        }
    }


}