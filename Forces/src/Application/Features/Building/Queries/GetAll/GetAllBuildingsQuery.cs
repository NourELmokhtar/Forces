using Forces.Application.Features.Items.Queries.GetAll;
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

namespace Forces.Application.Features.Building.Queries.GetAll
{
    public class GetAllBuildingsQuery : IRequest<IResult<List<GetAllBuildingsResponse>>>
    {
        public GetAllBuildingsQuery()
        {

        }
    }
    internal class GetAllBuildingsQueryHandler : IRequestHandler<GetAllBuildingsQuery, IResult<List<GetAllBuildingsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IVoteCodeService _voteCodeService;

        public GetAllBuildingsQueryHandler(IUnitOfWork<int> unitOfWork, IVoteCodeService voteCodeService)
        {
            _unitOfWork = unitOfWork;
            _voteCodeService = voteCodeService;
        }

        public Task<IResult<List<GetAllBuildingsResponse>>> Handle(GetAllBuildingsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
