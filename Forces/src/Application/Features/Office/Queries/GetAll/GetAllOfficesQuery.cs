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

namespace Forces.Application.Features.Office.Queries.GetAll
{
    public class GetAllOfficeQuery : IRequest<IResult<List<GetAllOfficeResponse>>>
    {
        public GetAllOfficeQuery()
        {

        }
    }
    internal class GetAllOfficeQueryHandler : IRequestHandler<GetAllOfficeQuery, IResult<List<GetAllOfficeResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IVoteCodeService _voteCodeService;

        public GetAllOfficeQueryHandler(IUnitOfWork<int> unitOfWork, IVoteCodeService voteCodeService)
        {
            _unitOfWork = unitOfWork;
            _voteCodeService = voteCodeService;
        }

        public Task<IResult<List<GetAllOfficeResponse>>> Handle(GetAllOfficeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
