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
    internal class AddEditHouseCommand : IRequest<IResult<int>>
    {
        
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

        public Task<IResult<int>> Handle(AddEditHouseCommand request, CancellationToken cancellationToken)
        {
            // Implement the logic for adding/editing a house
            throw new NotImplementedException();
        }
    }
}
