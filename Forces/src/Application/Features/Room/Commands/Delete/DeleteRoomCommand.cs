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

namespace Forces.Application.Features.Room.Commands.Delete
{
    internal class DeleteRoomCommand : IRequest<IResult<int>>
    {
        [Required]
        public int RoomId { get; set; }
    }

    internal class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, IResult<int>>
    {
        private readonly IStringLocalizer<DeleteRoomCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteRoomCommandHandler(
            IStringLocalizer<DeleteRoomCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            // Implement the logic for deleting a room
            throw new NotImplementedException();
        }
    }
}
