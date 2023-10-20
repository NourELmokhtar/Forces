using AutoMapper;
using Forces.Application.Enums;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Inventory.Commands.AddEdit
{
    public class AddEditInventoryCommand : IRequest<IResult<int>>
    {
        
    }
    internal class AddEditItemCommandHandler : IRequestHandler<AddEditInventoryCommand, IResult<int>>
    {
        private protected IItemRepository _ItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditItemCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;
        public AddEditItemCommandHandler(IItemRepository itemsRepository, IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditItemCommandHandler> localizer, IVoteCodeService voteCodeService)
        {
            _ItemsRepository = itemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public Task<IResult<int>> Handle(AddEditInventoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
