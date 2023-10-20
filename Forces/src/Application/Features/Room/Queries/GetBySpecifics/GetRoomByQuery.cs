using Forces.Application.Features.Room.Queries.GetAll;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Responses.Identity;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Room.Queries.GetBySpecifics
{
    internal class GetRoomByQuery : IRequest<IResult<List<GetRoomByResponse>>>
{
        public int? Id { get; set; }
        public string RoomName { get; set; }
    }

    internal class GetRoomByQueryHandler : IRequestHandler<GetRoomByQuery, IResult<List<GetRoomByResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetRoomByQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<List<GetRoomByResponse>>> Handle(GetRoomByQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
