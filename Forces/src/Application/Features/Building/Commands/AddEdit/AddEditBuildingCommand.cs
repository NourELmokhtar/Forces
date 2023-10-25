using AutoMapper;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Building.Commands.AddEdit
{
    public class AddEditBuildingCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string BuildingCode { get; set; }
        public int BaseId { get; set; }
    }
    internal class AddEditBuildingCommandHandler : IRequestHandler<AddEditBuildingCommand, IResult<int>>
    {
        private protected IItemRepository _ItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditBuildingCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;
        public AddEditBuildingCommandHandler(IItemRepository itemsRepository, IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditBuildingCommandHandler> localizer, IVoteCodeService voteCodeService)
        {
            _ItemsRepository = itemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public async Task<IResult<int>> Handle(AddEditBuildingCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var Building = _mapper.Map<Application.Models.Building>(request);

                var IsCodeExist = _unitOfWork.Repository<Application.Models.Building>().Entities.
                    Any(x => x.BuildingCode == request.BuildingCode && x.BaseId == request.BaseId);

                var IsNameExist = _unitOfWork.Repository<Application.Models.Building>().Entities.
                    Any(x => x.BuildingName == request.BuildingName && x.BaseId == request.BaseId);

                if (IsCodeExist)
                {
                    return await Result<int>.FailAsync(_localizer["Building With This Code is Already Exist!"]);

                }
                if (IsNameExist)
                {
                    return await Result<int>.FailAsync(_localizer["Building With This Name is Already Exist!"]);
                }
                await _unitOfWork.Repository<Application.Models.Building>().AddAsync(Building);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Building.Id, _localizer["Building Added!"]);
            }
            else
            {
                var Building = await _unitOfWork.Repository<Application.Models.Building>().GetByIdAsync(request.Id);
                if (Building != null)
                {
                    Building.BuildingName = request.BuildingName ?? Building.BuildingName;
                    Building.BuildingCode = request.BuildingCode ?? Building.BuildingCode;
                    if (request.BaseId != null)
                    {
                        Building.BaseId = request.BaseId;
                    }

                    var Messages = new List<string>();
                    var IsNameExist = _unitOfWork.Repository<Models.Building>().Entities.Any(x => x.BuildingName == Building.BuildingName && x.BaseId != Building.BaseId);
                    var IsCodeExist = _unitOfWork.Repository<Models.Building>().Entities.Any(x => x.BuildingCode == Building.BuildingCode && x.BaseId != Building.BaseId);
                    if (IsCodeExist)
                    {
                        return await Result<int>.FailAsync(_localizer["Building With This Code is Already Exist!"]);

                    }
                    if (IsNameExist)
                    {
                        return await Result<int>.FailAsync(_localizer["Building With This Name is Already Exist!"]);
                    }
                    else
                    {
                        await _unitOfWork.Repository<Models.Building>().UpdateAsync(Building);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(Building.Id, _localizer["Building Updated"]);
                    }



                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Building Not Found!"]);
                }
            }

        }
    }
}
