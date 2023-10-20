using Forces.Application.Features.House.Queries.GetAll;
using Forces.Application.Interfaces.Repositories;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.House.Queries.GetBySpecifics
{
    internal class GetHouseByQuery : IRequest<IResult<List<GetAllHousesResponse>>>
{
        public int? Id { get; set; }
        public string Address { get; set; }
    }

    internal class GetHouseByQueryHandler : IRequestHandler<GetHouseByQuery, IResult<List<GetAllHousesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetHouseByQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<List<GetAllHousesResponse>>> Handle(GetHouseByQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
