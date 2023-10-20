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

namespace Forces.Application.Features.Office.Commands.Delete
{
    internal class DeleteOfficeCommand : IRequest<IResult<int>>
    {
        [Required]
        public int OfficeId { get; set; }
    }
    internal class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, IResult<int>>
    {
        private readonly IStringLocalizer<DeleteOfficeCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteOfficeCommandHandler(IStringLocalizer<DeleteOfficeCommandHandler> localizer, IUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
