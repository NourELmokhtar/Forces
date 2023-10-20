using Forces.Application.Interfaces.Repositories;
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

namespace Forces.Application.Features.Building.Commands.Delete
{
    internal class DeleteBuildingCommand: IRequest<IResult<int>>
    {
        [Required]
        public int BuildingId { get; set; }
    }
    internal class DeleteItemCommandHandler : IRequestHandler<DeleteBuildingCommand, IResult<int>>
    {
        private readonly IStringLocalizer<DeleteItemCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteItemCommandHandler(IStringLocalizer<DeleteItemCommandHandler> localizer, IUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
