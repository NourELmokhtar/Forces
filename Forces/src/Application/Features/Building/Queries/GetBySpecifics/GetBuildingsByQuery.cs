using Forces.Application.Features.Items.Queries.GetBySpecifics;
using Forces.Application.Interfaces.Repositories;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Building.Queries.GetBySpecifics
{
    internal class GetBuildingsByQuery : IRequest<IResult<List<GetBuildingsByResponse>>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }

    }
    internal class GetAllBuildingsQuesryHandler : IRequestHandler<GetBuildingsByQuery, IResult<List<GetBuildingsByResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllBuildingsQuesryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<List<GetBuildingsByResponse>>> Handle(GetBuildingsByQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
