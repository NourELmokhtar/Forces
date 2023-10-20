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

namespace Forces.Application.Features.House.Commands.Delete
{
    internal class DeleteHouseCommand : IRequest<IResult<int>>
    {
        [Required]
        public int HouseId { get; set; }
    }

    internal class DeleteHouseCommandHandler : IRequestHandler<DeleteHouseCommand, IResult<int>>
    {
        private readonly IStringLocalizer<DeleteHouseCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteHouseCommandHandler(
            IStringLocalizer<DeleteHouseCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(DeleteHouseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
