using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.House.Queries.GetAll
{
    public class GetAllHouseQuery : IRequest<IResult<List<GetAllHousesResponse>>>
 {
        public GetAllHouseQuery()
        {
        }
    }

    internal class GetAllHouseQueryHandler : IRequestHandler<GetAllHouseQuery, IResult<List<GetAllHousesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IVoteCodeService _voteCodeService;

        public GetAllHouseQueryHandler(
            IUnitOfWork<int> unitOfWork,
            IVoteCodeService voteCodeService)
        {
            _unitOfWork = unitOfWork;
            _voteCodeService = voteCodeService;
        }

        public Task<IResult<List<GetAllHousesResponse>>> Handle(GetAllHouseQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
