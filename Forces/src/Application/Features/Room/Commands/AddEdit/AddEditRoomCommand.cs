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

namespace Forces.Application.Features.Room.Commands.AddEdit
{
    internal class AddEditRoomCommand : IRequest<IResult<int>>
    {
        }

    internal class AddEditRoomCommandHandler : IRequestHandler<AddEditRoomCommand, IResult<int>>
    {
        private protected IItemRepository _ItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditRoomCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;

        public AddEditRoomCommandHandler(
            IItemRepository itemsRepository,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            IStringLocalizer<AddEditRoomCommandHandler> localizer,
            IVoteCodeService voteCodeService)
        {
            _ItemsRepository = itemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public Task<IResult<int>> Handle(AddEditRoomCommand request, CancellationToken cancellationToken)
        {
            // Implement the logic for adding/editing a room
            throw new NotImplementedException();
        }
    }
}
