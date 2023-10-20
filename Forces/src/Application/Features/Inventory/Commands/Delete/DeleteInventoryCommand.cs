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

namespace Forces.Application.Features.Inventory.Commands.Delete
{
    internal class DeleteInventoryCommand : IRequest<IResult<int>>
    {
        [Required]
        public int InventoryId { get; set; }
    }

    internal class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, IResult<int>>
    {
        private readonly IStringLocalizer<DeleteInventoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteInventoryCommandHandler(
            IStringLocalizer<DeleteInventoryCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
