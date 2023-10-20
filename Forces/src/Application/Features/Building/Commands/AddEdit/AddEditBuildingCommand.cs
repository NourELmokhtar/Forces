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
    internal class AddEditBuildingCommand : IRequest<IResult<int>>
    {

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

        public Task<IResult<int>> Handle(AddEditBuildingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
