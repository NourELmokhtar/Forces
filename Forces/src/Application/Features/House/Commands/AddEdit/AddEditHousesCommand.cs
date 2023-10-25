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

namespace Forces.Application.Features.House.Commands.AddEdit
{
    public class AddEditHouseCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        public string HouseName { get; set; }
        public string HouseCode { get; set; }
        public int BaseId { get; set; }
    }

    internal class AddEditHouseCommandHandler : IRequestHandler<AddEditHouseCommand, IResult<int>>
    {
        private protected IItemRepository _ItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditHouseCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;

        public AddEditHouseCommandHandler(
            IItemRepository itemsRepository,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            IStringLocalizer<AddEditHouseCommandHandler> localizer,
            IVoteCodeService voteCodeService)
        {
            _ItemsRepository = itemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public async Task<IResult<int>> Handle(AddEditHouseCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var House = _mapper.Map<Application.Models.House>(request);

                var IsCodeExist = _unitOfWork.Repository<Application.Models.House>().Entities.
                    Any(x => x.HouseCode == request.HouseCode && x.BaseId == request.BaseId);

                var IsNameExist = _unitOfWork.Repository<Application.Models.House>().Entities.
                    Any(x => x.HouseName == request.HouseName && x.BaseId == request.BaseId);

                if (IsCodeExist)
                {
                    return await Result<int>.FailAsync(_localizer["House With This Code is Already Exist!"]);

                }
                if (IsNameExist)
                {
                    return await Result<int>.FailAsync(_localizer["House With This Name is Already Exist!"]);
                }
                await _unitOfWork.Repository<Application.Models.House>().AddAsync(House);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(House.Id, _localizer["House Added!"]);
            }
            else
            {
                var House = await _unitOfWork.Repository<Application.Models.House>().GetByIdAsync(request.Id);
                if (House != null)
                {
                    House.HouseName = request.HouseName ?? House.HouseName;
                    House.HouseCode = request.HouseCode ?? House.HouseCode;
                    if (request.BaseId != null)
                    {
                        House.BaseId = request.BaseId;
                    }

                    var Messages = new List<string>();
                    var IsNameExist = _unitOfWork.Repository<Models.House>().Entities.Any(x => x.HouseName == House.HouseName && x.BaseId != House.BaseId);
                    var IsCodeExist = _unitOfWork.Repository<Models.House>().Entities.Any(x => x.HouseCode == House.HouseCode && x.BaseId != House.BaseId);
                    if (IsCodeExist)
                    {
                        return await Result<int>.FailAsync(_localizer["House With This Code is Already Exist!"]);

                    }
                    if (IsNameExist)
                    {
                        return await Result<int>.FailAsync(_localizer["House With This Name is Already Exist!"]);
                    }
                    else
                    {
                        await _unitOfWork.Repository<Models.House>().UpdateAsync(House);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(House.Id, _localizer["House Updated"]);
                    }

                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["House Not Found!"]);
                }
            }

        }
    }
}
